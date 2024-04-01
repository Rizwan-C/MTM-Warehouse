using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


        [HttpGet("/warehouse/{id}/addmanager")]
        public IActionResult AddManagerPage(int id)         
        {
            ManagerWarehouseModel managerWarehouseModel = new ManagerWarehouseModel();
            managerWarehouseModel.WarehouseInfo = _context.WarehouseInfo_DbData.Find(id);
           // managerWarehouseModel.LoginEmp.WarehouseInfo = managerWarehouseModel.WarehouseInfo;
            return View(managerWarehouseModel);
        }


        [HttpPost("/warehouse/addmanager/hello")]
        public IActionResult AddManager(ManagerWarehouseModel managerWarehouseModel)
        {
            Console.WriteLine("WarehouseController :: POST : AddManager()");
            managerWarehouseModel.LoginEmp.WarehouseInfoId = managerWarehouseModel.WarehouseInfo.WarehouseInfoId;
            Console.WriteLine("> ", managerWarehouseModel.LoginEmp.WarehouseInfoId);
            //managerWarehouseModel.LoginEmp.WarehouseInfoId = managerWarehouseModel.WarehouseInfo.WarehouseInfoId;
            //managerWarehouseModel.LoginEmp.WarehouseInfo = _context.WarehouseInfo_DbData.Find(managerWarehouseModel.WarehouseInfo.WarehouseInfoId);

            // Clear ModelState errors related to WarehouseInfo
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("WarehouseInfo")).ToList())
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {                

                _context.loginEmps_DbData.Add(managerWarehouseModel.LoginEmp);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"The Manager \"{managerWarehouseModel.LoginEmp.Name}\" was added sucessfully.";

                return RedirectToAction("OpenWarehouse", new { id = managerWarehouseModel.WarehouseInfo?.WarehouseInfoId });
            }
            return View("AddManagerPage", managerWarehouseModel);
        }

    }
}




//var allWarehouseInfoForManager = _context.loginEmps_DbData
//    .Include(w => w.WarehouseInfoId)
//    .Where(w => w.WarehouseInfoId == managerWarehouseModel.WarehouseInfo.WarehouseInfoId)
//    .FirstOrDefault();