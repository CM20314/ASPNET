using CM20314.Models;
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
        public List<object> GetDirections([FromBody] RouteRequestData requestData)
        {
            return new List<object>();
        }
    }
}
