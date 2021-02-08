using System;

namespace CarCompanion.Shared.Contract.V1.Responses.ShareCar
{
    public class CreateShareKeyResponse
    {
        public bool Success { get; set; }
        public Guid ShareKey { get; set; }        
    }
}