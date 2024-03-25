using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class LoginEmp
    {
        [Key]
        public int LoginEmpId { get; set; }

        [Required(ErrorMessage ="")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "")]
        public string? AccessRights { get; set; }

    }
}
