using System;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Car
{
    public class CreateCarResponse : IResponseData
    {
        public Guid CarId { get; set; }
        public string MainName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public string Plate { get; set; }
        public int? Mileage { get; set; }
        public int ProductionYear { get; set; }
    }
}