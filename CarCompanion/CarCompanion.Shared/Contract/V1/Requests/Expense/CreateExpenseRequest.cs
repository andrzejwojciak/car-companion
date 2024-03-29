using System;
using System.ComponentModel.DataAnnotations;
using CarCompanion.Shared.Validation;

namespace CarCompanion.Shared.Contract.V1.Requests.Expense
{
    public class CreateExpenseRequest
    {

        [Required]
        [Range(0, 9999999999999999.99, ErrorMessage = "Value for Amount must more than 0")]
        public decimal? Amount { get; set; }     
    
        [MaxLength(512, ErrorMessage = "Value for Description can be 512 length max")]
        public string Description { get; set; }                                
        
        [Range(1, 1000000 ,ErrorMessage = "Value for MileageInterval must be between 1 and 1000000")]
        public int? MileageInterval { get; set; }

        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]      
        [IsDateValid(ErrorMessage = "Value for date can't be null or less than 1970-01-01, proper format is {yyyy-MM-dd}")]
        public DateTime? Date { get; set; }   

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [IsDateFuture(ErrorMessage = "Value for EndOfDateInterval must be later than today")]      
        public DateTime? EndOfDateInterval { get; set; }   

        [Required]
        public string Category { get; set; }        
    }
}