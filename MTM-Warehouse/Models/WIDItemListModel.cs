using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class WIDItemListModel
    {
        public int W_ID { get; set; }

        public List<WarehouseItems>? warehouseItems { get; set; }
    }
}
