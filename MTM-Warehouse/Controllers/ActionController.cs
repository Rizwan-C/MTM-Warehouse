using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Models;
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
        [Authorize(Roles = "super_user")]
        public IActionResult AddWarehousePage() {         
            return View(new WarehouseInfo());
        }


        [HttpPost("/manage/addWareHouse")]
        [Authorize(Roles = "super_user")]
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




        public IActionResult TransferItems()
        {
            var viewModel = new ItemTransferViewModel
            {
                Warehouses = _context.WarehouseInfo_DbData.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ConfirmTransfer(ItemTransferViewModel model)
        {
            if (model.ItemsToTransfer.Count > 1)
            {
                foreach (var item in model.ItemsToTransfer)
                {
                    var previousItemWarehouse = _context.WarehouseItems_DbData.Find(item.WarehouseItemId);
                    if (previousItemWarehouse != null)
                    {
                        previousItemWarehouse.Item_Unit_Quant -= item.QuantityToTransfer;
                        _warehouseInfoService.TotalPrice(previousItemWarehouse);

                        var existingItemInDestination = _context.WarehouseItems_DbData
                            .FirstOrDefault(wi => wi.Item_Name == previousItemWarehouse.Item_Name && wi.WarehouseInfoId == model.DestinationWarehouseId);

                        if (existingItemInDestination != null)
                        {
                            existingItemInDestination.Item_Unit_Quant += item.QuantityToTransfer;
                            _warehouseInfoService.TotalPrice(existingItemInDestination);
                        }
                        else
                        {
                            var newItemWarehouse = new WarehouseItems
                            {
                                Item_Name = previousItemWarehouse.Item_Name,
                                Item_Capacity_Quant = previousItemWarehouse.Item_Capacity_Quant,
                                Item_SpaceAccuired = previousItemWarehouse.Item_SpaceAccuired,
                                Item_price_per_unit = previousItemWarehouse.Item_price_per_unit,
                                Item_Unit_Quant = item.QuantityToTransfer, 
                                WarehouseInfoId = model.DestinationWarehouseId 
                            };
                            _warehouseInfoService.TotalPrice(newItemWarehouse);
                            _context.WarehouseItems_DbData.Add(newItemWarehouse); 
                        }
                    }
                }

                _context.SaveChanges();
            }
            TempData["LastActionMessage"] = $"Last action has been sucessfully fulfilled.";

            return RedirectToAction("TransferItems");
        }

        

        public IActionResult GetItemsForWarehouse(int warehouseId)
        {
            var items = _context.WarehouseItems_DbData
                .Where(item => item.WarehouseInfoId == warehouseId)
                .Select(item => new ItemToTransfer
                {
                    WarehouseItemId = item.WarehouseItemsId,
                    ItemName = item.Item_Name,
                    QuantityAvailable = item.Item_Unit_Quant ?? 0
                })
                .ToList();

            return Json(items);
        }

    }
}
