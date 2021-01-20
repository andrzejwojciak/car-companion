using System;
using System.ComponentModel.DataAnnotations;
using carcompanion.Validation;

namespace carcompanion.Contract.V1.Requests.Summary
{
    public class GetSummaryQueryRequest
    {        
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        [IsDateValid(ErrorMessage = "Value for StartDate can't be null or earlier than 1970-01-01. Porper format is {yyyy-MM-dd}")]
        [IsDatePast(ErrorMessage = "Value for EndDate can't be null or later than doday. Porper format is {yyyy-MM-dd}")]    
        public DateTime StartDate { get; set; }
        
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  
        [IsDatePast(ErrorMessage = "Value for EndDate can't be null or later than doday. Porper format is {yyyy-MM-dd}")]      
        public DateTime EndDate { get; set; }
    }
}