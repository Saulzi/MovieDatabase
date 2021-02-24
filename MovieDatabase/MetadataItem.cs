using System;
using System.Text.Json.Serialization;

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
        
    }
}
