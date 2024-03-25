using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class ApprovalJobs
    {

        [Key]
        public int ApprovalJobsId { get; set; }        

        [Required(ErrorMessage = "")]
        public string? TransportItem { get; set; }
        [Required(ErrorMessage = "")]
        public string? Item_Space { get; set; }
        [Required(ErrorMessage = "")]
        public string? From_Warehouse {  get; set; }
        [Required(ErrorMessage = "")]
        public string? To_Warehouse { get; set; }
        [Required(ErrorMessage = "")]
        public string? Item_Quantity { get; set; }
        [Required(ErrorMessage = "")]
        public string? Approval_Status { get; set; }

        

        public string? ApprovalsId { get; set; }

        public Approvals Approvals { get; set; }

    }
}
