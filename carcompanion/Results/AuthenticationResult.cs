namespace carcompanion.Results
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }        
        public string ErrorMessage { get; set; }       
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}