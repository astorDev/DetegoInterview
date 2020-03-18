using System.Linq;
using System.Threading.Tasks;
using Detego.RfidReader.Domain;
using Detego.RfidReader.Protocol;
using Detego.RfidReader.Protocol.Models;
using Microsoft.AspNetCore.Mvc;
using Rfid;

namespace Detego.RfidReader.WebApi.Controllers
{
    [ApiController]
    [Route(Uris.RfidReader)]
    public class RfidReaderController
    {
        public Tags Tags { get; }

        public RfidReaderController(Tags tags)
        {
            this.Tags = tags;
        }

        [HttpPatch]
        public void Update(RfidReaderChanges changes)
        {
            switch (changes.Status)
            {
                case RfidReaderStatus.Active:
                    this.Tags.Reader.Activate();
                    break;
                case RfidReaderStatus.Inactive:
                    this.Tags.Reader.Deactivate();
                    break;
            }
        }

        [HttpGet(Uris.SeenTags)]
        public SeenTags GetSeenTags()
        {
            var tags = this.Tags.Statistics.Select(s => new SeenTags.SeenTag
            {
                Id = s.Key,
                SeenTimes = s.Value.Times,
                FirstSeen = s.Value.FirstAt,
                LastSeen = s.Value.LastAt
            }).ToArray();

            return new SeenTags
            {
                Count = tags.Length,
                Tags = tags
            };
        }
    }
}