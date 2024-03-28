using Microsoft.AspNetCore.Mvc;

namespace MTM_Warehouse.Controllers
{
    public class ActionController : Controller
    {
        [HttpGet("/app/viewActions")]
        public IActionResult ListActions()
        {
            return View();
        }

        [HttpGet("/manage/addWareHouse")]
        public IActionResult AddWarehousePage() { 
        
            return View();
        }

        [HttpPost("/manage/addWareHouse")]
        public IActionResult AddWarehouse()
        {
            return View();
        }
    }
}
