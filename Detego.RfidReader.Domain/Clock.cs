using System;

namespace Detego.RfidReader.Domain
{
    public static class Clock
    {
        private static DateTime? time;
        public static DateTime Time
        {
            get => time ?? DateTime.Now;
            set => time = value;
        }
    }
}