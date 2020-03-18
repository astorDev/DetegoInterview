using System;
using System.Threading;

namespace Detego.RfidReader.Domain
{
    public class SeeingsStatistic
    {
        private int times;

        public int Times => this.times;

        public DateTime FirstAt { get; set; }
        
        public DateTime LastAt { get; set; }

        public void IncrementTimes()
        {
            Interlocked.Increment(ref this.times);
        }
    }
}