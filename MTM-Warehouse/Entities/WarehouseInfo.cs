using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class WarehouseInfo
    {

        [Key]
        public int WarehouseInfoId { get; set; }

        [Required(ErrorMessage = "")]
        public string? W_Name { get; set; }

        [Required(ErrorMessage = "")]
        public string? W_Location { get; set; }

        [Required(ErrorMessage = "")]
        public string? W_PinCode { get; set; }

        [Required(ErrorMessage = "")]
        public double? W_TotalCapacity { get; set; }

        [Required(ErrorMessage = "")]
        public double? W_SpaceAvailable { get; set; }

        [Required(ErrorMessage = "")]
        public double? W_PercentFull { get; set; }


    }
}
