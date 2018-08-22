using System;
using System.ComponentModel.DataAnnotations;

namespace TourManagement.API.Dtos
{
    public class TourWithManagerForCreation : TourForCreation 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage="required|Manager is required.")]
        public Guid ManagerId { get; set; }
    }
}