using Newtonsoft.Json;
using RestSharp;

namespace APIClient
{
    public static class HandleContent
    {
        public static T GetContent<T>(IRestResponse response)
        {
            var content = response.Content;

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
