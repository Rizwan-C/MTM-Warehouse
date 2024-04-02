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
        private IWarehouseService _warehouseInfoService;

        public WarehouseController(AllDbContext allDbContext, IWarehouseService warehouseInfoService)
        {
            _context = allDbContext;
            _warehouseInfoService = warehouseInfoService;
        }


        [HttpGet("/view/warehouse/{id}")] 
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
            EmployeeWarehouseModel employeeWarehouseModel = new EmployeeWarehouseModel();
            employeeWarehouseModel.WarehouseInfo = _context.WarehouseInfo_DbData.Find(id);
            return View(employeeWarehouseModel);
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

                TempData["LastActionMessage"] = $"The Employee \"{employeeWarehouseModel.EmpData.Name}\" was added sucessfully.";

                employeeWarehouseModel.EmpData = new EmpData();

                return RedirectToAction("AddEmployeePage", new {id = employeeWarehouseModel.WarehouseInfo.WarehouseInfoId} ); 
            }

            return View("AddEmployeePage");
        }



        [HttpGet("/warehouse/{id}/additem")]
        public IActionResult AddItemPage(int id)
        {
            ItemWarehouseModel itemWarehouseModel = new ItemWarehouseModel();
            itemWarehouseModel.WarehouseInfo = _context.WarehouseInfo_DbData.Find(id);
            return View(itemWarehouseModel);
        }

        [HttpPost("/warehouse/additem")]
        public IActionResult AddItem(ItemWarehouseModel itemWarehouseModel)
        {
            Console.WriteLine("WarehouseController :: POST : AddItem()");
            itemWarehouseModel.WarehouseItems.WarehouseInfoId = itemWarehouseModel.WarehouseInfo.WarehouseInfoId;
            Console.WriteLine("W-ID > ", itemWarehouseModel.WarehouseItems.WarehouseInfoId);

            // Clear ModelState errors related to WarehouseInfo
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("WarehouseInfo")).ToList())
            {
                ModelState.Remove(key);
            }

            itemWarehouseModel.WarehouseInfo = _context.WarehouseInfo_DbData.Find(itemWarehouseModel.WarehouseInfo.WarehouseInfoId);
            itemWarehouseModel.WarehouseItems.Item_SpaceAccuired = itemWarehouseModel.WarehouseItems.Item_Capacity_Quant * itemWarehouseModel.WarehouseItems.Item_Unit_Quant;
            _warehouseInfoService.WarehouseSpaceAvailable(itemWarehouseModel.WarehouseInfo, (double)itemWarehouseModel.WarehouseItems.Item_SpaceAccuired);
            _warehouseInfoService.WarehousePercentFull(itemWarehouseModel.WarehouseInfo);

            if (ModelState.IsValid)
            {
                _context.WarehouseInfo_DbData.Update(itemWarehouseModel.WarehouseInfo);

                _context.WarehouseItems_DbData.Add(itemWarehouseModel.WarehouseItems);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"Item - \"{itemWarehouseModel.WarehouseItems.Item_Name}\" was added sucessfully.";

                itemWarehouseModel.WarehouseItems = new WarehouseItems();

                return RedirectToAction("AddItemPage", new { id = itemWarehouseModel.WarehouseInfo.WarehouseInfoId });
            }

            return View("AddEmployeePage");
        }


    }
}


