using System.Collections.Concurrent;
using System.Collections.Generic;
using Rfid;

namespace Detego.RfidReader.Domain
{
    public class Tags
    {
        public IRfidReader Reader { get; }

        private readonly ConcurrentDictionary<string, SeeingsStatistic> tagsSeeings = new ConcurrentDictionary<string, SeeingsStatistic>();
        public IReadOnlyDictionary<string, SeeingsStatistic> Statistics => this.tagsSeeings;

        public Tags(IRfidReader reader)
        {
            this.Reader = reader;
            this.Reader.TagSeen += ReaderOnTagSeen;
        }

        private void ReaderOnTagSeen(object sender, TagSeenEventArgs e)
        {
            if (!this.tagsSeeings.TryGetValue(e.Identifier, out var statistic))
            {
                statistic = new SeeingsStatistic
                {
                    FirstAt = Clock.Time
                };

                if (!this.tagsSeeings.TryAdd(e.Identifier, statistic))
                {
                    this.ReaderOnTagSeen(sender, e);
                }
            }

            statistic.IncrementTimes();
            statistic.LastAt = Clock.Time;
        }
    }
}