using System;

namespace CarCompanion.Shared.Contract.V1.Responses.Car
{
    public class UpdateCarResponse
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