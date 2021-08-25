using TechTalk.SpecFlow;
using APIClient;

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

        [Given(@"todo with title ""(.*)"" is (present|absent)")]
        public void GivenTodoWithTitleIsPresent(string title, string availability)
        {
            stepsHelper.VerifyTodoByTitleAvailability(
                title: title,
                availability: availability);
        }

        [Given(@"page number ""(.*)"" of todos is loaded")]
        public void Step_VerifyPageNumbreOfTodosIsLoaded(long page)
        {
            var pagination = stepsHelper.VerifyPageNumbreOfTodosIsLoaded(page);
            scenarioContext[nameof(Pagination)] = pagination;
        }

        [When(@"go to (next|previous) page")]
        public void WhenGoToPage(string page)
        {
            Pagination currentPagination, nextPagination;
            scenarioContext.TryGetValue(nameof(Pagination), out currentPagination);

            if (page == "previous")
            {
                nextPagination = stepsHelper.GoToTodosPage(currentPagination.Links.Previous);
            }
            else
            {
                nextPagination = stepsHelper.GoToTodosPage(currentPagination.Links.Next);
            }

            scenarioContext[nameof(Pagination)] = nextPagination;
        }

        [Then(@"verify the page is loaded")]
        public void ThenVerifyTheNextPageIsLoaded()
        {
            Pagination paagination;
            scenarioContext.TryGetValue(nameof(Pagination), out paagination);

            stepsHelper.VerifyPage(paagination);
        }

        [When(@"create user")]
        public void Step_CreateUser()
        {
            UpdateUserRequestDTO user = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\CreateUser.json");

            UserDTO cratedUser = stepsHelper.CreateUser(user);

            scenarioContext[nameof(UserDTO)] = cratedUser;
        }

        [When(@"partially update user with email ""(.*)""")]
        public void Step_PartiallyUpdateUserWithEmail(string email)
        {
            UpdateUserRequestDTO user = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\UpdateUserEmail.json");
            UserDTO updatedUser = stepsHelper.UpdateUserWithEmail(email, user.Email);
            scenarioContext[nameof(UserDTO)] = updatedUser;
        }

        [When(@"update user with email ""(.*)""")]
        public void Step_UpdateUserWithEmail(string email)
        {
            UpdateUserRequestDTO user = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\UpdateUser.json");
            UserDTO updatedUser = stepsHelper.UpdateUser(email, user);
            scenarioContext[nameof(UserDTO)] = updatedUser;
        }

        [When(@"update user`s todo partially")]
        public void Step_UpdateUserSTodo()
        {
            //string email = table.Rows[0]["Email"];
            //string title = table.Rows[0]["Title"];
            //string status = table.Rows[0]["Status"];
            UpdateUserRequestDTO user = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\CreateUser.json");
            UpdateTodoRequestDTO todo = HandleContent.ParseJson<UpdateTodoRequestDTO>(@"TestData\UpdateTodo.json");
            TodoDTO updatedTodo = stepsHelper.UpdateUsersTodo(user.Email, todo.Title, todo.Status);

            scenarioContext[nameof(TodoDTO)] = updatedTodo;
        }

        [When(@"delete user with email ""(.*)""")]
        public void Step_DeleteUserWithEmail(string email)
        {
            long userId = this.stepsHelper.DeleteUserByEmail(email);
            scenarioContext["UserId"] = userId;
        }

        [When(@"delete user`s todo")]
        public void Step_DeleteTodoWithTitle()
        {
            UpdateUserRequestDTO user = HandleContent.ParseJson<UpdateUserRequestDTO>(@"TestData\CreateUser.json");
            UpdateTodoRequestDTO todo = HandleContent.ParseJson<UpdateTodoRequestDTO>(@"TestData\CreateTodo.json");
            long todoId = this.stepsHelper.DeleteUsersTodo(user.Email, todo.Title);
            scenarioContext["TodoId"] = todoId;
        }

        [When(@"user with email ""(.*)"" creates todo")]
        public void WhenUserWithEmailCreatesTodo(string email)
        {
            UpdateTodoRequestDTO todo = HandleContent.ParseJson<UpdateTodoRequestDTO>(@"TestData\CreateTodo.json");
            TodoDTO createdTodo = stepsHelper.CreateTodo(email, todo);
            scenarioContext[nameof(TodoDTO)] = createdTodo;
        }

        [Then(@"verify user is created")]
        [Then(@"verify user is updated")]
        public void Step_VerifyUser()
        {
            UserDTO user;
            scenarioContext.TryGetValue(nameof(UserDTO), out user);

            stepsHelper.VerifyUser(user.User);
        }

        [Then(@"verify todo is updated")]
        public void ThenVerifyTodoIsUpdated()
        {
            TodoDTO todo;
            scenarioContext.TryGetValue(nameof(TodoDTO), out todo);

            stepsHelper.VerifyTodo(todo.Todo);
        }

        [Then(@"verify todo is created")]
        public void ThenVerifyTodoIsCreated()
        {
            TodoDTO createdTodo;
            scenarioContext.TryGetValue(nameof(TodoDTO), out createdTodo);

            stepsHelper.VerifyTodo(createdTodo.Todo);
        }

        [Then(@"verify user is deleted")]
        public void Step_VerifyUserIsDeleted()
        {
            long userId;
            scenarioContext.TryGetValue("UserId", out userId);
            stepsHelper.VerifyUserIsDeleted(userId);
        }

        [Then(@"verify todo is deleted")]
        public void Step_VerifyTodoIsDeleted()
        {
            long todoId;
            scenarioContext.TryGetValue("TodoId", out todoId);
            stepsHelper.VerifyTodoIsDeleted(todoId);
        }
    }
}