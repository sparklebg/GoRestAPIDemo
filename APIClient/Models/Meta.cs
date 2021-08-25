using Newtonsoft.Json;

namespace APIClient
{
    public partial class Meta
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
