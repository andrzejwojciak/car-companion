using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Car
{
    public class DeleteCarResponse : IResponseData
    {
        public bool CarDeleted { get; set; }        
        
    }
}