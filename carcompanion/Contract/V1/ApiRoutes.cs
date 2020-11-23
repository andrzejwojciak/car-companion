namespace carcompanion.Contract.V1
{
    public static class ApiRoutes
    {
        
        public const  string Root = "api";
        private const  string Version = "v1";
        private const  string Base = Root + "/" + Version;

        public static class Cars
        {
            public const string Create = Base + "/cars";            
            public const string GetById = Base + "/cars/{carId}";           
            
        }

        public static class Expenses
        {            
            public const string GetCarExpesnes = Base + "/cars/{carId}/expenses";
            public const string CreateCarExpense = Base + "/cars/{carId}/expenses";
        }
        
    }
}