using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MTM_Warehouse.Entities
{
    public class EmpData
    {
        [Key]
        public int EmpDataId { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter Phone #")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Invalid Phone #")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter Pincode")]
        public string? PinCode { get; set; }


        
        public int? WarehouseInfoId { get; set; }
        public WarehouseInfo? WarehouseInfo { get; set; }

    }
}
