using Microsoft.EntityFrameworkCore;
using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class WarehouseModel
    {
        public WarehouseInfo? warehouseInfoModelObj {  get; set; }
        public List<LoginEmp>? loginEmpModelObj { get; set; }
        public List<EmpData>? empDataModelObj { get; set; }
        public List<WarehouseItems>? allItemsModelObj { get; set; }

    }
}
