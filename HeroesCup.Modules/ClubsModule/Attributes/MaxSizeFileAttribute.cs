using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
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
            var files = value as IEnumerable<IFormFile>;
            if (files != null)
            {
                long size = 0;
                foreach (var file in files)
                {
                    size += file.Length;
                    if (size > this.maxSize)
                    {
                        return new ValidationResult(string.Format(this.ErrorMessage, this.maxSize));
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}