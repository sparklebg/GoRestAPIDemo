using Newtonsoft.Json;
using RestSharp;
using System.IO;

namespace APIClient
{
    public static class HandleContent
    {
        public static T GetContent<T>(IRestResponse response)
        {
            var content = response.Content;

            return JsonConvert.DeserializeObject<T>(content);
        }
        
        public static string Serialize(dynamic content)
        {
            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }

        public static T ParseJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
        }
    }
}
