namespace carcompanion.Contract.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public static class Cars
        {
            public const string CreateCar = Base + "/cars";
            public const string GetCarById = Base + "/cars/{carId}";
            public const string GetUserCars = Base + "/cars/";
            public const string PutCar = Base + "/cars/{carId}";
            public const string PatchCar = Base + "/cars/{carId}";
            public const string DeleteCar = Base + "/cars/{carId}";
        }

        public static class Expenses
        {
            public const string GetCarExpesnes = Base + "/cars/{carId}/expenses";
            public const string CreateCarExpense = Base + "/cars/{carId}/expenses";
            public const string GetCarExpense = Base + "/cars/{carId}/expenses/{expenseId}";
            public const string DeleteCarExpense = Base + "/cars/{carId}/expenses/{expenseId}";
            public const string PutCarExpense = Base + "/cars/{carId}/expenses/{expenseId}";
            public const string PatchCarExpense = Base + "/cars/{carId}/expenses/{expenseId}";
            public const string GetCategories = Base + "/cars/expenses/categories";
        }

        public static class Summary
        {
            public const string GetSummaryForCar = Base + "/cars/{carId}/summary";
        }

        public static class Log
        {
            public const string GetUserLogs = Base + "/logs/my-logs";
            public const string GetAllLogs = Base + "/logs";
        }

        public static class ShareCar
        {
            public const string CreateShareKey = Base + "/cars/{carId}/share";
            public const string UseShareKey = Base + "/cars/use-sharekey/{shareKeyId}";
        }
    }
}