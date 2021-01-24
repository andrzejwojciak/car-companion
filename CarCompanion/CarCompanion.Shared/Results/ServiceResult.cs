namespace CarCompanion.Shared.Results
{
    public class ServiceResult<T>
    {
        private string _errorMessage;
        private int _statusCode;
        public bool Success { get; set; }
        public T Data { get; set; }

        public string ErrorMessage
        {
            get => _errorMessage ?? "Something went wrong";
            set => _errorMessage = value;
        }

        public int Status
        {
            get
            {
                if (_statusCode != 0) return _statusCode;
                return Success ? 200 : 500;
            }
            set => _statusCode = value;
        }
    }
}