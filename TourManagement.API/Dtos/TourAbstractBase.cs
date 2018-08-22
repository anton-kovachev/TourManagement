using System;
using System.ComponentModel.DataAnnotations;
using TourManagement.API.Validators;

namespace TourManagement.API.Dtos
{
    public abstract class TourAbstractBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Title is required.")]
        [MaxLength(200, ErrorMessage = "maxLength|Title is too long.")]
        public string Title { get; set; }

        [MaxLength(2000, ErrorMessage = "maxLength|Description is too long.")]
        public virtual string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "required|The start date is required.")]
        [TourManagement.API.Validators.StartDateBeforeEndDate(endDateProperty: "EndDate")]
        public DateTimeOffset StartDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "required|The end date is required.")]
        public DateTimeOffset EndDate { get; set; }
    }
}