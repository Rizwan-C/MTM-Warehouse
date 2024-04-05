using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Services
{
    public interface IWarehouseService
    {
        public WarehouseInfo WarehouseSpaceAvailable(WarehouseInfo warehouseInfo, double spaceOccupiedByItem);
        public WarehouseInfo WarehousePercentFull(WarehouseInfo warehouseInfo);
    }
}
