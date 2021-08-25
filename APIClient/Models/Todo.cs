using Newtonsoft.Json;
using System;

namespace APIClient
{
    public class Todo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("due_on")]
        public DateTimeOffset DueOn { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
