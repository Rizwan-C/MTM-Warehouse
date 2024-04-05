using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class WIDEmpListModel
    {
        public int W_ID { get; set; }

        public List<EmpData>? empDatas { get; set; }
    }
}
