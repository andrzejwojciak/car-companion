using System;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Validation
{
    public class IsDateValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
            => ((Convert.ToDateTime(value) >= new DateTime(1970, 1, 1)) ? ValidationResult.Success : new ValidationResult(ErrorMessage));                
    }
}