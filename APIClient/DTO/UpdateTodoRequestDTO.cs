using Newtonsoft.Json;
using System;

namespace APIClient
{
    public class UpdateTodoRequestDTO
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("due_on")]
        public DateTimeOffset DueOn { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
