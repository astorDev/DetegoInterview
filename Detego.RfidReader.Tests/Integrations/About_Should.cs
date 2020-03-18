using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Detego.RfidReader.Tests.Integrations
{
    [TestClass]
    public class About_Should
    {
        [TestMethod]
        public async Task ReturnValidMetadata()
        {
            var client = new WebApplicationFactory().Create();

            var about = await client.GetAboutAsync();
            
            Assert.AreEqual("Detego.RfidReader - АПИ для вакансии Detego", about.Description);
        }
    }
}