
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class JobProgress
    {

        [Key]
        public int JobProgressId { get; set; }


        [Required(ErrorMessage = "")]
        public string? State { get; set; }




        public ICollection<ApprovalJobs>? ApprovalJobs { get; set; }

    }
}
