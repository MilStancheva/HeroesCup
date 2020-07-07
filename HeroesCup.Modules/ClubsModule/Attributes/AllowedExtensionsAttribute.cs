using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            var files = value as IEnumerable<IFormFile>;
            if (files != null)
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);

                    if (!this.extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult(string.Format(this.ErrorMessage, string.Join(", ", this.extensions)));
                    }
                }                
            }

            return ValidationResult.Success;
        }
    }
}