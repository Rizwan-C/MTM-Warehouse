using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Services
{
    public class WarehouseService : IWarehouseService
    {
        public WarehouseInfo WarehousePercentFull(WarehouseInfo warehouseInfo)
        {                           
            double percentageFull = (double)((warehouseInfo.W_SpaceAvailable / warehouseInfo.W_TotalCapacity) * 100);
            warehouseInfo.W_PercentFull = Math.Truncate(percentageFull * 100) / 100;
            return warehouseInfo;
        }

        public WarehouseInfo WarehouseSpaceAvailable(WarehouseInfo warehouseInfo , double spaceOccupiedByItem)
        {
            warehouseInfo.W_SpaceAvailable = warehouseInfo.W_TotalCapacity - spaceOccupiedByItem;            
            return warehouseInfo;
        }
    }
}
