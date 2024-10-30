using Accord;
using CM20314.Helpers;
using CM20314.Models;
using CM20314.Models.Database;
using CM20314.Services;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;

namespace CM20314.Data
{
    /// <summary>
    /// Responsible for seeding the database using files from the Raw/ directory
    /// </summary>
    public class DbInitialiser
    {
        #pragma warning disable CS8618
        private FileService _fileService;
        private ApplicationDbContext _context;
        #pragma warning restore CS8618

        /// <summary>
        /// Initialises the DbInitialiser service
        /// </summary>
        /// <param name="context">DB Context to access database</param>
        /// <param name="fileService">File service to manipulate input files</param>
        public void Initialise(ApplicationDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
            switch (Constants.STARTUP_MODE)
            {
                case StartupMode.UseExistingDb:
                    break;
                case StartupMode.GenerateDb:
                    GenerateDbFromFiles();
                    break;
            }
        }

        /// <summary>
        /// Clears and seeds the database from raw file information
        /// </summary>
        private void GenerateDbFromFiles()
        {
            ClearDatabase();
            // Open paths file, create all Nodes and NodeArcs, without duplicating Nodes.
            ProcessPaths(Constants.SourceFilePaths.PATHS_FILENAME, Constants.SourceFilePaths.FLOOR_OUTDOOR, 0, new MapOffset(), true);     
            // Open buildings file one by one, create all Polylines and entrance Nodes and NodeArcs.
            ProcessBuildingBoundariesAndEntrances();
            // Open buildings floors folders one by one:
            ProcessBuildings();
            // Ensure coordinates are adjusted such that the top left corner is the origin
            var coords = StandardiseCoordinates();
            // Computes arc costs for pathfinding calculations
            ComputeNodeArcCosts(coords);
        }

        /// <summary>
        /// Traverses nodes and node arcs, splitting long node arcs into shorter ones
        /// </summary>
        public void SplitArcsAndConfigureJunctions()
        {
            List<Node> nodes = _context.Node.ToList();
            List<NodeArc> nodeArcs = _context.NodeArc.ToList();
            foreach (Node node in nodes)
            {
                node.Coordinate = _context.Coordinate.First(c => c.Id == node.CoordinateId);
                SetNodeJunctionCount(node);
            }
            _context.SaveChanges();
            foreach(NodeArc nodeArc in nodeArcs)
            {
                nodeArc.Node1 = nodes.First(a => a.Id == nodeArc.Node1Id);
                nodeArc.Node2 = nodes.First(a => a.Id == nodeArc.Node2Id);
                SplitAndSaveNodeArc(nodeArc);
            }
        }

        /// <summary>
        /// Computes number of junctions encountered by each node
        /// </summary>
        /// <param name="node">Node to analyse</param>
        private void SetNodeJunctionCount(Node node)
        {
            int count = _context.NodeArc.Where(a => a.Node1Id == node.Id || a.Node2Id == node.Id).Count();
            if(count > 0)
            {
                node.JunctionSize = count;
            }
        }

        /// <summary>
        /// Opens a path file, extracts coordinates and creates Nodes and NodeArcs.
        /// </summary>
        /// <param name="filename">File name to open</param>
        /// <param name="floorNum">Elevation for path</param>
        /// <param name="buildingId">Building ID (if path is in building)</param>
        /// <param name="mapOffset">Offset needed to map to system coordinates</param>
        /// <param name="isDisplayablePath">Should display on map (some node arcs are hidden)</param>
        private void ProcessPaths(string filename, int floorNum, int buildingId, MapOffset mapOffset, bool isDisplayablePath = false)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(filename);
            List<Node> currentLineNodes = new List<Node>();
            Stack<string> matchHandles = new Stack<string>();
            bool stepFree = true;

