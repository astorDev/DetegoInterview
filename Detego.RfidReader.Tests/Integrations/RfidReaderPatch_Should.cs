using System.Threading.Tasks;
using Detego.RfidReader.Protocol.Models;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Detego.RfidReader.Tests.Integrations
{
    [TestClass]
    public class RfidReaderPatch_Should
    {
        private readonly WebApplicationFactory factory = new WebApplicationFactory();

        [TestMethod]
        public async Task DontCallAnything_WhenEmptyChangePassed()
        {
            var client = this.factory.Create();
            await client.UpdateRfidReaderAsync(new RfidReaderChanges());
            
            A.CallTo(() => this.factory.FakeRfidReader.Activate()).MustNotHaveHappened();
            A.CallTo(() => this.factory.FakeRfidReader.Deactivate()).MustNotHaveHappened();
        }
        
        [TestMethod]
        public async Task CallActivate_WhenActiveStatusPassed()
        {
            var client = this.factory.Create();

            await client.UpdateRfidReaderAsync(new RfidReaderChanges
            {
                Status = RfidReaderStatus.Active
            });

            A.CallTo(() => this.factory.FakeRfidReader.Activate()).MustHaveHappened();
        }

        [TestMethod]
        public async Task CallDeactivate_WhenInactiveStatusPassed()
        {
            var client = this.factory.Create();

            await client.UpdateRfidReaderAsync(new RfidReaderChanges
            {
                Status = RfidReaderStatus.Inactive
            });

            A.CallTo(() => this.factory.FakeRfidReader.Deactivate()).MustHaveHappened();
        }
    }
}