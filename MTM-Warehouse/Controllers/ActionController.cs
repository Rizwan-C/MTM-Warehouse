using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Services;

namespace MTM_Warehouse.Controllers
{
    public class ActionController : Controller
    {

        private AllDbContext _context;
        private IWarehouseService _warehouseInfoService;
        public ActionController(AllDbContext allDbContext, IWarehouseService warehouseInfoService)
        {
            _context = allDbContext;
            _warehouseInfoService = warehouseInfoService;
        }


        [HttpGet("/app/viewActions")]
        public IActionResult ListActions()
        {
            int w_count = _context.WarehouseInfo_DbData.ToList().Count;
            int r_count = _context.loginEmps_DbData.ToList().Count;
            int e_count = _context.loginEmps_DbData.ToList().Count;

            ListPageCountService wre_count = new ListPageCountService(w_count, r_count, e_count); 

            return View(wre_count);
        }

        [HttpGet("/manage/addWareHouse")]
        public IActionResult AddWarehousePage() {         
            return View(new WarehouseInfo());
        }


        [HttpPost("/manage/addWareHouse")]
        public IActionResult AddWarehouseInfo( WarehouseInfo warehouseInfo)
        {
            warehouseInfo.W_SpaceAvailable = warehouseInfo.W_TotalCapacity;
            _warehouseInfoService.WarehousePercentFull(warehouseInfo);

            if(ModelState.IsValid)
            {
                _context.WarehouseInfo_DbData.Add(warehouseInfo);
                _context.SaveChanges();

                TempData["LastActionMessage"] = "Warehouse added sucessfully !";

                return RedirectToAction("ListActions", "Action");
            }
            else 
            { 
                return View("AddWarehousePage", warehouseInfo); 
            }
        }

    }
}