            for (int i = 0; i < lines.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine($"Extracting path {i} of {lines.Count}");
                lines[i] = lines[i].FormatCoordinateLine();

                if (lines[i].StartsWith("at point"))
                {
                    List<double> coordinates = lines[i].Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                    .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                    Coordinate coordinate = new Coordinate(coordinates[0] + mapOffset.OffX, coordinates[1] + mapOffset.OffY);
                    
                    Coordinate? matchingCoordinate = _context.Coordinate.FirstOrDefault(c => c.X == coordinate.X && c.Y == coordinate.Y);
                    
                    if(matchingCoordinate == null) {
                        _context.Coordinate.Add(coordinate);
                        _context.SaveChanges();
                        matchingCoordinate = coordinate;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Matching coordinate");
                    }

                    Node? matchingNode = _context.Node.FirstOrDefault(n => n.CoordinateId == matchingCoordinate.Id);

                    if (matchingNode == null)
                    {
                        if (matchHandles.Count > 0)
                        {
                            matchingNode = new Node(floorNum, buildingId, matchingCoordinate.Id, matchHandles.Pop());
                        }
                        else
                        {
                            matchingNode = new Node(floorNum, buildingId, matchingCoordinate.Id);
                        }
                        matchingNode.Coordinate = matchingCoordinate;
                        _context.Node.Add(matchingNode);
                        _context.SaveChanges();
                    }

                    currentLineNodes.Add(matchingNode);
                }
                else if (lines[i].StartsWith("MatchHandle"))
                {
                    matchHandles.Push(lines[i].Split(" = ")[1]);
                }
                else if (lines[i].StartsWith("LWPOLYLINE"))
                {
                    CreateNodeArcsForPath(currentLineNodes, stepFree, isDisplayablePath);
                    currentLineNodes.Clear();
                }
                else if (lines[i].StartsWith("# NOT STEP FREE"))
                {
                    stepFree = false;
                }
            }
            CreateNodeArcsForPath(currentLineNodes, stepFree, isDisplayablePath);
        }

