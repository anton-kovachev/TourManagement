/* using System.ComponentModel.DataAnnotations;

namespace TourManagementIdentityServer.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "User Name must be an email between 4 and 100 characters")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must between 4 and 100 characters", MinimumLength = 4)]
        public string Password { get; set; }
    }
} */