﻿using CM20314.Data;
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
        private readonly RoutingService _routingService;
        private readonly MapDataService _mapDataService;
        public HomeController(
            RoutingService routingService,
            MapDataService mapDataService)
        {
            // Acquire services via dependency injection
            _routingService = routingService;
            _mapDataService = mapDataService;
        }

        // GET api/directions
        [HttpGet("directions")]
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
        public List<Container> SearchContainers(string query)
        {
            return _mapDataService.SearchContainers(query);
        }
    }
}
