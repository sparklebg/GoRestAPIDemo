using Newtonsoft.Json;

namespace APIClient
{
    public partial class Pagination
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }
}
