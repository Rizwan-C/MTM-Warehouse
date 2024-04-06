using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MTM_Warehouse.Entities;
using MTM_Warehouse.Models;

namespace MTM_Warehouse.Controllers
{
    public class LoginController : Controller
    {
        //private AllDbContext _context;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AllDbContext _context;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, AllDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }


        [HttpGet("/MTM/register")]
        public IActionResult RegisterPage()
        {
            RegisterViewModel model = new RegisterViewModel();
            List<LoginEmp> loginEmps = _context.loginEmps_DbData.Where(l => l.Username == null || l.Username == string.Empty).ToList();
            model.LoginEmps = loginEmps;
            return View(model);
        }

        [HttpPost("/MTM/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            List<LoginEmp> loginEmps = _context.loginEmps_DbData.Where(l => l.Username == null || l.Username == string.Empty).ToList();
            model.LoginEmps = loginEmps;
            
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    LoginEmp emp = _context.loginEmps_DbData.Find(model.ManagerId);
                    emp.Username = model.Username;
                    _context.loginEmps_DbData.Update(emp);
                    _context.SaveChanges();

                    string selectedRole = model.SelectedRole;
                    if (!await _roleManager.RoleExistsAsync(selectedRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(selectedRole));
                    }

                    await _userManager.AddToRoleAsync(user, selectedRole);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View("RegisterPage",model);
        }

        [HttpGet]
        public IActionResult LoginPage(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        public async Task<IActionResult> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            
            TempData["LastActionMessage"] = "Logged out sucessfully !";
            return RedirectToAction("LoginPage", "Login");
        }


        [HttpPost("/login")]
        public async Task<IActionResult> FetchLoginDetails(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password,
                            isPersistent: model.RememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username/password.");
            return View("LoginPage", model);
        }


        public IActionResult AccessDenied()
        {            
            return View();
        }

    }
}
