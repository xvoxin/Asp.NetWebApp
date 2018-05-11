using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Extensions.Validators
{
    public class ArtistNameValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }
           
            var name = value.ToString();
            if (name.Length > 2 && name.Length <= 20)
                return ValidationResult.Success;
            else
                return new ValidationResult("Name should be 3 - 20 characters length");

        }
    }
}
