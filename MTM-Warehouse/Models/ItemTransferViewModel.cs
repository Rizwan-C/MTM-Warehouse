using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class ItemTransferViewModel
    {
        public int? SourceWarehouseId { get; set; }
        public int? DestinationWarehouseId { get; set; }
        public List<WarehouseInfo> Warehouses { get; set; } = new List<WarehouseInfo>();
        public List<ItemToTransfer> ItemsToTransfer { get; set; } = new List<ItemToTransfer>();

    }
}
