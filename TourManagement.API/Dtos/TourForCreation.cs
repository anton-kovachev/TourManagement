using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using TourManagement.API.Validators;

namespace TourManagement.API.Dtos
{
    public class TourForCreation : TourAbstractBase
    {
        [Required]
        public Guid? BandId { get; set; }

        [Required]
        public decimal EstimatedProfits { get; set; }
    }
}