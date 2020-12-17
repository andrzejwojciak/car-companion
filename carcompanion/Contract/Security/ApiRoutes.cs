namespace carcompanion.Contract.Security
{
    public static class ApiRoutes
    {
        private const string Root = "api";

        public static class Auth
        {
            private const string Base = Root + "/auth-manager";

            public const string Login = Base + "/login";
            public const string Register = Base + "/register";
            public const string Refresh = Base + "/refresh-token";
            public const string Logout = Base + "/logout";
        }
    }
}