using System;

namespace CarCompanion.Shared.Contract.V1.Responses.ShareCar
{
    public class UseShareKeyResponse
    {
        public bool Success { get; set; }
        public Guid CarId { get; set; }        
    }
}