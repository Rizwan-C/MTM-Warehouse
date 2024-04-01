using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Models;
using MTM_Warehouse.Services;

namespace MTM_Warehouse.Controllers
{
    public class WarehouseController : Controller
    {

        private AllDbContext _context;
        private IWarehouseInfoService _warehouseInfoService;
        public WarehouseController(AllDbContext allDbContext, IWarehouseInfoService warehouseInfoService)
        {
            _context = allDbContext;
            _warehouseInfoService = warehouseInfoService;
        }


        [HttpGet("/view/warehouse")] 
        public IActionResult OpenWarehouse(int id)
        {
            WarehouseModel warehouseModel = new WarehouseModel();
            
            WarehouseInfo? warehouseInfo = _context.WarehouseInfo_DbData.Find(id);
            //LoginEmp loginEmp = _context.loginEmps_DbData.Where()

            return View("WarehousePage");
        }


    }
}
