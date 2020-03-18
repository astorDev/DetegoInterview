using System;
using System.Threading;
using System.Threading.Tasks;
using Detego.RfidReader.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Detego.RfidReader.Tests.Units
{
    [TestClass]
    public class RfidReaderWrapper_Should
    {
        [TestMethod]
        public async Task SeeTagInAdequateTime()
        {
            var tagSeen = false;
            var wrapper = new RfidReaderWrapper(new Rfid.RfidReader());
            wrapper.TagSeen += (sender, args) => { tagSeen = true; };
            wrapper.Activate();
            
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var token = cancellationTokenSource.Token;
            
            while (!tagSeen)
            {
                await Task.Delay(5, token);
            }
        }
    }
}