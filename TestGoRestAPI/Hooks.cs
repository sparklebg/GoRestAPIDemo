using APIClient;
using TechTalk.SpecFlow;

namespace TestGoRestAPI
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private StepsHelper stepsHelper;
        UserDTO createdUser;
        TodoDTO createdTodo;

        [BeforeScenario (Order = 1)]
        [Scope(Tag = "RequiresUser")]
        public void CreateUser()
        {
            stepsHelper = new StepsHelper();
            var user = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\CreateUser.json");
            this.createdUser = stepsHelper.CreateUser(user);
        }

        [BeforeScenario(Order = 2)]
        [Scope(Tag = "RequiresTodo")]
        public void CreateTodo()
        {
            stepsHelper = new StepsHelper();
            var user = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\CreateUser.json");
            var todo = HandleContent.ParseJson<UpdateTodoRequestDTO>(@"TestData\CreateTodo.json");
            this.createdTodo = stepsHelper.CreateTodo(user.Email, todo);
        }

        [AfterScenario (Order = 2)]
        [Scope(Tag = "RequiresUser")]
        public void DeleteUser()
        {
            stepsHelper = new StepsHelper();
            stepsHelper.DeleteUserById(createdUser.User.Id);
        }

        [AfterScenario(Order = 1)]
        [Scope(Tag = "RequiresTodo")]
        public void DeleteTodo()
        {
            stepsHelper = new StepsHelper();
            stepsHelper.DeleteUserById(createdUser.User.Id);
        }

        [AfterScenario]
        [Scope(Tag = "DeleteUser")]
        public void DeleteUserByMail()
        {
            stepsHelper = new StepsHelper();
            var payload = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\CreateUser.json");
            stepsHelper.DeleteUserByEmail(payload.Email);
        }
    }
}
