using Newtonsoft.Json;

namespace APIClient
{
    public class TodosDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public Todo[] Todos { get; set; }
    }
}
