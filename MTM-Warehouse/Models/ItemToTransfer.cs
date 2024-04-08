namespace MTM_Warehouse.Models
{
    public class ItemToTransfer
    {
        public int WarehouseItemId { get; set; }
        public string ItemName { get; set; }
        public double QuantityAvailable { get; set; }
        public double QuantityToTransfer { get; set; }
    }
}
