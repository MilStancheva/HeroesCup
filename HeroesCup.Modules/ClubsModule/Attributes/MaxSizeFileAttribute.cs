using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClubsModule.Attributes
{
    public class MaxSizeFileAttribute : ValidationAttribute
    {
        private readonly int maxSize;

        public MaxSizeFileAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > this.maxSize)
                {
                    return new ValidationResult(string.Format(this.ErrorMessage, this.maxSize));
                }
            }

            return ValidationResult.Success;
        }
    }
}