using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly OnlineShopeEntity onlineShope;
        public CategoryController(OnlineShopeEntity _onlineShope)
        {
            onlineShope = _onlineShope;
        }
        public IActionResult GetLaptops()
        {
            IEnumerable<product> laptopss = onlineShope.Products.Include(q => q.category).Include(s=>s.ProductImagsss).Where(e => e.category.CateName == "laptop").ToList();
            return View(laptopss);
        }
        [Authorize]
        public IActionResult AddToCartLap(int id,string userId)
        {
            Cart cart = onlineShope.Carts.Include(q => q.CartProducts).FirstOrDefault(a => a.UserId == userId);
            foreach (CartProduct cp in cart.CartProducts)
            {
                product pr = onlineShope.Products.Include(q => q.category).FirstOrDefault(q => q.Id == cp.ProductId);

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
                    product pr = onlineShope.Products.Include(q => q.category).FirstOrDefault(q => q.Id == cp.ProductId);

                    cp.product = pr;
                }
                cart.TotalPrice = cart.TotalPrice + cartpro1.product.ProPrice;
                cart.totalQuantity++;

            }
            onlineShope.Carts.Update(cart);
            onlineShope.SaveChanges();



            
            return RedirectToAction("GetLaptops", "Category");
            
        }
        public IActionResult DetailsLap()
        {
            return View();
        }
        [Authorize]
        public IActionResult addToFavouriteLap(int Id,string userId)
        {
            Favourite favourite = onlineShope.Favorites.Include(a => a.favouriteProducts).FirstOrDefault(s => s.UserId == userId);
            foreach (FavouriteProduct favProd in favourite.favouriteProducts)
            {
                product produ = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).FirstOrDefault(a => a.Id == favProd.prodID);
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
                    product produ = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).FirstOrDefault(a => a.Id == favPro.prodID);
                    favPro.product = produ;
                }

                onlineShope.Favorites.Update(favourite);
                onlineShope.SaveChanges();
            }


            return RedirectToAction("GetLaptops", "Category");
        }
        public IActionResult GetPhones()
        {
            IEnumerable<product> phonesss = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).Where(e => e.category.CateName == "Mopile Phone").ToList();
            return View(phonesss);
        }
        [Authorize]
        public IActionResult AddToCartPhon(int id,string userId)
        {
            Cart cart = onlineShope.Carts.Include(q => q.CartProducts).FirstOrDefault(a => a.UserId == userId);
            foreach (CartProduct cp in cart.CartProducts)
            {
                product pr = onlineShope.Products.Include(q => q.category).FirstOrDefault(q => q.Id == cp.ProductId);

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
                    product pr = onlineShope.Products.Include(q => q.category).FirstOrDefault(q => q.Id == cp.ProductId);

                    cp.product = pr;
                }
                cart.TotalPrice = cart.TotalPrice + cartpro1.product.ProPrice;
                cart.totalQuantity++;

            }
            onlineShope.Carts.Update(cart);
            onlineShope.SaveChanges();



            
            return RedirectToAction("GetPhones", "Category");
           
        }
        public IActionResult DetailsPhon()
        {
            return View();
        }
        [Authorize]
        public IActionResult addToFavouritePhon(int Id,string userId)
        {
            Favourite favourite = onlineShope.Favorites.Include(a => a.favouriteProducts).FirstOrDefault(s => s.UserId == userId);
            foreach (FavouriteProduct favProd in favourite.favouriteProducts)
            {
                product produ = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).FirstOrDefault(a => a.Id == favProd.prodID);
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
                    product produ = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).FirstOrDefault(a => a.Id == favPro.prodID);
                    favPro.product = produ;
                }

                onlineShope.Favorites.Update(favourite);
                onlineShope.SaveChanges();
            }


            return RedirectToAction("GetPhones", "Category");
        }
        public IActionResult GetSamrtTv()
        {
            IEnumerable<product> TVss = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).Where(e => e.category.CateName == "Smart TV").ToList();
            return View(TVss);
        }
        [Authorize]
        public IActionResult AddToCartTV(int id,string userId)
        {
            Cart cart = onlineShope.Carts.Include(q => q.CartProducts).FirstOrDefault(a => a.UserId == userId);
            foreach (CartProduct cp in cart.CartProducts)
            {
                product pr = onlineShope.Products.Include(q => q.category).FirstOrDefault(q => q.Id == cp.ProductId);

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
                    product pr = onlineShope.Products.Include(q => q.category).FirstOrDefault(q => q.Id == cp.ProductId);

                    cp.product = pr;
                }
                cart.TotalPrice = cart.TotalPrice + cartpro1.product.ProPrice;
                cart.totalQuantity++;

            }
            onlineShope.Carts.Update(cart);
            onlineShope.SaveChanges();



            
            return RedirectToAction("GetSamrtTv", "Category");
            
        }
        public IActionResult DetailsTV()
        {
            return View();
        }
        [Authorize]
        public IActionResult addToFavouriteTV(int Id,string userId)
        {
            Favourite favourite = onlineShope.Favorites.Include(a => a.favouriteProducts).FirstOrDefault(s => s.UserId == userId);
            foreach (FavouriteProduct favProd in favourite.favouriteProducts)
            {
                product produ = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).FirstOrDefault(a => a.Id == favProd.prodID);
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
                    product produ = onlineShope.Products.Include(q => q.category).Include(s => s.ProductImagsss).FirstOrDefault(a => a.Id == favPro.prodID);
                    favPro.product = produ;
                }

                onlineShope.Favorites.Update(favourite);
                onlineShope.SaveChanges();
            }


            return RedirectToAction("GetSamrtTv", "Category");

        }
    }
}
