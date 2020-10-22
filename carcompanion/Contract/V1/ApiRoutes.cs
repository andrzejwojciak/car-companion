namespace carcompanion.Contract.V1
{
    public static class ApiRoutes
    {
        
        public const  string Root = "api";
        private const  string Version = "v1";
        private const  string Base = Root + "/" + Version;

        public static class Tests
        {             
            public const string GetAll = Base + "/tests";      
            public const string Create = Base + "/tests";
            public const string GetTestById = Base + "/tests/{id}";
            public const string Delete = Base + "/tests/{id}";
            public const string Update = Base + "/tests/{id}";
        }
        
    }
}