using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase
{
    public class MetadataItem
    {
        public ulong Id { get; }
        
        public ulong MovieId { get; }
   
        public string Title { get; }

        public string Language { get; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; }

        public ushort ReleaseYear { get; }

        [JsonConstructor]
        public MetadataItem(ulong id, ulong movieId, string title, string language, TimeSpan duration, ushort releaseYear) => (Id, MovieId, Title, Language, Duration, ReleaseYear) = (id, movieId, title, language, duration, releaseYear);

        // this is rougth as it doesn't handle CSV being different way round etc.... this also is not very defensive but will do here for the moment
        public static MetadataItem FromCSV(string csv)
        {
            // ".+?"|[^"]+?(?=,)|(?<=,)[^"]+


            var items = SplitCSV(csv).ToArray();

            // Would be nice to deconstruct ala javascript on array (id, movieId, title, language, duration, releaseYear) = items
            var id = ulong.Parse(items[0]);
            var movieId = ulong.Parse(items[1]);
            var title = items[2];
            var language = items[3];
            var duration = TimeSpan.Parse(items[4]);
            var releaseYear = ushort.Parse(items[5]);

            return new MetadataItem(id, movieId, title, language, duration, releaseYear);
        }

        // https://stackoverflow.com/questions/3776458/split-a-comma-separated-string-with-both-quoted-and-unquoted-strings/23888636
        private static IEnumerable<string> SplitCSV(string input)
        {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

            foreach (Match match in csvSplit.Matches(input))
            {
                yield return match.Value.TrimStart(',');
            }
        }

    }
}
