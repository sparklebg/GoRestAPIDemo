using APIClient;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace TestGoRestAPI
{
    public class StepsHelper
    {
        private IRestResponse response;

        public void VerifyUserByEmailAvailability(string email, string availability)
        {
            bool isPresent = availability.ToBoolean();

            User user = this.GetUserByEmail(email);

            if (isPresent)
            {
                Assert.True(
                    condition: user != null,
                    message: @$"User with email ""{email}"" is {availability.ToOppositeBoolean()}!");

                Assert.AreEqual(email, user.Email);
            }
            else
            {
                Assert.True(
                    condition: user == null,
                    message: @$"User with email ""{email}"" is {availability.ToOppositeBoolean()}!");
            }
        }

        public void VerifyUserByIdAvailability(long userId, string availability)
        {
            bool isPresent = availability.ToBoolean();

            User user = this.GetUserById(userId);

            if (isPresent)
            {
                Assert.True(
                    condition: user != null,
                    message: @$"User with email ""{userId}"" is {availability.ToOppositeBoolean()}!");

                Assert.AreEqual(userId, user.Id);
            }
            else
            {
                Assert.True(
                    condition: user == null,
                    message: @$"User with email ""{userId}"" is {availability.ToOppositeBoolean()}!");
            }
        }

        public UserDTO CreateUser(UpdateUserRequestDTO user)
        {
            var client = new Client<UpdateUserRequestDTO>();
            response = client.CreateUser(user);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), response.Content);
            
            var content = HandleContent.GetContent<UserDTO>(response);

            Assert.AreEqual(user.Name, content.User.Name);
            Assert.AreEqual(user.Email, content.User.Email);
            Assert.AreEqual(user.Gender, content.User.Gender);
            Assert.AreEqual(user.Status, content.User.Status);

            return content;
        }

        public UserDTO UpdateUserWithEmail(string oldEmail, string updatedEmail)
        {
            var client = new Client<UpdateUserRequestDTO>();
            User user = this.GetUserByEmail(oldEmail);

            response = client.UpdateUserPartially(user.Id, new { email = updatedEmail });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<UserDTO>(response);

            Assert.AreEqual(updatedEmail, content.User.Email);

            return content;
        }

        public UserDTO UpdateUser(string email, UpdateUserRequestDTO updatedUser)
        {
            var client = new Client<UpdateUserRequestDTO>();
            User user = this.GetUserByEmail(email);

            response = client.UpdateUser(user.Id, updatedUser);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<UserDTO>(response);

            Assert.AreEqual(updatedUser.Name, content.User.Name);
            Assert.AreEqual(updatedUser.Email, content.User.Email);
            Assert.AreEqual(updatedUser.Gender, content.User.Gender);
            Assert.AreEqual(updatedUser.Status, content.User.Status);

            return content;
        }

        public TodoDTO UpdateUsersTodo(string email, string title, string status)
        {
            var client = new Client<UpdateTodoRequestDTO>();
            User user = this.GetUserByEmail(email);
            Todo todo = this.GetTodoByUserIdAndTitle(user.Id, title);

            response = client.UpdateTodoPartially(todo.Id, new { status = status });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodoDTO>(response);

            Assert.AreEqual(todo.Id, content.Todo.Id);
            Assert.AreEqual(user.Id, content.Todo.UserId);
            Assert.AreEqual(title, content.Todo.Title);
            Assert.AreEqual(status, content.Todo.Status);

            return content;
        }

        public void VerifyUser(User user)
        {
            var client = new Client<UpdateUserRequestDTO>();
            response = client.GetUserById(user.Id);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<UserDTO>(response);

            Assert.AreEqual(user.Name, content.User.Name);
            Assert.AreEqual(user.Email, content.User.Email);
            Assert.AreEqual(user.Gender, content.User.Gender);
            Assert.AreEqual(user.Status, content.User.Status);
        }

        public void VerifyUserIsDeleted(long userId)
        {
            VerifyUserByIdAvailability(userId, "absent");
        }

        public void VerifyTodoIsDeleted(long todoId)
        {
            VerifyTodoByIdAvailability(todoId, "absent");
        }

        public long DeleteUserByEmail(string email)
        {
            User user = this.GetUserByEmail(email);
            this.DeleteUserById(user.Id);

            return user.Id;
        }

        public long DeleteUsersTodo(string email, string title)
        {
            User user = this.GetUserByEmail(email);
            Todo todo = this.GetTodoByUserIdAndTitle(user.Id, title);

            this.DeleteTodoById(todo.Id);

            return todo.Id;
        }

        public long DeleteUserById(long userId)
        {
            var client = new Client<UsersDTO>();
            response = client.DeleteUser(userId);

            return userId;
        }

        private long DeleteTodoById(long todoId)
        {
            var client = new Client<TodoDTO>();
            response = client.DeleteTodo(todoId);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent), response.Content);

            return todoId;
        }

        private User GetUserByEmail(string email)
        {
            var client = new Client<UsersDTO>();
            response = client.GetUserByEmail(email);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<UsersDTO>(response);

            return content.Users.FirstOrDefault();
        }

        private User GetUserById(long userId)
        {
            var client = new Client<UsersDTO>();
            response = client.GetUserById(userId);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;                

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<UsersDTO>(response);

            return content.Users.FirstOrDefault();
        }

        public void VerifyTodoByTitleAvailability(string title, string availability)
        {
            bool isPresent = availability.ToBoolean();

            Todo todo = this.GetTodoByTitle(title);

            if (isPresent)
            {
                Assert.True(
                    condition: todo != null,
                    message: @$"Todo with title ""{title}"" is {availability.ToOppositeBoolean()}!");

                Assert.AreEqual(title, todo.Title);
            }
            else
            {
                Assert.True(
                    condition: todo == null,
                    message: @$"Todo with title ""{title}"" is {availability.ToOppositeBoolean()}!");
            }
        }

        public void VerifyTodoByIdAvailability(long id, string availability)
        {
            bool isPresent = availability.ToBoolean();

            Todo todo = this.GetTodoById(id);

            if (isPresent)
            {
                Assert.True(
                    condition: todo != null,
                    message: @$"Todo with id ""{id}"" is {availability.ToOppositeBoolean()}!");

                Assert.AreEqual(id, todo.Title);
            }
            else
            {
                Assert.True(
                    condition: todo == null,
                    message: @$"Todo with id ""{id}"" is {availability.ToOppositeBoolean()}!");
            }
        }

        public TodoDTO CreateTodo(string email, UpdateTodoRequestDTO todo)
        {
            var client = new Client<UpdateTodoRequestDTO>();
            User user = this.GetUserByEmail(email);
            response = client.CreateTodo(user.Id, todo);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), response.Content);

            var content = HandleContent.GetContent<TodoDTO>(response);

            Assert.AreEqual(todo.Title, content.Todo.Title);
            Assert.AreEqual(todo.Status, content.Todo.Status);
            Assert.AreEqual(user.Id, content.Todo.UserId);

            return content;
        }

        public void VerifyTodo(Todo todo)
        {
            var client = new Client<UpdateUserRequestDTO>();
            response = client.GetTodoById(todo.Id);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodoDTO>(response);

            Assert.AreEqual(todo.Title, content.Todo.Title);
            Assert.AreEqual(todo.Status, content.Todo.Status);
            Assert.AreEqual(todo.UserId, content.Todo.UserId);
        }

        public Pagination VerifyPageNumbreOfTodosIsLoaded(long page)
        {
            var client = new Client<TodoDTO>();
            response = client.GetTodosPage(page);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodosDTO>(response);

            Assert.AreEqual(page, content.Meta.Pagination.Page);

            return content.Meta.Pagination;
        }

        public Pagination GoToTodosPage(Uri nextPageUri)
        {
            var client = new Client<TodosDTO>();
            response = client.GetTodosPageByUri(nextPageUri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodosDTO>(response);

            Assert.AreEqual(nextPageUri, content.Meta.Pagination.Links.Current);

            return content.Meta.Pagination;
        }

        public void VerifyPage(Pagination pagination)
        {
            var client = new Client<TodosDTO>();
            response = client.GetTodosPageByUri(pagination.Links.Current);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodosDTO>(response);

            Assert.AreEqual(pagination.Page, content.Meta.Pagination.Page);
        }

        private Todo GetTodoByTitle(string title)
        {
            var client = new Client<TodosDTO>();
            response = client.GetTodosByTitle(title);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodosDTO>(response);

            return content.Todos.FirstOrDefault();
        }

        private Todo GetTodoById(long id)
        {
            var client = new Client<TodosDTO>();
            response = client.GetTodoById(id);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodosDTO>(response);

            return content.Todos.FirstOrDefault();
        }

        private Todo GetTodoByUserIdAndTitle(long userId, string title)
        {
            var client = new Client<TodosDTO>();
            response = client.GetTodosByUserId(userId);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);

            var content = HandleContent.GetContent<TodosDTO>(response);

            return content.Todos
                .Where(t => t.Title == title)
                .FirstOrDefault();
        }
    }
}
