using carcompanion.Contract.V1.Responses.Expense;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Results
{
    public class ServiceResult
    {
        private string _errorMessage;
        private int _statusCode;

        public string ErrorMessage 
        { 
            get
            {
                if(_errorMessage.Equals(null))
                    return "Something went wrong";
                
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            } 
        }            

        public int StatusCode 
        { 
            get
            {
                if(_statusCode == 0)
                {
                    if(Success)
                    {
                        return 200;
                    } else return 500;
                }
                
                return _statusCode;
            }
            set
            {
                _statusCode = value;
            } 
        }

        public bool Success { get; set; }     
        public IResponseData ResponseData { get; set; }  
    }
}