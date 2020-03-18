using System;
using Rfid;

namespace Detego.RfidReader.Domain
{
    public interface IRfidReader
    {
        void Activate();

        void Deactivate();

        event EventHandler<TagSeenEventArgs> TagSeen;
    }
}