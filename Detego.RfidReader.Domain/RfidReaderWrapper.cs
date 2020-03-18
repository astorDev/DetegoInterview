using System;
using Rfid;

namespace Detego.RfidReader.Domain
{
    public class RfidReaderWrapper : IRfidReader
    {
        public Rfid.RfidReader Reader { get; }

        public RfidReaderWrapper(Rfid.RfidReader reader)
        {
            this.Reader = reader;
            this.Reader.TagSeen += (sender, args) => { this.TagSeen?.Invoke(this, args); };
        }
        
        public void Activate()
        {
            this.Reader.Activate();
        }

        public void Deactivate()
        {
            this.Reader.Deactivate();
        }

        public event EventHandler<TagSeenEventArgs> TagSeen;
    }
}