namespace carcompanion.Results.Interfaces
{
    public interface IServiceResult
    {
        bool Success { get; set; }              
        string ErrorMessage { get; set; }            
        int StatusCode { get; set; }     
    }
}