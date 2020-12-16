using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.V1.Requests
{
    public class PatchCarRequest
    {
        [MaxLength(30)]
        public string MainName { get; set; }

        [MaxLength(30)]
        public string Brand { get; set; }

        [MaxLength(30)]
        public string Model { get; set; }

        [MaxLength(30)]
        public string Generation { get; set; }
        
        [MaxLength(8)]
        public string Plate { get; set; }        

        [Range(0, 5000000, ErrorMessage = "Value for Mileage must be between 0 and 5000000")]
        public int? Mileage { get; set; }        
        
        [Range(1900, 3000, ErrorMessage = "Value for ProductionYear must be between 1900 and 3000")]
        public int? ProductionYear { get; set; }    
        
    }
}