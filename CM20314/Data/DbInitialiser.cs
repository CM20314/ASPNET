using CM20314.Helpers;
using CM20314.Models;
using CM20314.Models.Database;
using CM20314.Services;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;

namespace CM20314.Data
{
    public class DbInitialiser
    {
        private readonly FileService _fileService;
        private readonly ApplicationDbContext _context;

        public DbInitialiser(FileService fileService, DbContextOptions<ApplicationDbContext> contextOptions)
        {
            _fileService = fileService;
            _context = new ApplicationDbContext(contextOptions);
        }
        public void Initialise()
        {
            switch (Constants.STARTUP_MODE)
            {
                case Models.StartupMode.UseExistingDb:
                    break;
                case Models.StartupMode.GenerateDb:
                    GenerateDbFromFiles();
                    break;
            }
        }

        private void GenerateDbFromFiles()
        {
            ClearDatabase();

            // Open paths file, create all Nodes and NodeArcs, without duplicating Nodes.
            ProcessPaths(Constants.SourceFilePaths.PATHS_FILENAME, Constants.SourceFilePaths.FLOOR_OUTDOOR, 0, new MapOffset());
            // Open buildings file one by one, create all Polylines and entrance Nodes and NodeArcs.
            ProcessBuildingBoundariesAndEntrances();
            // Open buildings floors folders one by one:
            ProcessBuildings();
        }

        // Opens a path file, extracts coordinates and creates Nodes and NodeArcs.
        private void ProcessPaths(string filename, int floorNum, int buildingId, MapOffset mapOffset)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(filename);
            List<Node> currentLineNodes = new List<Node>();
            Stack<string> matchHandles = new Stack<string>();
            bool stepFree = true;

            for (int i = 0; i < 150 /*lines.Count*/; i++)
            {
                lines[i] = lines[i].FormatCoordinateLine();

                if (lines[i].StartsWith("at point"))
                {
                    List<double> coordinates = lines[i].Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                    .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                    Coordinate coordinate = new Coordinate(coordinates[0] + mapOffset.OffX, coordinates[1] + mapOffset.OffY);
                    _context.Coordinate.Add(coordinate);
                    _context.SaveChanges();

                    Coordinate? matchingCoordinate = _context.Coordinate.AsEnumerable().FirstOrDefault(c => c.getX() == coordinate.getX() && c.getY() == coordinate.getY());
                    Node? matchingNode = _context.Node.FirstOrDefault(n => n.Id == (matchingCoordinate != null ? matchingCoordinate.Id : -1));

                    if (matchingNode == null)
                    {
                        if (matchHandles.Count > 0)
                        {
                            matchingNode = new Node(floorNum, buildingId, coordinate.Id, matchHandles.Pop());
                        }
                        else
                        {
                            matchingNode = new Node(floorNum, buildingId, coordinate.Id);
                        }
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
                    CreateNodeArcsForPath(currentLineNodes, stepFree);
                }
                else if (lines[i].StartsWith("# NOT STEP FREE"))
                {
                    stepFree = false;
                }
                CreateNodeArcsForPath(currentLineNodes, stepFree);
            }
        }

        // Creates NodeArcs for a given list of nodes
        private void CreateNodeArcsForPath(List<Node> nodes, bool stepFree)
        {
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Coordinate? c1 = _context.Coordinate.First(c => c.Id == nodes[i].getCoordinateId());
                Coordinate? c2 = _context.Coordinate.First(c => c.Id == nodes[i + 1].getCoordinateId());
                NodeArc nodeArc = new NodeArc(
                    nodes[i], nodes[i + 1], stepFree,
                    Coordinate.CalculateEucilidianDistance(c1, c2),
                    NodeArcType.Path, false);
                _context.NodeArc.Add(nodeArc);
            }
            _context.SaveChanges();
        }

