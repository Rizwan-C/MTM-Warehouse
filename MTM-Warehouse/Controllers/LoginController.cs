using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginPage()
        {
            return View();
        }

        public IActionResult FetchLoginDetails()
        {
            

            if (ModelState.IsValid)
            {

                return View();
            }
            else if (true){
                return View();
            }
        }
    }
}
