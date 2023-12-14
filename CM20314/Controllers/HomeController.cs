using CM20314.Models;
using CM20314.Models.Database;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CM20314.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET api/directions
        [HttpGet("directions")]
        public RouteResponseData GetDirections([FromBody] RouteRequestData requestData)
        {
            // IMPLEMENT: RouteRequestData and RouteResponseData classes
            // IMPLEMENT: Call RoutingService.ComputeRoute(requestData) and return a response
            return new RouteResponseData();
        }

        // GET api/map
        [HttpGet("map")]
        public MapResponseData GetMap()
        {
            // IMPLEMENT: Call MapDataService.GetMapData()
            return new MapResponseData();
        }

        // GET api/search?query=SEARCH_QUERY
        [HttpGet("search")]
        public List<Container> SearchContainers(string query)
        {
            // IMPLEMENT: Call MapDataService.SearchContainers()
            return new List<Container>();
        }
    }
}
