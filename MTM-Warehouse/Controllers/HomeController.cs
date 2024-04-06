using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Models;
using MTM_Warehouse.Services;
using System.Diagnostics;

namespace MTM_Warehouse.Controllers
{
    public class HomeController : Controller
    {
        private AllDbContext _context;
        public HomeController(AllDbContext allDbContext)
        {
            _context = allDbContext;
        }

        public IActionResult HomePage()
        {
            List<WarehouseInfo> warehouseInfos = _context.WarehouseInfo_DbData.ToList();
            if(warehouseInfos.Count == 0)
            {
                warehouseInfos.Add(new WarehouseInfo() 
                {
                    WarehouseInfoId = -1,
                });
            }    
            return View(warehouseInfos);
        }


        public IActionResult Logout()
        {
            return RedirectToAction("LoginPage", "Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
