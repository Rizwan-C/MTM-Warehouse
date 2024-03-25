using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class JobProgress
    {

        [Key]
        public int JobProgressid { get; set; }


        [Required(ErrorMessage = "")]
        public string? State { get; set; }


        public int ApprovalJobsId { get; set; }
        
        public ApprovalJobs ApprovalJobs { get; set; }

    }
}
