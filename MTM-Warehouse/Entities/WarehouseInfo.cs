
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class WarehouseInfo
    {

        [Key]
        public int WarehouseInfoId { get; set; }

        [Required(ErrorMessage = "Enter valid warehouse name")]
        // ^([A-Za-z][A-Za-z0-9 -]{3,30})$
        public string? W_Name { get; set; }

        [Required(ErrorMessage = "Enter valid location")]
        public string? W_Location { get; set; }

        [Required(ErrorMessage = "Enter valid postalcode")]
        [RegularExpression("^(^[0-9]{5}(-[0-9]{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}[0-9]{1}[A-Z]{1} *[0-9]{1}[A-Z]{1}[0-9]{1}$)$", ErrorMessage = "Invalid postal code format. Rrefer this : 'N2M 5C7'")]
        public string? W_PinCode { get; set; }

        [Required(ErrorMessage = "Enter valid total capacity in terms of square footage (0.00)")] 
        public double? W_TotalCapacity { get; set; }

        
        public double? W_SpaceAvailable { get; set; }

        
        public double? W_PercentFull { get; set; }


        //public int EmpDataId { get; set; }
        public ICollection<WarehouseItems>? WarehouseItems { get; set; }

        public ICollection<EmpData>? EmpDatas { get; set; }

        public ICollection<LoginEmp>? loginEmps { get; set; }
    }
}
