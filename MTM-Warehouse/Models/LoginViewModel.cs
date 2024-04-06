using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(32)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(32)]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
