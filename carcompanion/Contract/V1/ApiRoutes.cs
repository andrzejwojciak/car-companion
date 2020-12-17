namespace carcompanion.Contract.V1
{
    public static class ApiRoutes
    {
        
        public const  string Root = "api";
        private const  string Version = "v1";
        private const  string Base = Root + "/" + Version;

        public static class Cars
        {
            public const string CreateCar = Base + "/cars";                   
            public const string PutCar = Base + "/cars/{carId}";
            public const string PatchCar = Base + "/cars/{carId}";
            public const string GetCarById = Base + "/cars/{carId}";  
            public const string DeleteCar = Base + "/cars/{carId}";
            public const string GetUserCars = Base + "/cars/user-cars";
        }

        public static class Expenses
        {            
            public const string GetCarExpesnes = Base + "/cars/{carId}/expenses";
            public const string CreateCarExpense = Base + "/cars/{carId}/expenses";
            public const string GetCarExpenseById = Base + "/cars/{carId}/expenses/{expenseId}"; 
            public const string DeleteCarExpenseById = Base + "/cars/{carId}/expenses/{expenseId}"; 
        }
        
    }
}