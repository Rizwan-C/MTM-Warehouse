using Microsoft.EntityFrameworkCore;
using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class ReportViewModel
    {
        public List<WarehouseInfo> warehouseInfo { get; set; }
        public List<WarehouseItems> warehouseItems { get; set; }
        public List<EmpData> empDatas { get; set; }
        public List<LoginEmp> loginEmps { get; set; }

        public int w_Id;
        public int i_Id;
        public int m_Id;
        public int e_Id;


    }
}
