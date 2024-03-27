using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MTM_Warehouse.Entities
{
    public class EmpData
    {
        [Key]
        public int EmpDataId { get; set; }

        [Required(ErrorMessage = "")]
        public double? Name { get; set; }

        [Required(ErrorMessage = "")]
        public double? Email { get; set; }

        [Required(ErrorMessage = "")]
        public double? Phone { get; set; }

        [Required(ErrorMessage = "")]
        public double? Address { get; set; }

        [Required(ErrorMessage = "")]
        public double? PinCode { get; set; }

        [Required(ErrorMessage = "")]
        public double? FirstDay { get; set; }

        [Required(ErrorMessage = "")]
        public double? LastDay { get; set; }

        
        public int? WarehouseInfoId { get; set; }
        public WarehouseInfo? WarehouseInfo { get; set; }

    }
}
