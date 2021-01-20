using System;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Validation
{
    public class IsDateFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            try
            {
                return ((Convert.ToDateTime(value) >= DateTime.Now || value == null) ? ValidationResult.Success : new ValidationResult(ErrorMessage));
            }
            catch
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}