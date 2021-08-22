using Newtonsoft.Json;

namespace APIClient
{
    public class UserDTO
    {
        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("data")]
        public User User { get; set; }
    }
}
