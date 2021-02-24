using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase
{
    public class MetadataRepository
    {
        public List<MetadataItem> Movies { get; } = new List<MetadataItem>();

        /// <summary>
        /// Returns all of the movies for the specified id doing the language filter,,, should this live here.. maybe not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MetadataItem> GetByMovieId(ulong id)
        {
            // filter all of the movied by id
            var byId = Movies.Where(f => f.MovieId == id);

            // where valid looking at CSV are there any? may have duff data coming in??
            var whereValid = byId.Where(f => f.Duration > TimeSpan.Zero &&
                                             f.ReleaseYear > 0 &&
                                             !string.IsNullOrEmpty(f.Title) &&
                                             !string.IsNullOrEmpty(f.Language));

            // We only want the first of each language so group and then select item
            var groupedByLang = whereValid.GroupBy(f => f.Language, (key, item) => item.OrderBy(f => f.MovieId).Last());

            // Then we want to order by the language
            return groupedByLang.OrderBy(f => f.Language);

        }


    }
}
