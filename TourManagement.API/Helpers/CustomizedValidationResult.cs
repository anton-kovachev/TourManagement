using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TourManagement.API.Helpers
{
    public class CustomizedValidationResult : Dictionary<string, IEnumerable<CustomizedValidationError>>
    {
        public CustomizedValidationResult() : base(StringComparer.OrdinalIgnoreCase)
        {

        }

        public CustomizedValidationResult(ModelStateDictionary modelState) : this()
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach (var propertyValidationEntry in modelState)
            {
                string propertyName = propertyValidationEntry.Key;
                var propertyCustomizedValidationErrors = new List<CustomizedValidationError>();

                foreach (var error in propertyValidationEntry.Value.Errors)
                {
                    var errorMessages = error.ErrorMessage.Split('|');
                    CustomizedValidationError customizedValidationError = null;

                    if (errorMessages.Length == 2)
                    {
                        customizedValidationError = new CustomizedValidationError(errorMessages[1], errorMessages[0]);
                    }
                    else 
                    {
                        customizedValidationError = new CustomizedValidationError(errorMessages[0]);
                    }

                    propertyCustomizedValidationErrors.Add(customizedValidationError);
                }

                Add(propertyName, propertyCustomizedValidationErrors);
            }
        }        
    }
}