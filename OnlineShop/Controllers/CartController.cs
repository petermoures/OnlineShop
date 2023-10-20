
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private readonly OnlineShopeEntity onlineShope;
        private readonly UserManager<User> userManager;
        public CartController( OnlineShopeEntity _onlineShope,UserManager<User> _userManager)
        {
            onlineShope = _onlineShope;
            userManager= _userManager;
        }
        
           [Authorize]
        public IActionResult ItemInCart()
        {
            string IdUser = userManager.GetUserId(User);
            Cart MyCart = onlineShope.Carts.Include(i => i.CartProducts).FirstOrDefault(a => a.UserId == IdUser);
            foreach (CartProduct p in MyCart.CartProducts)
           {
               var prod = onlineShope.Products.Include(q=>q.category).Include(e=>e.ProductImagsss).FirstOrDefault(i => i.Id == p.ProductId);
               p.product = prod;
           }

            return View(MyCart);
        }
        public IActionResult DeleteFromCart(int id)
        {
            string IdUser = userManager.GetUserId(User);
            Cart MyCar = onlineShope.Carts.Include(i => i.CartProducts).FirstOrDefault(a => a.UserId == IdUser);
            foreach (CartProduct p in MyCar.CartProducts)
            {
                var prod = onlineShope.Products.Include(q => q.category).FirstOrDefault(i => i.Id == p.ProductId);
                p.product = prod;
            }
            foreach (CartProduct p in MyCar.CartProducts)
            {

                if (p.ProductId == id)
                {
                    if (p.Quantity == 1)
                    {
                        MyCar.CartProducts.Remove(p);
                        MyCar.TotalPrice = MyCar.TotalPrice - p.product.ProPrice;
                        MyCar.totalQuantity--;
                        break;
                    }
                    else
                    {
                        p.Quantity--;
                        MyCar.TotalPrice = MyCar.TotalPrice - p.product.ProPrice;
                        MyCar.totalQuantity--;
                        break;
                    }
                }

            }      
            onlineShope.Carts.Update(MyCar);
            onlineShope.SaveChanges();

            return RedirectToAction("ItemInCart");
        }

        public IActionResult IncreadeToCart(int id)
        {
            string IdUser = userManager.GetUserId(User);
            Cart MyCa = onlineShope.Carts.Include(i => i.CartProducts).FirstOrDefault(a => a.UserId == IdUser);
            foreach (CartProduct p in MyCa.CartProducts)
            {
                var prod = onlineShope.Products.Include(q => q.category).FirstOrDefault(i => i.Id == p.ProductId);
                p.product = prod;
            }
            foreach(CartProduct pc in MyCa.CartProducts)
            {
                if(pc.ProductId == id)
                {
                    pc.Quantity++;
                    MyCa.TotalPrice = MyCa.TotalPrice + pc.product.ProPrice;
                    MyCa.totalQuantity++;
                    break;
                }
            }
            onlineShope.Carts.Update(MyCa);
            onlineShope.SaveChanges();
            return RedirectToAction("ItemInCart");
        }
    }
}
