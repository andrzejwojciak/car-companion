using System;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.V1.Requests.Log
{
    public class GetAllLogsQueryRequest
    {
        [Range(1, 200, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Required]
        public int PerPage { get; set; }

        [Range(1, 10000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Required]
        public int Page { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        
        [Required]
        public string SortOrder { get; set; }
    }
}