using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ViewModel;

namespace OnlineShop.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> RoleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AddRole()
        {
            return View();
        }
        
            [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            else
            {
                var result =
                await RoleManager.CreateAsync(new IdentityRole()
                {
                    Name = model.Name
                });
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(i =>
                    {
                        ModelState.AddModelError("", i.Description);
                    });
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}