using Microsoft.EntityFrameworkCore;

namespace MTM_Warehouse.Services
{
    public class ListPageCountService
    {
        public int w_count;
        public int r_count;
        public int e_count;

        public ListPageCountService(int w_count, int r_count, int e_count) 
        {
            this.w_count = w_count;
            this.r_count = r_count;
            this.e_count = e_count;
        }

    }
}
