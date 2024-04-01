using MTM_Warehouse.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MTM_Warehouse.Models
{
    public class ListActionPageModel
    {

        private AllDbContext _context;
        public ListActionPageModel(AllDbContext allDbContext)
        {
            _context = allDbContext;
        }

        public int WarehouseCount()
        {            
            return _context.WarehouseInfo_DbData.ToList().Count;
        }

        int AllWarehouseCount 
        { 
            get 
            {
                int? count = WarehouseCount();
                return count ?? 0;
            }
            
        }
            

        public int LoginEmpCount()
        {
            return _context.loginEmps_DbData.ToList().Count;
        }

        int AllLoginEmpCount
        {
            get
            {
                int? count = LoginEmpCount();
                return count ?? 0;
            }
            
        }


        public int EmpDataCount()
        {
            return _context.loginEmps_DbData.ToList().Count;
        }

        int AllEmpDataCount
        {
            get
            {
                int? count = EmpDataCount();
                return count ?? 0;
            }
            
        }


    }
}
