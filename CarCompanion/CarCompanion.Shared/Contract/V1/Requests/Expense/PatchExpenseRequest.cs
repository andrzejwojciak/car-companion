using System;
using System.ComponentModel.DataAnnotations;
using CarCompanion.Shared.Validation;

namespace CarCompanion.Shared.Contract.V1.Requests.Expense
{
    public class PatchExpenseRequest
    {
        [Range(0, 9999999999999999.99, ErrorMessage = "Value for Amount must more than 0")]
        public decimal? Amount { get; set; }     
    
        [MaxLength(256, ErrorMessage = "Value for Description can be 256 length max")]
        public string Description { get; set; }                                
        
        [Range(0, 1000000 ,ErrorMessage = "Value for MileageInterval must be between 0 and 1000000")]
        public int? MileageInterval { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [IsDateFuture(ErrorMessage = "Value for EndOfDateInterval must be later than today")]      
        public DateTime? EndOfDateInterval { get; set; }   

        public string Category { get; set; }   
        
    }
}