        /// <summary>
        /// Creates NodeArcs for a given list of nodes
        /// </summary>
        /// <param name="nodes">Nodes to process</param>
        /// <param name="stepFree">True if path is step-free, otherwise False</param>
        /// <param name="isDisplayablePath">Should display on map (some node arcs are hidden)</param>
        private void CreateNodeArcsForPath(List<Node> nodes, bool stepFree, bool isDisplayablePath)
        {
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Coordinate? c1 = _context.Coordinate.First(c => c.Id == nodes[i].CoordinateId);
                Coordinate? c2 = _context.Coordinate.First(c => c.Id == nodes[i + 1].CoordinateId);
                NodeArc nodeArc = new NodeArc(
                    nodes[i], nodes[i + 1], stepFree,
                    Coordinate.CalculateEucilidianDistance(c1, c2),
                    NodeArcType.Path, false, isDisplayablePath, true);

                _context.NodeArc.Add(nodeArc);
                SplitAndSaveNodeArc(nodeArc);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Splits node arc and saves it if it is too long
        /// </summary>
        /// <param name="nodeArc">Node arc to process</param>
        private void SplitAndSaveNodeArc(NodeArc nodeArc)
        {
            NodeArcSplitSet splitNodeArc = SplitNodeArc(nodeArc);
            if(splitNodeArc.NodeArcs.Count > 1)
            {
                System.Diagnostics.Debug.WriteLine("Splitting arc");
                _context.NodeArc.Remove(nodeArc);
                foreach (Node node in splitNodeArc.Nodes)
                {
                    _context.Coordinate.Add(node.Coordinate);
                    _context.SaveChanges();
                    node.CoordinateId = node.Coordinate.Id;
                }
                _context.AddRange(splitNodeArc.Nodes);
                _context.SaveChanges();

                foreach (NodeArc arc in splitNodeArc.NodeArcs)
                {
                    arc.Node1Id = arc.Node1.Id;
                    arc.Node2Id = arc.Node2.Id;
                    _context.NodeArc.Add(arc);
                }
            }
        }

        /// <summary>
        /// Splits node arc if it is too long
        /// </summary>
        /// <param name="nodeArc">Node arc to process</param>
        /// <returns>Resulting (new) nodes and node arcs</returns>
        public static NodeArcSplitSet SplitNodeArc(NodeArc nodeArc)
        {
            NodeArcSplitSet set = new NodeArcSplitSet();

            var distance = Coordinate.CalculateEucilidianDistance(nodeArc.Node1.Coordinate, nodeArc.Node2.Coordinate);
            if (distance > Constants.MAX_NODE_ARC_LENGTH)
            {
                int numberOfIntermediateNodes = (int)Math.Floor(distance / Constants.MAX_NODE_ARC_LENGTH);
                if (numberOfIntermediateNodes == 1)
                {
                    set.NodeArcs.Add(nodeArc);
                    return set;
                }
                double xStep = (nodeArc.Node2.Coordinate.X - nodeArc.Node1.Coordinate.X) / (numberOfIntermediateNodes + 1);
                double yStep = (nodeArc.Node2.Coordinate.Y - nodeArc.Node1.Coordinate.Y) / (numberOfIntermediateNodes + 1);

                for(int i = 1; i <= numberOfIntermediateNodes; i++)
                {
                    double intermediateX = nodeArc.Node1.Coordinate.X + i * xStep;
                    double intermediateY = nodeArc.Node1.Coordinate.Y + i * yStep;
                    Node intermediateNode = new Node(nodeArc.Node1.Floor, nodeArc.Node1.BuildingId, 0, coordinate: new Coordinate(intermediateX, intermediateY));
                    set.Nodes.Add(intermediateNode);
                }

                List<Node> allRelevantNodes = new List<Node>() { nodeArc.Node1, nodeArc.Node2 };
                allRelevantNodes.InsertRange(1, set.Nodes);
                for(int j = 0; j < allRelevantNodes.Count() - 1; j++)
                {
                    NodeArc newNodeArc = new NodeArc(allRelevantNodes.ElementAt(j), allRelevantNodes.ElementAt(j + 1), nodeArc.StepFree,
                        Coordinate.CalculateEucilidianDistance(allRelevantNodes.ElementAt(j).Coordinate, allRelevantNodes.ElementAt(j + 1).Coordinate),
                        (NodeArcType)nodeArc.NodeArcType, nodeArc.RequiresUsageRequest, nodeArc.IsMapDisplayablePath, true);
                    set.NodeArcs.Add(newNodeArc);
                }
            }

            return set;
        }

        /// <summary>
        /// Processes all building boundaries (polylines) and entrances (i.e. links from sitewide path nodes to building nodes)
        /// </summary>
        private void ProcessBuildingBoundariesAndEntrances()
        {
            foreach (string buildingName in Constants.SourceFilePaths.BUILDING_NAMES)
            {
                System.Diagnostics.Debug.WriteLine($"Extracting building boundaries and entrances for {buildingName}");
                List<string> lines = _fileService.ReadLinesFromFileWithName(buildingName);
                string shortName = lines[0];
                string longName = lines[1];
                Stack<string> matchHandles = new Stack<string>();

                bool pointsAreEntranceNodes = false;

                List<Coordinate> polylineCoordinates = new List<Coordinate>();
                List<Coordinate> entranceCoordinates = new List<Coordinate>(); // first - on sitewide path, second - inside building

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith("at point"))
                    {
                        // Extract coordinates
                        lines[i] = lines[i].FormatCoordinateLine();
                        List<double> coordinates = lines[i].Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                        .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                        Coordinate coordinate = new Coordinate(coordinates[0], coordinates[1], (int)coordinates[2]);
                        if (!pointsAreEntranceNodes)
                        {
                            polylineCoordinates.Add(coordinate);
                        }
                        else
                        {
                            if (matchHandles.Count > 0)
                            {
                                coordinate.MatchHandle = matchHandles.Pop();
                            }
                            entranceCoordinates.Add(coordinate);
                        }
                    }
                    else if (lines[i].StartsWith("MatchHandle"))
                    {
                        matchHandles.Push(lines[i].Split(" = ")[1]);
                    }
                    else if (lines[i].StartsWith("#ENTRANCES"))
                    {
                        pointsAreEntranceNodes = true;
                    }
                }

                // If points are ABCD we want ABCD not ABCDA
                if(polylineCoordinates.First().Equals(polylineCoordinates.Last()))
                {
                    polylineCoordinates.RemoveAt(polylineCoordinates.Count - 1);
                }

                _context.Coordinate.AddRange(polylineCoordinates);
                _context.SaveChanges();

                Polyline polyline = new Polyline(polylineCoordinates);
                List<int> floors = Constants.SourceFilePaths.BUILDING_FLOORS.ContainsKey(buildingName) ? Constants.SourceFilePaths.BUILDING_FLOORS[buildingName] : new List<int>();
                Building building = new Building(shortName, longName, polyline.ToString(), floors);
                _context.Building.Add(building);
                _context.SaveChanges();

                for (int i = 0; i < entranceCoordinates.Count; i += 2)
                {
                    Coordinate entranceNodeCoord = new Coordinate(entranceCoordinates[i + 1].X, entranceCoordinates[i + 1].Y, entranceCoordinates[i + 1].Z);
                    _context.Coordinate.Add(entranceNodeCoord);
                    _context.SaveChanges();

                    Node entranceNode = new Node(entranceNodeCoord.Z, building.Id, entranceNodeCoord.Id, entranceCoordinates[i].MatchHandle ?? "");
                    _context.Node.Add(entranceNode);
                    _context.SaveChanges();

                    Coordinate? coord = _context.Coordinate.FirstOrDefault(c => c.X == entranceCoordinates[i].X && c.Y == entranceCoordinates[i].Y);
                    if (coord != null)
                    {
                        Node? sitewidePathNode = _context.Node.FirstOrDefault(n => n.CoordinateId == coord.Id);
                        if (sitewidePathNode != null)
                        {
                            sitewidePathNode.MatchHandle = entranceCoordinates[i].MatchHandle ?? "";
                            _context.Node.Update(sitewidePathNode);
                            NodeArc entranceNodeArc = new NodeArc(sitewidePathNode, entranceNode, true,
                                Coordinate.CalculateEucilidianDistance(entranceNodeCoord, coord), NodeArcType.Path, false, false, false);
                            _context.NodeArc.Add(entranceNodeArc);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Floor-level processing for buildings
        /// </summary>
        private void ProcessBuildings()
        {
            foreach (string buildingName in Constants.SourceFilePaths.BUILDING_NAMES)
            {
                System.Diagnostics.Debug.WriteLine($"Extracting floors for {buildingName}");

                List<int> floors = Constants.SourceFilePaths.BUILDING_FLOORS.ContainsKey(buildingName) ?
                    Constants.SourceFilePaths.BUILDING_FLOORS[buildingName] : new List<int>();
                if (floors.Count() == 0) continue;
                Building building = _context.Building.First(b => b.ShortName.Equals(buildingName));

                foreach (int floor in floors)
                {
                    if (_fileService.FolderExistsForFloor(buildingName, floor))
                    {
                        System.Diagnostics.Debug.WriteLine($"Folder exists for {building}-{floor}");

                        ProcessFloor(building, floor);
                    }
                }
            }
        }

        /// <summary>
        /// Processes floor-specific files
        /// </summary>
        /// <param name="building">Building to process</param>
        /// <param name="floor">Floor to process</param>
        private void ProcessFloor(Building building, int floor)
        {
            // SHOULD FIRST FIND MULTIPLIER AND OFFSET
            MapOffset mapOffset = ComputeMapOffset(building, floor, 1);

            //      Create floor, create polyline boundary.
            System.Diagnostics.Debug.WriteLine($"Extracting floor boundaries for {building.LongName}");
            ProcessFloorBoundary(building, floor, mapOffset);
            //      Open paths, create all Nodes and NodeArcs, without duplicating Nodes.
            System.Diagnostics.Debug.WriteLine($"Extracting paths for {building.LongName}");
            ProcessPaths($"{building.ShortName}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floor}\\{Constants.SourceFilePaths.FLOOR_FILENAME_PATHS}", floor, building.Id, mapOffset);
            //      Open containers, create Rooms and corridors, for rooms
            System.Diagnostics.Debug.WriteLine($"Extracting floor containers for {building.LongName}");
            ProcessFloorContainers(building, floor, mapOffset);
            //      Open internal links (between floors), create Nodes and add flag
            //      Open external links, create NodeArcs to connect to outside nodes.
            System.Diagnostics.Debug.WriteLine($"Extracting external floor links for {building.LongName}");
            ProcessExternalFloorLinks(building, floor, mapOffset);
        }

        /// <summary>
        /// Computes x and y offset by determining a reference point between the two scales
        /// </summary>
        /// <param name="building">Building to process</param>
        /// <param name="floorNum">Floor to process</param>
        /// <param name="scale">Scale for zoom (currently placeholder)</param>
        /// <returns>Required offset</returns>
        private MapOffset ComputeMapOffset(Building building, int floorNum, double scale)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_EXTERNAL_LINKS,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.ShortName}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");
            Stack<string> matchHandles = new Stack<string>();
            List<Coordinate> currentLineCoords = new List<Coordinate>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("at point"))
                {
                    lines[i] = lines[i].FormatCoordinateLine();
                    // Extract coordinates from line
                    List<double> coordinates = lines[i].Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                    .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                    Coordinate coordinate;
                    if (matchHandles.Count > 0)
                    {
                        coordinate = new Coordinate(coordinates[0], coordinates[1], matchHandle: matchHandles.Pop());
                    }
                    else
                    {
                        coordinate = new Coordinate(coordinates[0], coordinates[1]);
                    }

                    currentLineCoords.Add(coordinate);

                }
                else if (lines[i].StartsWith("MatchHandle"))
                {
                    matchHandles.Push(lines[i].Split(" = ")[1]);
                }
                else if (lines[i].StartsWith("LWPOLYLINE") && currentLineCoords.Count > 0)
                {
                    break;
                }
            }

            Node matchingNode = _context.Node.First(n => n.MatchHandle.Equals(currentLineCoords[1].MatchHandle));

            Coordinate n1coordinate = _context.Coordinate.First(c => c.Id == matchingNode.CoordinateId);

            return CalculateOffset(currentLineCoords[1], n1coordinate, scale);
        }

