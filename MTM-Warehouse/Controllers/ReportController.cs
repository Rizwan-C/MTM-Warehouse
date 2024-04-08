using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Models;
using MTM_Warehouse.Services;

namespace MTM_Warehouse.Controllers
{
    public class ReportController : Controller
    {
        private AllDbContext _context;
        private IWarehouseService _warehouseInfoService;
        private UserManager<User> _userManager;


        public ReportController(AllDbContext allDbContext, IWarehouseService warehouseInfoService, UserManager<User> userManager)
        {
            _context = allDbContext;
            _warehouseInfoService = warehouseInfoService;
            _userManager = userManager;
        }

       
        [HttpGet("/reports")]
        public IActionResult ReportPage() 
        {
            List<WarehouseInfo> warehouseInfo = _context.WarehouseInfo_DbData.ToList();
            List<WarehouseItems> warehouseItems = _context.WarehouseItems_DbData.ToList();

            var warehouseData = warehouseItems
                                    .GroupBy(item => item.WarehouseInfoId)
                                    .Select(group => new
                                    {
                                        WarehouseId = group.Key,
                                        ItemsCount = group.Count(),
                                        TotalCost = group.Sum(item => item.Item_total_cost)
                                    })
                                    .ToList();

            // Efficiently fetch warehouse names with a join
            var warehouseNames = from wd in warehouseData
                                 join wi in _context.WarehouseInfo_DbData on wd.WarehouseId equals wi.WarehouseInfoId
                                 select wi.W_Name;

            ViewBag.WarehouseNames = warehouseNames.ToList();

            // Prepare data for the charts
            ViewBag.WarehouseItemsCounts = warehouseData.Select(x => x.ItemsCount).ToList();
            ViewBag.WarehouseItemsCosts = warehouseData.Select(x => x.TotalCost).ToList();
            ViewBag.WarehouseInfo = warehouseInfo;


            return View();
        }

        //List<WarehouseInfo> warehouseInfo = _context.WarehouseInfo_DbData.ToList();
        //List<WarehouseItems> warehouseItems = _context.WarehouseItems_DbData.ToList();
        //List<EmpData> empDatas = _context.EmpData_DbData.ToList();
        //List<LoginEmp> loginEmps = _context.loginEmps_DbData.ToList();

        //ReportViewModel reportViewModel = new ReportViewModel();


        //ViewBag.W_Count = warehouseInfo.Count();
        //ViewBag.I_Count = warehouseItems.Count();
        //ViewBag.M_Count = loginEmps.Count();
        //ViewBag.E_Count = empDatas.Count();

        [HttpGet("reports/downloads")]
        public IActionResult DownloadPage()
        {
            List<WarehouseInfo> warehouseInfo = _context.WarehouseInfo_DbData.ToList();
            

           


            return View();
        }


    }
}
