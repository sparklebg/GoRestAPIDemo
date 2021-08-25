namespace APIClient
{   
    public static class APIResource
    {
        public static readonly string GetUsers = "public/v1/users";

        public static readonly string GetUserById = $"public/v1/users/{{{UrlSegment.UserId}}}";

        public static readonly string GetUserByEmail = $"public/v1/users?email={{{UrlSegment.UserEmail}}}";

        public static readonly string GetTodoById = $"public/v1/todos/{{{UrlSegment.TodoId}}}";

        public static readonly string GetTodoByTitle = $"public/v1/todos?title={{{UrlSegment.TodoTitle}}}";

        public static readonly string GetTodosByPage = $"public/v1/todos?page={{{UrlSegment.Page}}}";

        public static readonly string GetUserTodosByUserId = $"public/v1/users/{{{UrlSegment.UserId}}}/todos";
    }
}
