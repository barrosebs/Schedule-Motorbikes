using System.ComponentModel.DataAnnotations;

namespace SM.Domain.Validation
{
    public class StartDateToAllocationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime startDate)
            {
                if (startDate < DateTime.Today)
                {
                    return new ValidationResult("A data de início deve ser maior ou igual a data de hoje.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
