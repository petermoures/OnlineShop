using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System.Security.Cryptography.X509Certificates;

namespace OnlineShop.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly OnlineShopeEntity onlineShope;
        private readonly UserManager<User> userManager;
        public FavouriteController(OnlineShopeEntity _onlineShope,UserManager<User> _userManager)
        {
            onlineShope = _onlineShope;
            userManager = _userManager;
        }
        [Authorize]
        public IActionResult FavouriteItem()
        {
            string IdUser = userManager.GetUserId(User);
            Favourite favourite = onlineShope.Favorites.Include(a => a.favouriteProducts).FirstOrDefault(s => s.UserId == IdUser);
            foreach(FavouriteProduct favProd in favourite.favouriteProducts)
            {
                product produ = onlineShope.Products.Include(q => q.category).Include(d=>d.ProductImagsss).FirstOrDefault(a => a.Id == favProd.prodID);
                favProd.product=produ;
            }
            
          

            return View(favourite);
        }
        [Authorize]
        public IActionResult addToFavourite(int Id, string userId)
        {
            Favourite favourite = onlineShope.Favorites.Include(a => a.favouriteProducts).FirstOrDefault(s => s.UserId == userId);
            foreach (FavouriteProduct favProd in favourite.favouriteProducts)
            {
                product produ = onlineShope.Products.Include(q => q.category).Include(s=>s.ProductImagsss).FirstOrDefault(a => a.Id == favProd.prodID);
                favProd.product = produ;
            }

            bool IsFound = false;


            foreach (FavouriteProduct favProd in favourite.favouriteProducts)
            {
                if (favProd.prodID == Id)
                {
                    IsFound = true;
                    break;


                }

            }
            if (IsFound == false)
            {
                FavouriteProduct favourite1 = new FavouriteProduct()
                {
                    FavouriteID = favourite.Id,
                    prodID = Id

                };
                favourite.favouriteProducts.Add(favourite1);
                foreach (FavouriteProduct favPro in favourite.favouriteProducts)
                {
                    product produ = onlineShope.Products.Include(q => q.category).Include(s=>s.ProductImagsss).FirstOrDefault(a => a.Id == favPro.prodID);
                    favPro.product = produ;
                }

                onlineShope.Favorites.Update(favourite);
                onlineShope.SaveChanges();
            }


            return RedirectToAction("Index","Home");
        }
        [Authorize]
        public IActionResult DeleteItemFromFavourite(int Id)
        {
            string IdUser = userManager.GetUserId(User);
            Favourite favourite = onlineShope.Favorites.Include(a => a.favouriteProducts).FirstOrDefault(s => s.UserId == IdUser);
            foreach (FavouriteProduct favProd in favourite.favouriteProducts)
            {
                product produ = onlineShope.Products.Include(q => q.category).FirstOrDefault(a => a.Id == favProd.prodID);
                favProd.product = produ;
            }

            FavouriteProduct favourite1 = onlineShope.FavoritesProducts.FirstOrDefault(a => a.prodID == Id);
            favourite.favouriteProducts.Remove(favourite1);
            onlineShope.Favorites.Update(favourite);
            onlineShope.SaveChanges();

            return RedirectToAction("FavouriteItem","Favourite");
        }
        [Authorize]
       public IActionResult addItemToCart(int id)
        {
            string IdUser = userManager.GetUserId(User);
            Cart cart = onlineShope.Carts.Include(q => q.CartProducts).FirstOrDefault(a => a.UserId == IdUser);
            foreach (CartProduct cp in cart.CartProducts)
            {
                product pr = onlineShope.Products.Include(q => q.category).Include(d=>d.ProductImagsss).FirstOrDefault(q => q.Id == cp.ProductId);

                cp.product = pr;
            }

            bool IsProductExist = false;

            foreach (CartProduct cp in cart.CartProducts)
            {
                if (cp.ProductId == id)
                {
                    cp.Quantity++;
                    cart.TotalPrice = cart.TotalPrice + cp.product.ProPrice;
                    cart.totalQuantity++;
                    IsProductExist = true;
                    break;

                }
            }
            if (!IsProductExist)
            {
                var cartpro1 = new CartProduct()
                {
                    CartId = cart.Id,
                    ProductId = id,
                    Quantity = 1
                };
                cart.CartProducts.Add(cartpro1);
                foreach (CartProduct cp in cart.CartProducts)
                {
                    product pr = onlineShope.Products.Include(q => q.category).Include(w=>w.ProductImagsss).FirstOrDefault(q => q.Id == cp.ProductId);

                    cp.product = pr;
                }
                cart.TotalPrice = cart.TotalPrice + cartpro1.product.ProPrice;
                cart.totalQuantity++;

            }
            onlineShope.Carts.Update(cart);
            onlineShope.SaveChanges();
            return RedirectToAction("FavouriteItem");
        }
    }
}
