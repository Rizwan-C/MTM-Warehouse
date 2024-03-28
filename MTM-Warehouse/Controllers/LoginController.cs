using Microsoft.AspNetCore.Mvc;
using MTM_Warehouse.Entities;

namespace MTM_Warehouse.Controllers
{
    public class LoginController : Controller
    {
        private AllDbContext _context;
        public LoginController(AllDbContext allDbContext)
        {
            _context = allDbContext;
        }

        public IActionResult LoginPage()
        {
            return View();
        }

        public IActionResult Logout()
        {
            TempData["LastActionMessage"] = "Logged out sucessfully !";
            return View("LoginPage");
        }

        public IActionResult FetchLoginDetails( LoginEmp loginEmp)
        {
            LoginEmp loggedEmp = _context.loginEmps_DbData.Where(s => s.Username == loginEmp.Username).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if ( loginEmp?.Username?.ToLower() == loggedEmp?.Username?.ToLower())
                {
                    HttpContext.Session.SetString("Name", loggedEmp?.Name?.ToLower());
                    HttpContext.Session.SetString("UserName", loggedEmp?.Username?.ToLower());
                    HttpContext.Session.SetString("AccessRight", loggedEmp.AccessRights.ToLower());

                    ViewBag.Name = HttpContext.Session.GetString("Name") ?? "None";
                    ViewBag.UserName = HttpContext.Session.GetString("UserName") ?? "None";
                    ViewBag.AccessRight = HttpContext.Session.GetString("AccessRight") ?? "None";


                    return RedirectToAction("HomePage","Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The username or password is incorrect.");
                    return View("LoginPage", loginEmp); 
                }
            }
            else
            {
                return View("LoginPage", loginEmp);
            }

        }
    }
}