        /// <summary>
        /// Computes offset between two coordinates.
        /// </summary>
        /// <param name="src">Source coordinate</param>
        /// <param name="dst">Destination coordinate</param>
        /// <param name="scale">Scale for zoom (currently placeholder)</param>
        /// <returns></returns>
        private MapOffset CalculateOffset(Coordinate src, Coordinate dst, double scale)
        {
            double offsetX = dst.X - src.X;
            double offsetY = dst.Y - src.Y;

            return new MapOffset(offsetX, offsetY, scale);
        }

        /// <summary>
        /// Processes polyline boundary for a floor.
        /// </summary>
        /// <param name="building">Building to process</param>
        /// <param name="floorNum">Floor number to process</param>
        /// <param name="mapOffset">Offset to apply to points</param>
        private void ProcessFloorBoundary(Building building, int floorNum, MapOffset mapOffset)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_BOUNDARY,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.ShortName}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");
            List<Coordinate> polylineCoords = new List<Coordinate>();

            foreach (string line in lines)
            {
                List<double> coordinates = line.FormatCoordinateLine().Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                        .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                Coordinate coordinate = new Coordinate(coordinates[0] + mapOffset.OffX, coordinates[1] + mapOffset.OffY, (int)coordinates[2]);
                polylineCoords.Add(coordinate);
            }

            _context.Coordinate.AddRange(polylineCoords);
            _context.SaveChanges();
            Polyline polyline = new Polyline(polylineCoords);
            Floor floor = new Floor(floorNum.ToString(), floorNum.ToString(), polyline.ToString());
            _context.Floor.Add(floor);
            _context.SaveChanges();
        }

        /// <summary>
        /// Processes polyline boundaries for containers (rooms) within a floor
        /// </summary>
        /// <param name="building">Building to process</param>
        /// <param name="floorNum">Floor number to process</param>
        /// <param name="mapOffset">Offset to apply to points</param>
        private void ProcessFloorContainers(Building building, int floorNum, MapOffset mapOffset)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_CONTAINERS,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.ShortName}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");

            List<Coordinate> currentLineCoords = new List<Coordinate>();
            string roomName = string.Empty;

            foreach (string line in lines)
            {
                if (line.StartsWith("at point"))
                {
                    List<double> coordinates = line.FormatCoordinateLine().Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                        .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                    Coordinate coordinate = new Coordinate(coordinates[0] + mapOffset.OffX, coordinates[1] + mapOffset.OffY, (int)coordinates[2]);
                    currentLineCoords.Add(coordinate);
                }
                else if (line.StartsWith("#"))
                {
                    if (!line.StartsWith("#END"))
                    {
                        roomName = line.Substring(1);
                    }
                    if (currentLineCoords.Count > 0)
                    {
                        string separatorChar = building.ShortName.EndsWith(" ") ? "" : " ";
                        string roomLongName = $"{building.ShortName}{separatorChar}{roomName}";
                        _context.Coordinate.AddRange(currentLineCoords);
                        _context.SaveChanges();
                        Polyline polyline = new Polyline(currentLineCoords);
                        Room room = new Room(roomName, building.ShortName + " " + roomName, polyline.ToString(), floorNum, building.Id, roomName.StartsWith("C"));
                        _context.Room.Add(room);
                        _context.SaveChanges();
                        currentLineCoords = new List<Coordinate>();
                    }
                }
            }
        }

        /// <summary>
        /// Processes links from floorplan to external nodes.
        /// </summary>
        /// <param name="building">Building to process</param>
        /// <param name="floorNum">Floor number to process</param>
        /// <param name="mapOffset">Offset to apply to points</param>
        private void ProcessExternalFloorLinks(Building building, int floorNum, MapOffset mapOffset)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_EXTERNAL_LINKS,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.ShortName}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");
            Stack<string> matchHandles = new Stack<string>();
            List<Coordinate> currentLineCoords = new List<Coordinate>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("at point"))
                {
                    // Extract coordinates from line
                    lines[i] = lines[i].FormatCoordinateLine();
                    List<double> coordinates = lines[i].Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                    .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                    Coordinate coordinate;
                    if (matchHandles.Count > 0)
                    {
                        coordinate = new Coordinate(coordinates[0] + mapOffset.OffX, coordinates[1] + mapOffset.OffY, matchHandle: matchHandles.Pop());
                    }
                    else
                    {
                        coordinate = new Coordinate(coordinates[0] + mapOffset.OffX, coordinates[1] + mapOffset.OffY);
                    }

                    currentLineCoords.Add(coordinate);
                    _context.Coordinate.Add(coordinate);
                    _context.SaveChanges();
                }
                else if (lines[i].StartsWith("MatchHandle"))
                {
                    matchHandles.Push(lines[i].Split(" = ")[1]);
                }
                else if (lines[i].StartsWith("LWPOLYLINE") && currentLineCoords.Count > 0)
                {
                    CreateExternalLink(currentLineCoords);
                    currentLineCoords.Clear();
                }
            }
            CreateExternalLink(currentLineCoords);
        }

        /// <summary>
        /// Creates an external link for a given pair of coordinates.
        /// </summary>
        /// <param name="coordinates">Pair (as list) of coordinates to process</param>
        private void CreateExternalLink(List<Coordinate> coordinates)
        {
            // first coordinate is inside, second is out.
            // first should coincide with inside path node
            // second should coincide with sitewide path node

            Node outsideNode = _context.Node.First(n => n.MatchHandle.Equals(coordinates[0].MatchHandle));

            //Coordinate outsideNodeCoord = _context.Coordinate.AsEnumerable().First(c => c.getX() == coordinates[1].getX() && c.getY() == coordinates[1].getY());
            //Node? outsideNode = _context.Node.AsEnumerable().FirstOrDefault(n => n.getCoordinateId() == outsideNodeCoord.Id);

            Node insideNode = _context.Node.OrderBy(n => n.Id).Last(n => n.MatchHandle.Equals(coordinates[0].MatchHandle));

            NodeArc nodeArc = new NodeArc(insideNode, outsideNode, true, 0, NodeArcType.Path, false, false, false);
            _context.NodeArc.Add(nodeArc);
            _context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Added external link node arc");
        }

        /// <summary>
        /// Scale and offset coordinates for use in client app
        /// </summary>
        /// <returns>Standardised coordinates</returns>
        private List<Coordinate> StandardiseCoordinates()
        {
            List<Coordinate> coordinates = _context.Coordinate.ToList();
            StandardiseCoordinates(coordinates);
            _context.SaveChanges();
            return coordinates;
        }

        /// <summary>
        /// Computes cost (as Euclidian distance) for all node arcs
        /// </summary>
        /// <param name="coordinates">Coordinates (that correspond to the node arcs)</param>
        private void ComputeNodeArcCosts(List<Coordinate> coordinates)
        {
            List<NodeArc> nodeArcs = _context.NodeArc.ToList();
            foreach(NodeArc arc in nodeArcs)
            {
                arc.Node1 = _context.Node.First(n => n.Id == arc.Node1Id);
                arc.Node2 = _context.Node.First(n => n.Id == arc.Node2Id);
                arc.Node1.Coordinate = coordinates.First(c => c.Id == arc.Node1.CoordinateId);
                arc.Node2.Coordinate = coordinates.First(c => c.Id == arc.Node2.CoordinateId);

                arc.Cost = Coordinate.CalculateEucilidianDistance(arc.Node1.Coordinate, arc.Node2.Coordinate);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Standardises a list of coordinates by scaling and offsetting
        /// </summary>
        /// <param name="coords">Standardised coordinates</param>
        public static void StandardiseCoordinates(List<Coordinate> coords)
        {
            var minCoordX = coords.Min(c => c.X);
            var maxCoordX = coords.Max(c => c.X);
            var minCoordY = coords.Min(c => c.Y);
            var maxCoordY = coords.Max(c => c.Y);

            var rangeX = maxCoordX - minCoordX;
            var rangeY = maxCoordY - minCoordY;

            var scale = Math.Min(Constants.COORDINATE_RANGE / rangeX, Constants.COORDINATE_RANGE / rangeY);
            var offsetX = -1 * minCoordX;
            var offsetY = -1 * minCoordY;

            foreach (Coordinate coord in coords)
            {
                coord.X = (coord.X + offsetX) * scale;
                coord.Y = (coord.Y + offsetY) * scale;
            }

            maxCoordY = coords.Max(c => c.Y);

            foreach (Coordinate coord in coords)
            {
                coord.Y = Constants.COORDINATE_RANGE - coord.Y;
            }
        }

        /// <summary>
        /// Clears all rows from database tables
        /// </summary>
        private void ClearDatabase()
        {
            foreach (var entity in _context.GetType().GetProperties())
            {
                if (entity.PropertyType.IsGenericType &&
                    entity.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    IEnumerable<object>? dbSet = (IEnumerable<object>?)entity.GetValue(_context);
                    if (dbSet == null) continue;
                    foreach (var item in dbSet.ToList())
                    {
                        _context.Entry(item).State = EntityState.Deleted;
                    }
                }
            }

            _context.SaveChanges();
        }

    }
}
