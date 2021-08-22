using RestSharp;
using TechTalk.SpecFlow;
using APIClient;
using TechTalk.SpecFlow.Assist;

namespace TestGoRestAPI.Steps
{
    [Binding]
    public class Users
    {
        private readonly ScenarioContext scenarioContext;
        private readonly StepsHelper stepsHelper;

        public Users(ScenarioContext scenarioContext, StepsHelper stepsHelper)
        {
            this.scenarioContext = scenarioContext;
            this.stepsHelper = stepsHelper;
        }        
        
        [Given(@"user with email ""(.*)"" is (present|absent)")]
        public void Step_VerifyUserByEmailAvailability(string email, string availability)
        {
            stepsHelper.VerifyUserByEmailAvailability(
                email: email,
                availability: availability);
        }

        [When(@"create user:")]
        public void Step_CreateUser(Table table)
        {
            UpdateUserRequestDTO user = table.CreateInstance<UpdateUserRequestDTO>();
            
            UserDTO cratedUser = stepsHelper.CreateUser(user);

            scenarioContext[nameof(UserDTO)] = cratedUser;
        }

        [When(@"partially update user with email ""(.*)"":")]
        public void Step_PartiallyUpdateUserWithEmail(string email, Table table)
        {
            UpdateUserRequestDTO user = table.CreateInstance<UpdateUserRequestDTO>();
            scenarioContext[nameof(UpdateUserRequestDTO)] = user;

            UserDTO updatedUser = stepsHelper.UpdateUserWithEmail(email, user.Email);

            scenarioContext[nameof(UserDTO)] = updatedUser;
        }

        [When(@"update user with email ""(.*)"":")]
        public void Step_UpdateUserWithEmail(string email, Table table)
        {
            UpdateUserRequestDTO user = table.CreateInstance<UpdateUserRequestDTO>();
            scenarioContext[nameof(UpdateUserRequestDTO)] = user;

            UserDTO updatedUser = stepsHelper.UpdateUser(email, user);

            scenarioContext[nameof(UserDTO)] = updatedUser;
        }

        [When(@"delete user with email ""(.*)""")]
        public void Step_DeleteUserWithEmail(string email)
        {
            long userId = this.stepsHelper.DeleteUserByEmail(email);
            scenarioContext["UserId"] = userId;
        }

        [Then(@"verify user is created")]
        [Then(@"verify user is updated")]
        public void Step_VerifyUser()
        {
            UserDTO createdUser;
            scenarioContext.TryGetValue(nameof(UserDTO), out createdUser);

            stepsHelper.VerifyUser(createdUser.User);
        }

        [Then(@"verify user is deleted")]
        public void Step_VerifyUserIsDeleted()
        {
            long userId;
            scenarioContext.TryGetValue("UserId", out userId);
            stepsHelper.VerifyUserIsDeleted(userId);
        }
    }
}