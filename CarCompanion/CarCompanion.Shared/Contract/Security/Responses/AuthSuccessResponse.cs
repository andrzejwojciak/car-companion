namespace CarCompanion.Shared.Contract.Security.Responses
{
    public class AuthSuccessResponse
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }        
    }
}