using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase
{
    public class MetadataRepository
    {
        public List<MetadataItem> Movies { get; } = new List<MetadataItem>();
    }
}
