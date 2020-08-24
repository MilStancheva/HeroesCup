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
            ValidationResult validationResult = ValidationResult.Success;
            if (files != null)
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    validationResult = this.GetValidationResult(validationContext, file);
                    if (validationResult != ValidationResult.Success)
                    {
                        return validationResult;
                    }
                }
            }
            else
            {
                var file = value as IFormFile;
                if (file != null)
                {
                    validationResult = this.GetValidationResult(validationContext, file);
                    if (validationResult != ValidationResult.Success)
                    {
                        return validationResult;
                    }
                }
            }

            return validationResult;
        }

        private ValidationResult GetValidationResult(ValidationContext validationContext, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!this.extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(string.Format(this.GetErrorMessage(validationContext), string.Join(", ", this.extensions)));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                return "Invalid error message";
            }

            HeroesCup.Localization.ManagerLocalizer localizer = validationContext.GetService(typeof(HeroesCup.Localization.ManagerLocalizer)) as HeroesCup.Localization.ManagerLocalizer;
            return localizer.General[ErrorMessage];
        }
    }
}