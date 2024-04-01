using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class WarehouseModel
    {
        public WarehouseInfo warehouseInfo {  get; set; }
        public LoginEmp LoginEmp { get; set; }

        public WarehouseItems wearehouseItems { get; set; }

        public EmpData empData { get; set; }

    }
}
