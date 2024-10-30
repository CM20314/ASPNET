using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;
using CM20314.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CM20314.Controllers
{
    /// <summary>
    /// Home controller responsible for all calls to the API
    /// </summary>
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoutingService _routingService;
        private readonly MapDataService _mapDataService;
        private readonly DbInitialiser _dbInitialiser;
        public HomeController(
            ApplicationDbContext context,
            RoutingService routingService,
            MapDataService mapDataService,
            DbInitialiser dbInitialiser)
        {
            // Acquire services via dependency injection
            _context = context;
            _routingService = routingService;
            _mapDataService = mapDataService;
            _dbInitialiser = dbInitialiser;
        }

        // For testing
        //[HttpGet("init")]
        //public void Initialise()
        //{
        //    _dbInitialiser.SplitArcsAndConfigureJunctions(_context);
        //}

        /// <summary>
        /// POST api/directions
        /// </summary>
        /// <param name="requestData">Route request data (e.g. start, end)</param>
        /// <returns>RouteResponseData object containing directions</returns>
        [HttpPost("directions")]
        public RouteResponseData GetDirections([FromBody] RouteRequestData requestData)
        {
            // Make call to RoutingService 
            return _routingService.ComputeRoute(requestData);
        }

        /// <summary>
        /// GET api/map
        /// </summary>
        /// <param name="buildingId">Building to view (default = all buildings)</param>
        /// <returns>Map data</returns>
        [HttpGet("map")]
        public MapResponseData GetMap(int buildingId = 0)
        {
            // Make call to MapDataService
            return _mapDataService.GetMapData(buildingId);
        }

        /// <summary>
        /// GET api/search?query=SEARCH_QUERY
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>Search results</returns>
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
