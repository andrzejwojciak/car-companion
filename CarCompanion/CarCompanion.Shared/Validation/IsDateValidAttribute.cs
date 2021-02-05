using System;
using System.ComponentModel.DataAnnotations;

namespace CarCompanion.Shared.Validation
{
    public class IsDateValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {   
            try
            {
                return (Convert.ToDateTime(value) >= new DateTime(1970, 1, 1)) ? ValidationResult.Success : new ValidationResult(ErrorMessage);
            }
            catch
            {
                return new ValidationResult(ErrorMessage);
            }
        }           
    }
}