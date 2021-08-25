using Newtonsoft.Json;
using System;

namespace APIClient
{
    public partial class Links
    {
        [JsonProperty("previous")]
        public Uri Previous { get; set; }

        [JsonProperty("current")]
        public Uri Current { get; set; }

        [JsonProperty("next")]
        public Uri Next { get; set; }
    }
}
