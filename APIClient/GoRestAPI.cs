using RestSharp;
using System;

namespace APIClient
{
    public class GoRestAPI
    {   
        private const string BASE_URL = "https://gorest.co.in/";

        public RestClient CreateClient(Uri uri = null)
        {
            RestClient client;

            if (uri == null)
            {
                client = new RestClient(BASE_URL);
            }
            else
            {
                client = new RestClient(uri);
            }

            return client;
        }
       
        public RestRequest CreateGetRequest()
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            return request;
        }

        public RestRequest CreateUpdateRequest(Method method, string payload, string accessToken)
        {
            var request = new RestRequest(method);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            request.AddParameter("application/json", payload, ParameterType.RequestBody);

            return request;
        }

        public RestRequest CreateDeleteRequest(string accessToken)
        {
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));

            return request;
        }

        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }
    }
}
