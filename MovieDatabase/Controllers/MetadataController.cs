using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace MovieDatabase.Controllers
{
    public class MetadataController : ControllerBase
    {
        private readonly MetadataRepository _metadataRepository;

        public MetadataController(MetadataRepository metadataRepository)
        {
            _metadataRepository = metadataRepository ?? throw new ArgumentNullException(nameof(metadataRepository));
        }

        [HttpPost("metadata")]
        public IActionResult PostMetadata([FromBody]MetadataItem metadataItem)
        {
            _metadataRepository.Movies.Add(metadataItem);
            return Ok();
        }

        [HttpGet("metadata/{id}")]
        public IActionResult GetMetadata(ulong id)
        {
            var movie = _metadataRepository.Movies.FirstOrDefault(f => f.MovieId == id);
            return movie != null ? (IActionResult)Ok(movie) : NotFound();
        }
    }
}
