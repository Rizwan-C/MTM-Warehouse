using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Services
{
    public interface IWarehouseInfoService
    {
        public WarehouseInfo WarehouseSpaceAvailable(WarehouseInfo warehouseInfo);
        public WarehouseInfo WarehousePercentFull(WarehouseInfo warehouseInfo);
    }
}
