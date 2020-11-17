using System;

namespace carcompanion.Contract.V1.Responses
{
    public class GetCarByIdResponse
    {        
        public Guid CarId { get; set; }
        public string MainName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public string Plate { get; set; }
        public int Mileage { get; set; }        
        public int ProductionYear { get; set; }             
    }
}