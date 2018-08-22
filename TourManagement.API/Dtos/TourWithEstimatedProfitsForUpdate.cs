using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TourManagement.API.Dtos
{
    public class TourWithEstimatedProfitsForUpdate : TourForUpdate
    {
        [Required(ErrorMessage="required|Estimated Profits is required,")]
        public decimal EstimatedProfits { get; set; }
    }
}