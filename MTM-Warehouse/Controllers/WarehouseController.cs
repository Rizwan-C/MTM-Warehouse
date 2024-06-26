﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<User> _userManager;            
        

        public WarehouseController(AllDbContext allDbContext, IWarehouseService warehouseInfoService, UserManager<User> userManager)
        {
            _context = allDbContext;
            _warehouseInfoService = warehouseInfoService;
            _userManager = userManager;
        }


        [HttpGet("/view/warehouse/{id}")] 
        public IActionResult OpenWarehouse(int id)
        {            
            WarehouseInfo? warehouseInfo = _context.WarehouseInfo_DbData.Find(id);
            List<LoginEmp> loginEmp = _context.loginEmps_DbData.Where(emp => emp.WarehouseInfoId == id).ToList();
            List<EmpData> empData = _context.EmpData_DbData.Where(emp => emp.WarehouseInfoId == id).ToList();
            List<WarehouseItems> allItems = _context.WarehouseItems_DbData.Where(items => items.WarehouseInfoId == id).ToList() ;
            double total = 0;
            foreach (WarehouseItems item in allItems)
            {
                total += (double)item.Item_SpaceAccuired;
            }
            

            WarehouseModel warehouseModel = new WarehouseModel()
            {
                warehouseInfoModelObj = warehouseInfo,
                loginEmpModelObj = loginEmp,
                empDataModelObj = empData, 
                allItemsModelObj = allItems
            };

            warehouseInfo = _warehouseInfoService.WarehouseSpaceAvailable(warehouseInfo, total);
            warehouseInfo = _warehouseInfoService.WarehousePercentFull(warehouseInfo);
            _context.WarehouseInfo_DbData.Update(warehouseInfo);
            _context.SaveChanges();

            return View("WarehousePage", warehouseModel);
        }

        // -- ALL MANAGER ITEMS --
        // --       STARTS      --
        [HttpGet("/warehouse/{id}/addmanager")]
        [Authorize(Roles = "super_user")]
        public IActionResult AddManagerPage(int id)
        {
            ManagerWarehouseModel managerWarehouseModel = new ManagerWarehouseModel();
            managerWarehouseModel.WarehouseInfo = _context.WarehouseInfo_DbData.Find(id);
           // managerWarehouseModel.LoginEmp.WarehouseInfo = managerWarehouseModel.WarehouseInfo;
            return View(managerWarehouseModel);
        }


        [HttpPost("/warehouse/addmanager")]
        [Authorize(Roles = "super_user")]
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

                TempData["LastActionMessage"] = $"The Manager \"{managerWarehouseModel.LoginEmp.Name}\" added sucessfully.";

                return RedirectToAction("OpenWarehouse", new { id = managerWarehouseModel.WarehouseInfo?.WarehouseInfoId });
            }
            return View("AddManagerPage", managerWarehouseModel);
        }


        [HttpGet("/warehouse/manager/{id}")]
        [Authorize(Roles = "super_user")]
        public IActionResult ViewManagerPage(int id)
        {
            List<LoginEmp> ManagerList = _context.loginEmps_DbData
                .Where(emp => emp.WarehouseInfoId == id).Include(emp => emp.WarehouseInfo)
                .ToList();

            WIDManagerListModel model = new WIDManagerListModel()
            {
                managerList = ManagerList,
                W_ID = id
            };

            return View(model);
        }

        [HttpGet("/warehouse/edit/manager/{id}")]
        [Authorize(Roles = "super_user")]
        public IActionResult EditManagerPage(int id)
        {
            LoginEmp manager = _context.loginEmps_DbData.Where(i => i.LoginEmpId == id).Include(emp => emp.WarehouseInfo).FirstOrDefault();
            return View(manager);
        }

        [HttpPost("/warehouse/edit/manager")]
        [Authorize(Roles = "super_user")]
        public IActionResult EditManager(LoginEmp manager)
        {
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("WarehouseInfo")).ToList())
            {
                ModelState.Remove(key);
            }
            if (ModelState.IsValid)
            {
                _context.Entry(manager.WarehouseInfo).State = EntityState.Unchanged;
                _context.loginEmps_DbData.Update(manager);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"The Manager \"{manager.Name}\" edited successfully.";

                return RedirectToAction("ViewManagerPage", new { id = manager.WarehouseInfoId });
            }

            return View("EditManagerPage", manager);
        }

        [HttpPost("/warehouse/{id}/deletemanager")]
        [Authorize(Roles = "super_user")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var manager = _context.loginEmps_DbData.Find(id);

            if (manager.Username != null)
            {
                string? username = manager.Username;
                _context.loginEmps_DbData.Remove(manager);
                _context.SaveChanges();

                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        Console.WriteLine("Username : '" + username + "' has been deleted successfully"); // Assuming you have a UserList action to redirect to
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine("", error.Description);
                        }
                    }
                }

            }
            else
            {
                _context.loginEmps_DbData.Remove(manager);
                _context.SaveChanges();
            }

            TempData["LastActionMessage"] = $"The Manager \"{manager.Name}\" deleted successfully.";

            return RedirectToAction("ViewManagerPage", new { id = manager.WarehouseInfoId });
        }



        // --        ENDS       --
        // -- ALL MANAGER ITEMS --



        // -- ALL EMPLOYEE ITEMS --
        // --       STARTS      --

        [HttpGet("/warehouse/{id}/addemployee")]
        [Authorize(Roles = "super_user,user")]
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

                TempData["LastActionMessage"] = $"The Employee \"{employeeWarehouseModel.EmpData.Name}\" added sucessfully.";

                employeeWarehouseModel.EmpData = new EmpData();

                return RedirectToAction("AddEmployeePage", new {id = employeeWarehouseModel.WarehouseInfo.WarehouseInfoId} ); 
            }

            return View("AddEmployeePage", employeeWarehouseModel);
        }


        [HttpGet("/warehouse/Employee/{id}")]
        [Authorize(Roles = "super_user,user")]
        public IActionResult ViewEmployeePage(int id)
        {
            List<EmpData> EmpList = _context.EmpData_DbData
                .Where(emp => emp.WarehouseInfoId == id)
                .ToList();

            WIDEmpListModel model = new WIDEmpListModel()
            {
                empDatas = EmpList,
                W_ID = id
            };

            return View(model);
        }

        [HttpGet("/warehouse/edit/employee/{id}")]
        [Authorize(Roles = "super_user,user")]
        public IActionResult EditEmployeePage(int id)
        {
            EmpData manager = _context.EmpData_DbData.Where(i => i.EmpDataId == id).Include(emp => emp.WarehouseInfo).First();
            return View(manager);
        }

        [HttpPost("/warehouse/edit/employee")]
        [Authorize(Roles = "super_user,user")]
        public IActionResult EditEmployee(EmpData employee)
        {
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("WarehouseInfo")).ToList())
            {
                ModelState.Remove(key);
            }
            if (ModelState.IsValid)
            {
                _context.Entry(employee.WarehouseInfo).State = EntityState.Unchanged;
                _context.EmpData_DbData.Update(employee);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"The Manager \"{employee.Name}\" edited successfully.";

                return RedirectToAction("ViewEmployeePage", new { id = employee.WarehouseInfoId });
            }


            return View("EditEmployeePage", employee);
        }


        [HttpPost("/warehouse/{id}/deletemployee")]
        [Authorize(Roles = "super_user,user")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.EmpData_DbData.Find(id);
            _context.EmpData_DbData.Remove(employee);
            _context.SaveChanges();

            TempData["LastActionMessage"] = $"The Employee \"{employee.Name}\" deleted successfully.";

            return RedirectToAction("ViewEmployeePage", new { id = employee.WarehouseInfoId });
        }


        // --        ENDS       --
        // -- ALL EMPLOYEE ITEMS --


        // -- ALL ITEM ITEMS --
        // --    STARTS     --

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
            WarehouseInfo warehouseInfo = _context.WarehouseInfo_DbData.Find(itemWarehouseModel.WarehouseInfo.WarehouseInfoId);

            // Clear ModelState errors related to WarehouseInfo
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("WarehouseInfo")).ToList())
            {
                ModelState.Remove(key);
            }


            if (warehouseInfo.W_SpaceAvailable < (itemWarehouseModel.WarehouseItems.Item_Capacity_Quant * itemWarehouseModel.WarehouseItems.Item_Unit_Quant))
            {
                ModelState.AddModelError("WarehouseCapacity", "There is not enough space in the warehouse for the requested quantity of items.");
                return View("AddItemPage", itemWarehouseModel);
            }

            Console.WriteLine("WarehouseController :: POST : AddItem()");
            itemWarehouseModel.WarehouseItems.WarehouseInfoId = itemWarehouseModel.WarehouseInfo.WarehouseInfoId;
            Console.WriteLine("W-ID > ", itemWarehouseModel.WarehouseItems.WarehouseInfoId);

            // Check if the item already exists in the database
            var existingItem = _context.WarehouseItems_DbData
                .FirstOrDefault(wi => wi.Item_Name == itemWarehouseModel.WarehouseItems.Item_Name && wi.WarehouseInfoId == warehouseInfo.WarehouseInfoId);

            if (existingItem != null)
            {
                //existingItem.Item_Unit_Quant += itemWarehouseModel.WarehouseItems.Item_Unit_Quant; // Update quantity or other fields as needed
                //_warehouseInfoService.TotalPrice(existingItem); // Recalculate the total price if necessary

                ModelState.AddModelError("ItemExists", "An item with this name already exists in the selected warehouse.");
                return View("AddItemPage", itemWarehouseModel);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    itemWarehouseModel.WarehouseInfo = _context.WarehouseInfo_DbData.Find(itemWarehouseModel.WarehouseInfo.WarehouseInfoId);
                    itemWarehouseModel.WarehouseItems.Item_SpaceAccuired = itemWarehouseModel.WarehouseItems.Item_Capacity_Quant * itemWarehouseModel.WarehouseItems.Item_Unit_Quant;
                    itemWarehouseModel.WarehouseItems = _warehouseInfoService.TotalPrice(itemWarehouseModel.WarehouseItems);

                    _context.WarehouseInfo_DbData.Update(itemWarehouseModel.WarehouseInfo);
                    _context.WarehouseItems_DbData.Add(itemWarehouseModel.WarehouseItems);
                    _context.SaveChanges();

                    TempData["LastActionMessage"] = $"Item - \"{itemWarehouseModel.WarehouseItems.Item_Name}\" added sucessfully.";

                    itemWarehouseModel.WarehouseItems = new WarehouseItems();

                    return RedirectToAction("AddItemPage", new { id = itemWarehouseModel.WarehouseInfo.WarehouseInfoId });
                }
            }

            

            return View("AddItemPage", itemWarehouseModel);
        }


        [HttpGet("/warehouse/Item/{id}")]
        public IActionResult ViewItemsPage(int id)
        {
            List<WarehouseItems> ItemsList = _context.WarehouseItems_DbData
                .Where(emp => emp.WarehouseInfoId == id)
                .ToList();

            WIDItemListModel model = new WIDItemListModel()
            {
                warehouseItems = ItemsList,
                W_ID = id
            };

            return View(model);
        }

        [HttpGet("/warehouse/edit/item/{id}")]
        public IActionResult EditItemPage(int id)
        {

            WarehouseItems item = _context.WarehouseItems_DbData.Where(i => i.WarehouseItemsId == id).Include(emp => emp.WarehouseInfo).First();

            WarehouseInfo warehouseInfo = _context.WarehouseInfo_DbData.Find(item.WarehouseInfoId);           
             
            List<WarehouseItems> allItems = _context.WarehouseItems_DbData.Where(items => items.WarehouseInfoId == item.WarehouseInfoId).ToList();
            double total = 0;
            foreach (WarehouseItems itm in allItems)
            {
                total += (double)itm.Item_SpaceAccuired;
            }
            warehouseInfo = _warehouseInfoService.WarehouseSpaceAvailable(warehouseInfo, total);
            warehouseInfo = _warehouseInfoService.WarehousePercentFull(warehouseInfo);
            
            _context.WarehouseInfo_DbData.Update(warehouseInfo);
            _context.SaveChanges();

            return View(item);
        }

        [HttpPost("/warehouse/edit/Item")]
        public IActionResult EditItem(WarehouseItems items)
        {
            WarehouseInfo warehouseInfo = _context.WarehouseInfo_DbData.Find(items.WarehouseInfoId);
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("WarehouseInfo")).ToList())
            {
                ModelState.Remove(key);
            }
            if (warehouseInfo.W_SpaceAvailable < (items.Item_Capacity_Quant * items.Item_Unit_Quant))
            {
                ModelState.AddModelError("WarehouseCapacity", "There is not enough space in the warehouse for the requested quantity of items.");
                return View("EditItemPage", items);
            }
            // Detach the warehouseInfo entity to prevent tracking conflicts
            _context.Entry(warehouseInfo).State = EntityState.Detached;

            items.Item_SpaceAccuired = items.Item_Unit_Quant * items.Item_Capacity_Quant;

            
            if (ModelState.IsValid)
            {
                items = _warehouseInfoService.TotalPrice(items);
                _context.WarehouseItems_DbData.Update(items);
                _context.SaveChanges();

                TempData["LastActionMessage"] = $"Item \"{items.Item_Name}\" edited successfully.";

                return RedirectToAction("ViewItemsPage", new { id = items.WarehouseInfoId });
            }

            return View("EditItemPage", items);
        }

        

        [HttpPost("/warehouse/{id}/deleteItem")]
        public IActionResult DeleteItem(int id)
        {
            var item = _context.WarehouseItems_DbData.Find(id);
            _context.WarehouseItems_DbData.Remove(item);
            _context.SaveChanges();

            TempData["LastActionMessage"] = $"The Employee \"{item.Item_Name}\" deleted successfully.";

            return RedirectToAction("ViewItemsPage", new { id = item.WarehouseInfoId });
        }

        // --    ENDS     --
        // -- ALL ITEM ITEMS --
        
        
    }
}


