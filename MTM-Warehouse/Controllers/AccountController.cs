﻿using Microsoft.AspNetCore.Mvc;

namespace MTM_Warehouse.Controllers
{
    public class AccountController : Controller
    {        

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
