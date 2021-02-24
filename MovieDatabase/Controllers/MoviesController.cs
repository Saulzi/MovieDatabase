using Microsoft.AspNetCore.Mvc;

namespace MovieDatabase.Controllers
{
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok();
        }
    }
}
