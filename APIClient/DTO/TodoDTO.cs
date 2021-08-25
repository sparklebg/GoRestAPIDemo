using Newtonsoft.Json;

namespace APIClient
{
    public class TodoDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public Todo Todo { get; set; }
    }
}
