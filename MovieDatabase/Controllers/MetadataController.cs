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
            // We are interested in the count, 
            var movie = _metadataRepository.GetByMovieId(id).ToArray();

            // Return movie 404 when not found.. 
            return movie.Length > 0 ? (IActionResult)Ok(movie) : NotFound();
        }
    }
}
