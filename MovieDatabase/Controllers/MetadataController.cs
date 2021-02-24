using Microsoft.AspNetCore.Mvc;

namespace MovieDatabase.Controllers
{
    public class MetadataController : ControllerBase
    {
        [HttpPost("metadata")]
        public IActionResult PostMetadata()
        {
            return Ok();
        }

        [HttpGet("metadata/{id}")]
        public IActionResult GetMetadata(string id)
        {
            return Ok();
        }
    }
}
