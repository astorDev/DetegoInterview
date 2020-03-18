using System;
using System.Collections.Generic;

namespace Detego.RfidReader.Protocol.Models
{
    public class SeenTags
    {
        public int Count { get; set; }

        public IEnumerable<SeenTag> Tags { get; set; }
        
        public class SeenTag
        {
            public string Id { get; set; }
            
            public int SeenTimes { get; set; }
            
            public DateTime FirstSeen { get; set; }
            
            public DateTime LastSeen { get; set; }
        }
    }
}