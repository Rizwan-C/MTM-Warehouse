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
            WarehouseInfo? warehouseInfo = _context.WarehouseInfo_DbData.Find(id);
            List<LoginEmp> loginEmp = _context.loginEmps_DbData.Where(emp => emp.WarehouseInfoId == id).ToList();
            List<EmpData> empData = _context.EmpData_DbData.Where(emp => emp.WarehouseInfoId == id).ToList();
            List<WarehouseItems> allItems = _context.WarehouseItems_DbData.Where(items => items.WarehouseInfoId == id).ToList() ;

            WarehouseModel warehouseModel = new WarehouseModel()
            {
                warehouseInfoModelObj = warehouseInfo,
                loginEmpModelObj = loginEmp,
                empDataModelObj = empData, 
                allItemsModelObj = allItems
            };

            return View("WarehousePage", warehouseModel);
        }


    }
}
