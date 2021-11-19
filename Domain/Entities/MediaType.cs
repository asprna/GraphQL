using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public class MediaType
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        public long MediaTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
