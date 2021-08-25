using RestSharp;
using System;

namespace APIClient
{
    public class Client<T>
    {        
        private const string ACCESS_TOKEN = "598cfd288d116f472dbe8d69bba6eb1514f3373489e0aa71fc4ffc544584c652";

        public IRestResponse GetUserById(long userId)
        {
            var response = GetGETResponse(
                                resource: APIResource.GetUserById,
                                urlSegment: UrlSegment.UserId,
                                value: userId);

            return response;
        }

        public IRestResponse GetUserByEmail(string email)
        {
            var response = GetGETResponse(
                                resource: APIResource.GetUserByEmail,
                                urlSegment: UrlSegment.UserEmail,
                                value: email);

            return response;
        }

        public IRestResponse GetTodosByTitle(string title)
        {
            var response = GetGETResponse(
                                resource: APIResource.GetTodoByTitle,
                                urlSegment: UrlSegment.TodoTitle,
                                value: title);

            return response;
        }

        public IRestResponse GetTodosPage(long page)
        {
            var response = GetGETResponse(
                                resource: APIResource.GetTodosByPage,
                                urlSegment: UrlSegment.Page,
                                value: page);

            return response;
        }

        public IRestResponse CreateUser(dynamic payload)
        {
            var response = GetUpdateResponse(
                                method: Method.POST,
                                resource: APIResource.GetUsers,
                                payload: payload);
            
            return response;
        }

        public IRestResponse GetTodosPageByUri(Uri page)
        {
            var api = new GoRestAPI();
            var client = api.CreateClient(page);
            var request = api.CreateGetRequest();
            var response = api.GetResponse(client, request);

            return response;
        }

        public IRestResponse UpdateUserPartially(long userId, dynamic payload)
        {
            var response = GetUpdateResponse(
                                    method: Method.PATCH,
                                    payload: payload,
                                    resource: APIResource.GetUserById,
                                    urlSegment: UrlSegment.UserId,
                                    value: userId);

            return response;
        }

        public IRestResponse UpdateUser(long userId, dynamic payload)
        {
            var response = GetUpdateResponse(
                                    method: Method.PUT,
                                    payload: payload,
                                    resource: APIResource.GetUserById,
                                    urlSegment: UrlSegment.UserId,
                                    value: userId);

            return response;
        }

        public IRestResponse UpdateTodoPartially(long todoId, dynamic payload)
        {
            var response = GetUpdateResponse(
                                    method: Method.PATCH,
                                    payload: payload,
                                    resource: APIResource.GetTodoById,
                                    urlSegment: UrlSegment.TodoId,
                                    value: todoId);

            return response;
        }

        public IRestResponse DeleteUser(long userId)
        {
            var response = GetDELETEResponse(
                                resource: APIResource.GetUserById,
                                urlSegment: UrlSegment.UserId,
                                value: userId);

            return response;
        }

        public IRestResponse DeleteTodo(long todoId)
        {
            var response = GetDELETEResponse(
                                resource: APIResource.GetTodoById,
                                urlSegment: UrlSegment.TodoId,
                                value: todoId);

            return response;
        }

        public IRestResponse GetTodoById(long todoId)
        {
            var response = GetGETResponse(
                                resource: APIResource.GetTodoById,
                                urlSegment: UrlSegment.TodoId,
                                value: todoId);

            return response;
        }

        public IRestResponse GetTodosByUserId(long userId)
        {
            var response = GetGETResponse(
                                resource: APIResource.GetUserTodosByUserId,
                                urlSegment: UrlSegment.UserId,
                                value: userId);

            return response;
        }

        public IRestResponse CreateTodo(long userId, dynamic payload)
        {
            var response = GetUpdateResponse(
                                    method: Method.POST,
                                    payload: payload,
                                    resource: APIResource.GetUserTodosByUserId,
                                    urlSegment: UrlSegment.UserId,
                                    value: userId);

            return response;
        }

        private IRestResponse GetGETResponse(string resource, string urlSegment, object value)
        {
            var api = new GoRestAPI();
            var client = api.CreateClient();
            var request = api.CreateGetRequest();
            request.Resource = resource;
            request.AddUrlSegment(urlSegment, value);
            var response = api.GetResponse(client, request);

            return response;
        }

        private IRestResponse GetUpdateResponse(Method method, dynamic payload, string resource, string urlSegment = null, object value = null)
        {
            var api = new GoRestAPI();
            var client = api.CreateClient();
            var jsonRequest = HandleContent.Serialize(payload);
            var request = api.CreateUpdateRequest(method, jsonRequest, ACCESS_TOKEN);
            request.Resource = resource;
            
            if (!string.IsNullOrEmpty(urlSegment) && value != null)
            {
                request.AddUrlSegment(urlSegment, value);
            }
                
            var response = api.GetResponse(client, request);

            return response;
        }

        private IRestResponse GetDELETEResponse(string resource, string urlSegment, object value)
        {
            var api = new GoRestAPI();
            var client = api.CreateClient();
            var request = api.CreateDeleteRequest(ACCESS_TOKEN);
            request.Resource = resource;
            request.AddUrlSegment(urlSegment, value);
            var response = api.GetResponse(client, request);

            return response;
        }
    }
}
