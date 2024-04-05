using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Models
{
    public class WIDManagerListModel
    {
        public int W_ID { get; set; }

        public List<LoginEmp>? managerList { get; set; }
    }
}
