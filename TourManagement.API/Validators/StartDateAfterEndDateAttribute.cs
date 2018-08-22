using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TourManagement.API.Validators
{
    public class StartDateBeforeEndDateAttribute : ValidationAttribute
    {
        public StartDateBeforeEndDateAttribute(string endDateProperty): base("startDateBeforeEndDate|{0} is before {1}")
        {
            EndDateProperty = endDateProperty;
        }
        public string EndDateProperty { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTimeOffset endDate = GetEndDate(validationContext);

            if (DateTimeOffset.TryParse(value.ToString(), out DateTimeOffset startDate))
            {

                if (startDate < endDate) 
                {
                    return ValidationResult.Success;
                }
            }
            
            return new ValidationResult(string.Format(this.ErrorMessageString, endDate, startDate));
        }

        private DateTimeOffset GetEndDate(ValidationContext validationContext)
        {   
            PropertyInfo endDatePropertyInfo = validationContext.ObjectType.GetProperty(this.EndDateProperty);

            if (endDatePropertyInfo != null) 
            {
                if (DateTimeOffset.TryParse(endDatePropertyInfo.GetValue(validationContext.ObjectInstance).ToString(), out DateTimeOffset endDate))
                {
                    return endDate;
                }
            }
            
            return default(DateTimeOffset);
        }
    }
}