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


        [HttpPost("/warehouse/addmanager")]
        public IActionResult AddManager(ManagerWarehouseModel managerWarehouseModel)
        {
            Console.WriteLine("WarehouseController :: POST : AddManager()");
            managerWarehouseModel.LoginEmp.WarehouseInfoId = managerWarehouseModel.WarehouseInfo.WarehouseInfoId;
            Console.WriteLine("> ", managerWarehouseModel.LoginEmp.WarehouseInfoId);

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


        [HttpGet("/warehouse/{id}/addemployee")]
        public IActionResult AddEmployeePage(int id)
        {
            EmployeeWarehouseModel managerWarehouseModel = new EmployeeWarehouseModel();
            managerWarehouseModel.WarehouseInfo = _context.WarehouseInfo_DbData.Find(id);
            return View(managerWarehouseModel);
        }

        [HttpPost("/warehouse/addemployee")]
        public IActionResult AddEmployee(EmployeeWarehouseModel employeeWarehouseModel)
        {
            Console.WriteLine("WarehouseController :: POST : AddEmployee()");
            employeeWarehouseModel.EmpData.WarehouseInfoId = employeeWarehouseModel.WarehouseInfo.WarehouseInfoId;
            Console.WriteLine("W-ID > ", employeeWarehouseModel.EmpData.WarehouseInfoId);

            // Clear ModelState errors related to WarehouseInfo
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("WarehouseInfo")).ToList())
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                _context.EmpData_DbData.Add(employeeWarehouseModel.EmpData);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"The Manager \"{employeeWarehouseModel.EmpData.Name}\" was added sucessfully.";

                employeeWarehouseModel.EmpData = new EmpData();

                return RedirectToAction("OpenWarehouse", employeeWarehouseModel ); //new { employeeWarehouseModel = employeeWarehouseModel }
            }

            return View("AddEmployeePage");
        }


    }
}




//var allWarehouseInfoForManager = _context.loginEmps_DbData
//    .Include(w => w.WarehouseInfoId)
//    .Where(w => w.WarehouseInfoId == managerWarehouseModel.WarehouseInfo.WarehouseInfoId)
//    .FirstOrDefault();