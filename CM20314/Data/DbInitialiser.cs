using CM20314.Models.Database;
using CM20314.Services;
using Microsoft.EntityFrameworkCore;
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
            ProcessSitewidePaths();
            // Open buildings file one by one, create all Polylines and entrance Nodes and NodeArcs.
            ProcessBuildingBoundariesAndEntrances();
            // Open buildings floors folders one by one:
            //      Create floor, create polyline boundary.
            //      Open paths, create all Nodes and NodeArcs, without duplicating Nodes.
            //      Open containers, create Rooms and corridors, for rooms 
            //      Open internal links (between floors), create Nodes and add flag
            //      Open external links, create NodeArcs to connect to outside nodes.
        }

        private void ProcessSitewidePaths()
        {
            List<string> lines = _fileService.ReadLinesFromFileWithName(Constants.SourceFilePaths.PATHS_FILENAME);
            List<Node> currentLineNodes = new List<Node>();
            bool stepFree = true;

            for (int i = 0; i < 100 /*lines.Count*/; i++)
            {
                lines[i] = lines[i].Replace("Z=    ", "Z=");

                if (lines[i].StartsWith("at point"))
                {
                    // Extract coordinates from line
                    List<double> coordinates = lines[i].Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                    .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                    Coordinate coordinate = new Coordinate(coordinates[0], coordinates[1]);
                    _context.Add(coordinate);
                    _context.SaveChanges();

                    Coordinate? matchingCoordinate = _context.Coordinate.FirstOrDefault(c => c.Equals(coordinate));
                    Node? matchingNode = _context.Node.FirstOrDefault(n => n.Id == (matchingCoordinate != null ? matchingCoordinate.Id : -1));

                    if (matchingNode == null)
                    {
                        matchingNode = new Node(Constants.SourceFilePaths.FLOOR_OUTDOOR, 0, coordinate.Id);
                        _context.Add(matchingNode);
                        _context.SaveChanges();
                    }

                    currentLineNodes.Add(matchingNode);
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

        private void CreateNodeArcsForPath(List<Node> nodes, bool stepFree)
        {
            for(int i = 0; i < nodes.Count - 1; i++)
            {
                Coordinate? c1 = _context.Coordinate.First(c => c.Id == nodes[i].getCoordinateID());
                Coordinate? c2 = _context.Coordinate.First(c => c.Id == nodes[i + 1].getCoordinateID());
                NodeArc nodeArc = new NodeArc(
                    nodes[i], nodes[i + 1], stepFree,
                    Coordinate.CalculateEucilidianDistance(c1, c2),
                    NodeArcType.Path, false);
                _context.NodeArc.Add(nodeArc);
            }
            _context.SaveChanges();
        }
        private void ProcessBuildingBoundariesAndEntrances()
        {
            foreach (string buildingName in Constants.SourceFilePaths.BUILDING_NAMES)
            {
                List<string> lines = _fileService.ReadLinesFromFileWithName(buildingName);
                string shortName = lines[0];
                string longName = lines[1];

                bool pointsAreEntranceNodes = false;

                List<Coordinate> polylineCoordinates = new List<Coordinate>();
                List<Coordinate> entranceCoordinates = new List<Coordinate>(); // first - on sitewide path, second - inside building

                for (int i = 0; i < lines.Count; i++)
                {
                    lines[i] = lines[i].Replace("Z=    ", "Z=");

                    if (lines[i].StartsWith("at point"))
                    {
                        // Extract coordinates
                        List<double> coordinates = lines[i].Split(" ").Where(e => !string.IsNullOrWhiteSpace(e)).Reverse().Take(3).Reverse()
                                        .Select(coord => double.Parse(coord.Split('=')[1])).ToList();

                        Coordinate coordinate = new Coordinate(coordinates[0], coordinates[1]);
                        if (!pointsAreEntranceNodes)
                        {
                            polylineCoordinates.Add(coordinate);
                        }
                        else
                        {
                            entranceCoordinates.Add(coordinate);
                        }
                    }
                    else if (lines[i].StartsWith("#ENTRANCES"))
                    {
                        pointsAreEntranceNodes = true;   
                    }
                }

                _context.AddRange(polylineCoordinates);
                _context.SaveChanges();
                Polyline polyline = new Polyline(polylineCoordinates.Select(pc => pc.Id));
                List<int> floors = Constants.SourceFilePaths.BUILDING_FLOORS.ContainsKey(buildingName) ? Constants.SourceFilePaths.BUILDING_FLOORS[buildingName] : new List<int>();
                Building building = new Building(shortName, longName, polyline, floors);
                _context.Add(building);

                Coordinate entranceNodeCoord = new Coordinate(entranceCoordinates[1].getX(), entranceCoordinates[1].getY());
                _context.Add(entranceNodeCoord);

                _context.SaveChanges();

                // NEEDS FLOOR SPECIFICATION - NOT HARDCODED AS BELOW
                Node entranceNode = new Node(2, building.Id, entranceNodeCoord.Id);
                _context.Add(entranceNode);
                _context.SaveChanges();

                Coordinate? coord = _context.Coordinate.FirstOrDefault(c => c.Equals(entranceCoordinates[0]));
                if(coord != null)
                {
                    Node? sitewidePathNode = _context.Node.FirstOrDefault(n => n.getCoordinateID() == coord.Id);
                    if(sitewidePathNode != null)
                    {
                        _context.Add(sitewidePathNode);
                        _context.SaveChanges();

                        NodeArc entranceNodeArc = new NodeArc(sitewidePathNode, entranceNode, false,
                            Coordinate.CalculateEucilidianDistance(entranceNodeCoord, coord), NodeArcType.Path, false);
                        _context.Add(entranceNodeArc);
                        _context.SaveChanges();
                    }
                }
            }
        }
        private void ProcessBuilding()
        {

        }
        private void ProcessFloor()
        {

        }

        // Inside ApplicationDbContext.cs or another appropriate class
        private void ClearDatabase()
        {
            // Assuming you have a DbSet for each entity in your database
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
