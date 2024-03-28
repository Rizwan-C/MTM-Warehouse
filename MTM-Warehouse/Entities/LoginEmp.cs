using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class LoginEmp
    {
        [Key]
        public int LoginEmpId { get; set; }

        
        public string? Name { get; set; }

        
        public string? Email { get; set; }

        [Required(ErrorMessage = "Needs Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Needs Password")]
        public string? Password { get; set; }

        
        public string? AccessRights { get; set; }


        public ICollection<Approvals>? Approvals { get; set; }
    }
}