        // Processes all building boundaries (polylines) and entrances (i.e. links from sitewide path nodes to building nodes).
        private void ProcessBuildingBoundariesAndEntrances()
        {
            foreach (string buildingName in Constants.SourceFilePaths.BUILDING_NAMES)
            {
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
                                coordinate.setMatchHandle(matchHandles.Pop());
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

                _context.Coordinate.AddRange(polylineCoordinates);
                _context.SaveChanges();

                Polyline polyline = new Polyline(polylineCoordinates);
                List<int> floors = Constants.SourceFilePaths.BUILDING_FLOORS.ContainsKey(buildingName) ? Constants.SourceFilePaths.BUILDING_FLOORS[buildingName] : new List<int>();
                Building building = new Building(shortName, longName, polyline.ToString(), floors);
                _context.Building.Add(building);
                _context.SaveChanges();

                for (int i = 0; i < entranceCoordinates.Count; i += 2)
                {
                    Coordinate entranceNodeCoord = new Coordinate(entranceCoordinates[i + 1].getX(), entranceCoordinates[i + 1].getY(), entranceCoordinates[i + 1].getZ());
                    _context.Coordinate.Add(entranceNodeCoord);
                    _context.SaveChanges();

                    Node entranceNode = new Node(entranceNodeCoord.getZ(), building.Id, entranceNodeCoord.Id, entranceCoordinates[i].getMatchHandle());
                    _context.Node.Add(entranceNode);
                    _context.SaveChanges();

                    Coordinate? coord = _context.Coordinate.AsEnumerable().FirstOrDefault(c => c.getX() == entranceCoordinates[i].getX() && c.getY() == entranceCoordinates[i].getY());
                    if (coord != null)
                    {
                        Node? sitewidePathNode = _context.Node.AsEnumerable().FirstOrDefault(n => n.getCoordinateId() == coord.Id);
                        if (sitewidePathNode != null)
                        {
                            sitewidePathNode.setMatchHandle(entranceCoordinates[i].getMatchHandle());
                            _context.Node.Update(sitewidePathNode);
                            NodeArc entranceNodeArc = new NodeArc(sitewidePathNode, entranceNode, false,
                                Coordinate.CalculateEucilidianDistance(entranceNodeCoord, coord), NodeArcType.Path, false);
                            _context.NodeArc.Add(entranceNodeArc);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        // Floor-level processing for buildings
        private void ProcessBuildings()
        {
            foreach (string buildingName in Constants.SourceFilePaths.BUILDING_NAMES)
            {
                List<int> floors = Constants.SourceFilePaths.BUILDING_FLOORS.ContainsKey(buildingName) ?
                    Constants.SourceFilePaths.BUILDING_FLOORS[buildingName] : new List<int>();
                Building building = _context.Building.AsEnumerable().First(b => b.getShortName().Equals(buildingName));

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

        // Processes floor-specific files
        private void ProcessFloor(Building building, int floor)
        {
            // SHOULD FIRST FIND MULTIPLIER AND OFFSET
            MapOffset mapOffset = ComputeMapOffset(building, floor, 1);

            //      Create floor, create polyline boundary.
            ProcessFloorBoundary(building, floor, mapOffset);
            //      Open paths, create all Nodes and NodeArcs, without duplicating Nodes.
            ProcessPaths($"{building.getShortName()}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floor}\\{Constants.SourceFilePaths.FLOOR_FILENAME_PATHS}", floor, building.Id, mapOffset);
            //      Open containers, create Rooms and corridors, for rooms
            ProcessFloorContainers(building, floor, mapOffset);
            //      Open internal links (between floors), create Nodes and add flag
            //      Open external links, create NodeArcs to connect to outside nodes.
            ProcessExternalFloorLinks(building, floor, mapOffset);
        }

        // Computes x and y offset by determining a reference point between the two scales
        private MapOffset ComputeMapOffset(Building building, int floorNum, double scale)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_EXTERNAL_LINKS,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.getShortName()}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");
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

            Node matchingNode = _context.Node.AsEnumerable().First(n => n.getMatchHandle() == currentLineCoords[1].getMatchHandle());

            Coordinate n1coordinate = _context.Coordinate.First(c => c.Id == matchingNode.getCoordinateId());

            return CalculateOffset(n1coordinate, currentLineCoords[1], scale);
        }

        // Computes offset between two coordinates.
        private MapOffset CalculateOffset(Coordinate src, Coordinate dst, double scale)
        {
            double offsetX = dst.getX() - src.getX();
            double offsetY = dst.getY() - src.getY();

            return new MapOffset(offsetX, offsetY, scale: scale);
        }

        // Processes polyline boundary for a floor.
        private void ProcessFloorBoundary(Building building, int floorNum, MapOffset mapOffset)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_BOUNDARY,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.getShortName()}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");
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

        // Processes polyline boundaries for containers (rooms) within a floor
        private void ProcessFloorContainers(Building building, int floorNum, MapOffset mapOffset)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_CONTAINERS,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.getShortName()}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");

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
                        string separatorChar = building.getShortName().EndsWith(" ") ? "" : " ";
                        string roomLongName = $"{building.getShortName()}{separatorChar}{roomName}";
                        _context.Coordinate.AddRange(currentLineCoords);
                        _context.SaveChanges();
                        Polyline polyline = new Polyline(currentLineCoords);
                        Room room = new Room(roomName, building.getShortName(), polyline.ToString(), floorNum, building.Id, roomName.StartsWith("C"));
                        _context.Room.Add(room);
                        _context.SaveChanges();
                        currentLineCoords = new List<Coordinate>();
                    }
                }
            }
        }

        // Processes links from floorplan to external nodes.
        private void ProcessExternalFloorLinks(Building building, int floorNum, MapOffset mapOffset)
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.FLOOR_FILENAME_EXTERNAL_LINKS,
                root: Constants.SourceFilePaths.ROOT + $"\\{building.getShortName()}{Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR}{floorNum}");
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

        // Creates an external link for a given pair of coordinates.
        private void CreateExternalLink(List<Coordinate> coordinates)
        {
            // first coordinate is inside, second is out.
            // first should coincide with inside path node
            // second should coincide with sitewide path node

            Node outsideNode = _context.Node.AsEnumerable().First(n => n.getMatchHandle() == coordinates[0].getMatchHandle());

            //Coordinate outsideNodeCoord = _context.Coordinate.AsEnumerable().First(c => c.getX() == coordinates[1].getX() && c.getY() == coordinates[1].getY());
            //Node? outsideNode = _context.Node.AsEnumerable().FirstOrDefault(n => n.getCoordinateId() == outsideNodeCoord.Id);

            Node insideNode = _context.Node.AsEnumerable().Last(n => n.getMatchHandle() == coordinates[0].getMatchHandle());

            double cost = Coordinate.CalculateEucilidianDistance(
            _context.Coordinate.First(c => c.Id == insideNode.getCoordinateId()),
            _context.Coordinate.First(c => c.Id == outsideNode.getCoordinateId())
            );
            NodeArc nodeArc = new NodeArc(insideNode, outsideNode, false, cost, NodeArcType.Path, false);
            _context.NodeArc.Add(nodeArc);
            _context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Added external link node arc");
        }

        // Clears all rows from database tables.
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
