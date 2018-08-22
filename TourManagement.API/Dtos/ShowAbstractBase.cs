using System;
using System.ComponentModel.DataAnnotations;

namespace TourManagement.API.Dtos
{
    public class ShowAbstractBase
    {
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(150)]
        public string Venue { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(150)]
        public string City { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(150)]
        public string Country { get; set; }
    }
}