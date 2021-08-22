using APIClient;
using NUnit.Framework;
using RestSharp;
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
                    //condition: content.Users.Length > 0,
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

        public long DeleteUserByEmail(string email)
        {
            User user = this.GetUserByEmail(email);
            this.DeleteUserById(user.Id);

            return user.Id;
        }

        public long DeleteUserById(long userId)
        {
            var client = new Client<UsersDTO>();
            response = client.DeleteUser(userId);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent), response.Content);

            return userId;
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
    }
}
