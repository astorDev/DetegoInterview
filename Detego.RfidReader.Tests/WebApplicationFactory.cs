using System;
using System.Net.Http;
using System.Threading.Tasks;
using Detego.RfidReader.Domain;
using Detego.RfidReader.Protocol;
using Detego.RfidReader.WebApi;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Detego.RfidReader.Tests
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        public readonly IRfidReader FakeRfidReader = A.Fake<IRfidReader>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(s => { s.AddSingleton(this.FakeRfidReader); });
        }

        public RfidReaderClient Create()
        {
            return new TestRfidReaderClient(this.CreateClient());
        }
        
        private class TestRfidReaderClient : RfidReaderClient
        {
            public TestRfidReaderClient(HttpClient client) : base(client)
            {
            }

            protected override async Task OnResponseBodyReadAsync(string responseBody)
            {
                Console.WriteLine(responseBody);
            }
        }
    }
}