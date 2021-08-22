using Newtonsoft.Json;

namespace APIClient
{
    public class UsersDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public User[] Users { get; set; }
    }
}
