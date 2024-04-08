using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Models;
using MTM_Warehouse.Services;
using System.Text;

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
            DownloadReportViewModel downloadReportViewModel = new DownloadReportViewModel()
            { 
                warehouseInfo = warehouseInfo
            };

            return View(downloadReportViewModel);
        }

        [HttpGet]
        public IActionResult DownloadWarehouseReport()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Warehouse Report");
                worksheet.Cell("A1").Value = "Name";
                worksheet.Cell("B1").Value = "Location";
                worksheet.Cell("C1").Value = "PinCode";
                worksheet.Cell("D1").Value = "TotalCapacity";
                worksheet.Cell("E1").Value = "SpaceAvailable";
                worksheet.Cell("F1").Value = "PercentFull";
                worksheet.Cell("G1").Value = "ItemCount";
                worksheet.Cell("H1").Value = "TotalCostOfAllItems";

                // Fetch all warehouse data
                var data = _context.WarehouseInfo_DbData
                    .Select(warehouse => new
                    {
                        warehouse.W_Name,
                        warehouse.W_Location,
                        warehouse.W_PinCode,
                        warehouse.W_TotalCapacity,
                        warehouse.W_SpaceAvailable,
                        warehouse.W_PercentFull,
                        ItemCount = warehouse.WarehouseItems.Count,
                        TotalCost = warehouse.WarehouseItems.Sum(item => item.Item_total_cost ?? 0) 
                    })
                    .ToList();

                int currentRow = 2; 
                foreach (var warehouse in data)
                {
                    worksheet.Cell(currentRow, 1).Value = warehouse.W_Name;
                    worksheet.Cell(currentRow, 2).Value = warehouse.W_Location;
                    worksheet.Cell(currentRow, 3).Value = warehouse.W_PinCode;
                    worksheet.Cell(currentRow, 4).Value = warehouse.W_TotalCapacity;
                    worksheet.Cell(currentRow, 5).Value = warehouse.W_SpaceAvailable;
                    worksheet.Cell(currentRow, 6).Value = warehouse.W_PercentFull;
                    worksheet.Cell(currentRow, 7).Value = warehouse.ItemCount;
                    worksheet.Cell(currentRow, 8).Value = warehouse.TotalCost;
                    currentRow++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WarehouseReport_All.xlsx");
                }
            }
        }



        [HttpGet]
        public IActionResult DownloadItemsReport(int? warehouseId)
        {
            if (!warehouseId.HasValue)
            {
                // Handle the case where no warehouse is selected or return an error message.
                return Content("Please select a warehouse.");
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Items Report");
                worksheet.Cell("A1").Value = "Item Name";
                worksheet.Cell("B1").Value = "Quantity";
                worksheet.Cell("C1").Value = "Space per Unit";
                worksheet.Cell("D1").Value = "Total Space Acquired"; // New column for Item_SpaceAccuired
                worksheet.Cell("E1").Value = "Cost Per Unit";
                worksheet.Cell("F1").Value = "Total Cost";

                // Fetch items for the selected warehouse
                var items = _context.WarehouseItems_DbData
                                    .Where(item => item.WarehouseInfoId == warehouseId)
                                    .ToList();

                int currentRow = 2; // Start from the second row; first row is for headers
                foreach (var item in items)
                {
                    worksheet.Cell(currentRow, 1).Value = item.Item_Name;
                    worksheet.Cell(currentRow, 2).Value = item.Item_Unit_Quant;
                    worksheet.Cell(currentRow, 3).Value = item.Item_Capacity_Quant;
                    worksheet.Cell(currentRow, 4).Value = item.Item_SpaceAccuired; 
                    worksheet.Cell(currentRow, 5).Value = item.Item_price_per_unit;
                    worksheet.Cell(currentRow, 6).Value = item.Item_total_cost;
                    currentRow++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ItemsReport_Warehouse{warehouseId}.xlsx");
                }
            }
        }
   

        [HttpGet]
        public IActionResult DownloadManagersReport()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Managers Report");
                worksheet.Cell("A1").Value = "Name";
                worksheet.Cell("B1").Value = "Email";
                worksheet.Cell("C1").Value = "Role";
                worksheet.Cell("D1").Value = "Username";
                worksheet.Cell("E1").Value = "Warehouse";

                var managersData = _context.loginEmps_DbData
                    .Where(emp => emp.Role.Contains("user")) 
                    .Select(emp => new
                    {
                        emp.Name,
                        emp.Email,
                        emp.Role,
                        emp.Username,
                        WarehouseName = emp.WarehouseInfo.W_Name 
                    })
                    .ToList();

                int currentRow = 2; 
                foreach (var manager in managersData)
                {
                    worksheet.Cell(currentRow, 1).Value = manager.Name;
                    worksheet.Cell(currentRow, 2).Value = manager.Email;
                    worksheet.Cell(currentRow, 3).Value = manager.Role;
                    worksheet.Cell(currentRow, 4).Value = manager.Username;
                    worksheet.Cell(currentRow, 5).Value = manager.WarehouseName;
                    currentRow++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ManagersReport.xlsx");
                }
            }
        }


        [HttpGet]
        public IActionResult DownloadEmployeeReport(int? warehouseId)
        {
            if (!warehouseId.HasValue)
            {
                // Handle the case where no warehouse is selected or return an error message.
                return Content("Please select a warehouse.");
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees Report");
                worksheet.Cell("A1").Value = "Name";
                worksheet.Cell("B1").Value = "Email";
                worksheet.Cell("C1").Value = "Phone";
                worksheet.Cell("D1").Value = "Address";
                worksheet.Cell("E1").Value = "PinCode";
                worksheet.Cell("F1").Value = "Warehouse";

                // Fetch employees for the selected warehouse
                var employees = _context.EmpData_DbData
                    .Where(emp => emp.WarehouseInfoId == warehouseId)
                    .Select(emp => new
                    {
                        emp.Name,
                        emp.Email,
                        emp.Phone,
                        emp.Address,
                        emp.PinCode,
                        WarehouseName = emp.WarehouseInfo.W_Name
                    })
                    .ToList();

                int currentRow = 2; // Start from the second row; first row is for headers
                foreach (var emp in employees)
                {
                    worksheet.Cell(currentRow, 1).Value = emp.Name;
                    worksheet.Cell(currentRow, 2).Value = emp.Email;
                    worksheet.Cell(currentRow, 3).Value = emp.Phone;
                    worksheet.Cell(currentRow, 4).Value = emp.Address;
                    worksheet.Cell(currentRow, 5).Value = emp.PinCode;
                    worksheet.Cell(currentRow, 6).Value = emp.WarehouseName;
                    currentRow++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"EmployeesReport_Warehouse{warehouseId}.xlsx");
                }
            }
        }



}
}
