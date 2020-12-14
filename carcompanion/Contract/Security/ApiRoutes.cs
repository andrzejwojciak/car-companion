namespace carcompanion.Contract.Security
{
    public static class ApiRoutes
    {
        public static class Auth
        {
            public const string Login = "auth-manager/login";
            public const string Register = "auth-manager/register";
            public const string Refresh = "auth-manager/refresh-token";
            public const string Logout = "auth-manager/logout";
        }
    }
}