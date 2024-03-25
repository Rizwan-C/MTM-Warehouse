using System.ComponentModel.DataAnnotations;

namespace MTM_Warehouse.Entities
{
    public class WarehouseItems
    {
        [Key]
        public int WarehouseItemsId { get; set; }

        [Required(ErrorMessage = "")]
        public string? Item_Name { get; set; }

        [Required(ErrorMessage = "")]
        public double? Item_Unit_Quant { get; set; }

        [Required(ErrorMessage = "")]
        public double Item_Capacity_Quant { get; set; }

        [Required(ErrorMessage = "")]
        public string? Item_SpaceAccuired { get; set; }

        
        public int? WarehouseInfoId { get; set; }

        public WarehouseInfo? WarehouseInfo { get; set; }
    }
}
