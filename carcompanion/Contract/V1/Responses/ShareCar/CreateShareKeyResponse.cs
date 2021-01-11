using System;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.ShareCar
{
    public class CreateShareKeyResponse : IResponseData
    {
        public bool Success { get; set; }
        public Guid ShareKey { get; set; }        
    }
}