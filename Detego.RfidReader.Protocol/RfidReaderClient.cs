using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Detego.RfidReader.Protocol.Models;
using Newtonsoft.Json;

namespace Detego.RfidReader.Protocol
{
    public class RfidReaderClient : WebApiClient
    {
        public RfidReaderClient(HttpClient client) : base(client)
        {
        }

        public async Task<About> GetAboutAsync()
        {
            var response = await this.Client.GetAsync(Uris.About);
            return await this.ReadAsync<About>(response);
        }

        public async Task<SeenTags> GetSeenTagsAsync()
        {
            var response = await this.Client.GetAsync($"{Uris.RfidReader}/{Uris.SeenTags}");
            return await this.ReadAsync<SeenTags>(response);
        }
        
        public async Task UpdateRfidReaderAsync(RfidReaderChanges changes)
        {
            var content = new StringContent(JsonConvert.SerializeObject(changes));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            
            var response = await this.Client.PatchAsync(Uris.RfidReader, content);
            await this.ReadAsync(response);
        }
    }
}