using System;
using System.ComponentModel.DataAnnotations;

namespace TourManagement.API.Dtos
{
    public class TourForUpdate : TourAbstractBase
    {
        private string _description = string.Empty;

        [Required(AllowEmptyStrings = false, 
            ErrorMessage = "required|When updating a tour, the description is required.")]
        public override string Description { get => base.Description; set => base.Description = value; }

/*         [Required(ErrorMessage="managerId|Manager is required")]
        public Guid? ManagerId { get; set; } */
    }
}