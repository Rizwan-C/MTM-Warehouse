using MTM_Warehouse.Entities;
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(32)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

        public string? SelectedRole { get; set; }

        public int ManagerId { get; set; }

        public List<LoginEmp>? LoginEmps { get; set; }
    }
}
