using System;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.V1.Requests
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

        [Range(0, 5000000, ErrorMessage = "Wrong mileage number")]
        public int Mileage { get; set; }        
        
        [Range(1900, 3000, ErrorMessage = "Wrong production year")]
        public int ProductionYear { get; set; }             
    }
}