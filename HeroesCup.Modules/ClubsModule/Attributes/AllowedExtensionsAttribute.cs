using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ClubsModule.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!this.extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(string.Format(this.ErrorMessage, string.Join( ", ", this.extensions)));
                }
            }

            return ValidationResult.Success;
        }
    }
}
