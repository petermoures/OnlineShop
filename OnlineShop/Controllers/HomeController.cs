using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly OnlineShopeEntity onlineShope;
        private readonly UserManager<User> userManager;

        public HomeController(OnlineShopeEntity _onlineShope, UserManager<User> _userManager)
        {
            onlineShope = _onlineShope;
            userManager = _userManager;
        }
        
        public object Session { get; private set; }
        
        public IActionResult Index()
        {
            List<product> products = onlineShope.Products.Include(i => i.ProductImagsss).ToList();
            return View(products);
        }
    }   
    
}