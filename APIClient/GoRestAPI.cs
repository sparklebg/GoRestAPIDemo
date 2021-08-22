using Newtonsoft.Json;
using RestSharp;

namespace APIClient
{
    public class GoRestAPI<T>
    {
        
        public RestClient client;
        public RestRequest request;
        public const string BASE_URL = "https://gorest.co.in/";

        public RestClient CreateClient()
        {
            client = new RestClient(BASE_URL);

            return client;
        }

        public RestRequest CreatePostRequest(string payload, string token)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddParameter("application/json", payload, ParameterType.RequestBody);            

            return request;
        }

        public RestRequest CreatePutRequest(string payload, string token)
        {
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddParameter("application/json", payload, ParameterType.RequestBody);

            return request;
        }

        public RestRequest CreatePatchRequest(string payload, string token)
        {
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddParameter("application/json", payload, ParameterType.RequestBody);

            return request;
        }

        public RestRequest CreateGetRequest()
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            return request;
        }

        public RestRequest CreateDeleteRequest(string token)
        {
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));

            return request;
        }

        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }

        public DTO GetContent<DTO>(IRestResponse response)
        {
            var content = response.Content;
            DTO dtoObject = JsonConvert.DeserializeObject<DTO>(content);

            return dtoObject;
        }

        public string Serialize(dynamic content)
        {
            string serializeObject = JsonConvert.SerializeObject(content, Formatting.Indented);
            return serializeObject;
        }
    }
}
