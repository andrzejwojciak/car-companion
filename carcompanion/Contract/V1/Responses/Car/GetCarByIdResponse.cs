using System;
using System.Collections.Generic;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Car
{
    public class GetCarByIdResponse : IResponseData
    {        
        public Guid CarId { get; set; }
        public string MainName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public string Plate { get; set; }
        public int? Mileage { get; set; }        
        public int ProductionYear { get; set; }      
        public IEnumerable<CarUserResponse> Users { get; set; }
    }
}