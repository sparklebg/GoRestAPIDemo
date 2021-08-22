using RestSharp;

namespace APIClient
{
    public class Client<T>
    {
        
        private const string TOKEN = "d1be9b47242fdccce807adc6d96fccf9ee03f4c12c5d1afcce85f3cb3a267d89";

        public UsersDTO GetListOfUsers()
        {
            var api = new GoRestAPI<UsersDTO>();
            var client = api.CreateClient();
            var request = api.CreateGetRequest();
            request.Resource = "public/v1/users";
            var response = api.GetResponse(client, request);
            UsersDTO content = api.GetContent<UsersDTO>(response);

            return content;
        }

        public long GetNumberOfUsers()
        {   
            UsersDTO content = this.GetListOfUsers();

            return content.Meta.Pagination.Total;
        }

        public long GetNumberOfPages()
        {
            UsersDTO content = this.GetListOfUsers();

            return content.Meta.Pagination.Pages;
        }

        public long GetPageNumber()
        {
            UsersDTO content = this.GetListOfUsers();

            return content.Meta.Pagination.Page;
        }

        public IRestResponse GetUserById(long id)
        {
            var api = new GoRestAPI<UsersDTO>();
            var client = api.CreateClient();
            var request = api.CreateGetRequest();
            request.Resource = "public/v1/users/{id}";
            request.AddUrlSegment("id", id);
            var response = api.GetResponse(client, request);

            return response;
        }

        public IRestResponse GetUserByEmail(string email)
        {
            var api = new GoRestAPI<UsersDTO>();
            var client = api.CreateClient();
            var request = api.CreateGetRequest();
            request.Resource = "public/v1/users?email={email}";
            request.AddUrlSegment("email", email);
            var response = api.GetResponse(client, request);

            return response;
        }

        public IRestResponse CreateUser(dynamic payload)
        {
            var api = new GoRestAPI<UpdateUserRequestDTO>();
            var url = api.CreateClient();
            var jsonRequest = api.Serialize(payload);
            var request = api.CreatePostRequest(jsonRequest, TOKEN);
            request.Resource = "public/v1/users";
            var response = api.GetResponse(url, request);
            
            return response;
        }

        public IRestResponse UpdateUserPartially(long userId, dynamic payload)
        {
            var api = new GoRestAPI<UpdateUserRequestDTO>();
            var url = api.CreateClient();
            var jsonRequest = api.Serialize(payload);
            RestRequest request = api.CreatePatchRequest(jsonRequest, TOKEN);
            request.Resource = "public/v1/users/{userId}";
            request.AddUrlSegment("userId", userId);
            var response = api.GetResponse(url, request);

            return response;
        }

        public IRestResponse UpdateUser(long userId, dynamic payload)
        {
            var api = new GoRestAPI<UpdateUserRequestDTO>();
            var url = api.CreateClient();
            var jsonRequest = api.Serialize(payload);
            RestRequest request = api.CreatePutRequest(jsonRequest, TOKEN);
            request.Resource = "public/v1/users/{userId}";
            request.AddUrlSegment("userId", userId);
            var response = api.GetResponse(url, request);

            return response;
        }

        public IRestResponse DeleteUser(long userId)
        {
            var api = new GoRestAPI<UpdateUserRequestDTO>();
            var url = api.CreateClient();
            RestRequest request = api.CreateDeleteRequest(TOKEN);
            request.Resource = "public/v1/users/{userId}";
            request.AddUrlSegment("userId", userId);
            var response = api.GetResponse(url, request);

            return response;
        }
    }
}
