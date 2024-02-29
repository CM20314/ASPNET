using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;
using CM20314.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CM20314.Controllers
{
    // Home controller responsible for all calls to the API
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoutingService _routingService;
        private readonly PathfindingService _pathfindingService;
        private readonly MapDataService _mapDataService;
        private readonly FileService _fileService;
        private readonly DbInitialiser _dbInitialiser;
        public HomeController(
            ApplicationDbContext context,
            RoutingService routingService,
            PathfindingService pathfindingService,
            FileService fileService,
            MapDataService mapDataService,
            DbInitialiser dbInitialiser)
        {
            // Acquire services via dependency injection
            _context = context;
            _routingService = routingService;
            _mapDataService = mapDataService;
            _pathfindingService = pathfindingService;
            _fileService = fileService;
            _dbInitialiser = dbInitialiser;
        }

        [HttpPost("init")]
        public void Initialise()
        {
            _dbInitialiser.Initialise(_context, _fileService);
            _mapDataService.Initialise(_context);
            _routingService.Initialise(_pathfindingService, _mapDataService, _context);
        }

        // POST api/directions
        [HttpPost("directions")]
        public RouteResponseData GetDirections([FromBody] RouteRequestData requestData)
        {
            // Make call to RoutingService 
            return _routingService.ComputeRoute(requestData);
        }

        // GET api/map
        [HttpGet("map")]
        public MapResponseData GetMap(int buildingId = 0)
        {
            // Make call to MapDataService
            return _mapDataService.GetMapData(buildingId);
        }

        // GET api/search?query=SEARCH_QUERY
        [HttpGet("search")]
        public SearchResponseData SearchContainers(string query)
        {
            return new SearchResponseData()
            {
                Results = _mapDataService.SearchContainers(query)
            };
        }
    }
}
