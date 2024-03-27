using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class Approvals
    {


        [Key]
        public string? ApprovalsId { get; set; }        

        [Required(ErrorMessage = "")]
        public string? ApplicantId { get; set; }

        [Required(ErrorMessage = "")]
        public string? ApplicantName { get; set; }

        [Required(ErrorMessage = "")]
        public string? Message { get; set; }

        [Required(ErrorMessage = "")]
        public string? Approval_Job {  get; set; }



        public int LoginEmpId { get; set; }

        public LoginEmp LoginEmp { get; set; }


        public int ApprovalJobsId { get; set; }

        public ApprovalJobs ApprovalJobs { get; set; }


    }
}
