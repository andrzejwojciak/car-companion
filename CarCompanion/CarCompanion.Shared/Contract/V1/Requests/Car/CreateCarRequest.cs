using System;
using System.ComponentModel.DataAnnotations;

namespace CarCompanion.Shared.Contract.V1.Requests.Car
{
    public class CreateCarRequest
    {
        [MaxLength(30)]
        public string MainName { get; set; }

        [MaxLength(30)]
        [Required]
        public string Brand { get; set; }

        [MaxLength(30)]
        [Required]
        public string Model { get; set; }

        [MaxLength(30)]
        public string Generation { get; set; }
        
        [MaxLength(8)]
        public string Plate { get; set; }        

        [Range(0, 5000000, ErrorMessage = "Value for Mileage must be between 0 and 5000000")]
        public int? Mileage { get; set; }        
        
        [Range(1000, 6000, ErrorMessage = "Value for ProductionYear must be between 10000 and 6000")]
        [Required]
        public int ProductionYear { get; set; }             
    }
}