using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Models;
using OnlineShop.ViewModel;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly OnlineShopeEntity onlineShopeEntity;
        private readonly RoleManager<IdentityRole> RoleManager;
        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager,RoleManager<IdentityRole> roleManager ,OnlineShopeEntity _onlineShopeEntity)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            RoleManager = roleManager;
            onlineShopeEntity = _onlineShopeEntity;

        }
        [HttpGet]
        public IActionResult Register()
        {
           // ViewBag.Roles = RoleManager.Roles.ToList().Select(i => new SelectListItem(i.Name, i.Name)).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AddUserViewModel addUser)
        {
            

            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                User user = new User();
                user.UserName = addUser.UserName;
                user.Email = addUser.Email;
                user.firstName = addUser.FirstName;
                user.lastName = addUser.LastName;
                IdentityResult result =
                    await userManager.CreateAsync(user, addUser.Password);
                   // await userManager.AddToRoleAsync(user, addUser.Role);
                if (result.Succeeded == false)
                {
                    result.Errors.ToList().ForEach(

                        i =>
                        {
                            ModelState.AddModelError("", i.Description);
                        }

                        );
                    return View();
                }
                else
                {
                    Cart cart = new Cart();
                    cart.totalQuantity = 0;
                    cart.TotalPrice = 0;
                    cart.UserId=user.Id;
                    onlineShopeEntity.Carts.Add(cart);
                    Favourite favourite = new Favourite();
                    favourite.UserId=user.Id;
                    onlineShopeEntity.Favorites.Add(favourite);
                    onlineShopeEntity.SaveChanges();
                    return RedirectToAction("Get", "Product");
                }

            }
        }

        public async Task<IActionResult> SignIn()
        {
            return View();
        }
        [HttpPost]
            public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {

                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded == false)
                {
                    ModelState.AddModelError("", "This Passwoed or User Name is incorect");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    } 
}
