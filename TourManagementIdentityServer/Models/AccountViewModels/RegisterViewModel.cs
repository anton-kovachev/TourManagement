using System;
using System.ComponentModel.DataAnnotations;

namespace TourManagementIdentityServer.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The email address must be between 4 and 100 characters long", MinimumLength = 4)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must between 4 and 100 characters", MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [StringLength(100, ErrorMessage = "The password must between 4 and 100 characters", MinimumLength = 4)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }
    }
}