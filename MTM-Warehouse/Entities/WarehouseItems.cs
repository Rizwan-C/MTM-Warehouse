using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace MTM_Warehouse.Entities
{
    public class WarehouseItems
    {
        [Key]
        public int WarehouseItemsId { get; set; }

        [Required(ErrorMessage = "Please enter Item Name")]
        public string? Item_Name { get; set; }

        [Required(ErrorMessage = "Please enter Quntity")]
        public double? Item_Unit_Quant { get; set; }

        [Required(ErrorMessage = "Please enter Space required to store")]
        public double? Item_Capacity_Quant { get; set; }
        
        public double? Item_SpaceAccuired { get; set; }
        
        public int? WarehouseInfoId { get; set; }

        public WarehouseInfo? WarehouseInfo { get; set; }
    }
}
