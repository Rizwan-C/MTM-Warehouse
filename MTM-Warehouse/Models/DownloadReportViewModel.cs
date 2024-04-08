using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class DownloadReportViewModel
    {
        public int? SelectedWarehouseId { get; set; }

        public List<WarehouseInfo> warehouseInfo { get; set; }
    }
}
