using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Detego.RfidReader.Protocol
{
    public class WebApiClient
    {
        public HttpClient Client { get; }

        public WebApiClient(HttpClient client)
        {
            this.Client = client;
        }

        protected async Task<T> ReadAsync<T>(HttpResponseMessage responseMessage)
        {
            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            await this.OnResponseBodyReadAsync(responseBody);

            if (!responseMessage.IsSuccessStatusCode)
            {
                await this.OnUnsuccessfulStatusCodeAsync(responseMessage);
            }

            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        protected async Task ReadAsync(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                await this.OnUnsuccessfulStatusCodeAsync(responseMessage);
            }
        }

        protected virtual async Task OnUnsuccessfulStatusCodeAsync(HttpResponseMessage message)
        {
            message.EnsureSuccessStatusCode();
        }

        protected virtual async Task OnResponseBodyReadAsync(string responseBody)
        {

        }
    }
}