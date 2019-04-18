using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Todo.Core
{
    public class DiscordMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("embeds")]
        public DiscordEmbed[] Embeds { get; set; }
    }

    public class DiscordEmbed
    {

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

}
