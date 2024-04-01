using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Services
{
    public class WarehouseInfoService : IWarehouseInfoService
    {
        public WarehouseInfo WarehousePercentFull(WarehouseInfo warehouseInfo)
        {                           
            double percentageFull = (double)((warehouseInfo.W_SpaceAvailable / warehouseInfo.W_TotalCapacity) * 100);
            warehouseInfo.W_PercentFull = Math.Truncate(percentageFull * 100) / 100;
            return warehouseInfo;
        }

        public WarehouseInfo WarehouseSpaceAvailable(WarehouseInfo warehouseInfo)
        {

            double percentageFull = (double)((warehouseInfo.W_SpaceAvailable / warehouseInfo.W_TotalCapacity) * 100);
            warehouseInfo.W_PercentFull = Math.Truncate(percentageFull * 100) / 100;
            return warehouseInfo;
        }
    }
}
