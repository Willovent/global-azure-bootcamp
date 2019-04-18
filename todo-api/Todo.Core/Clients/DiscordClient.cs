
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Clients
{
    public class DiscordClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _webhookUrl;

        public DiscordClient(string webhookUrl)
        {
            _httpClient = new HttpClient();
            _webhookUrl = webhookUrl;
        }

        public async Task<bool> SendMessageAsync(DiscordMessage message)
        {
            var json = JsonConvert.SerializeObject(message);
            var strContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await _httpClient.PostAsync(_webhookUrl, strContent);
            return result.IsSuccessStatusCode;
        }
    }
}
