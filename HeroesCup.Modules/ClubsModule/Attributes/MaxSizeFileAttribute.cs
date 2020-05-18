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

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > this.maxSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { this.maxSize} bytes.";
        }
    }
}
