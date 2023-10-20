using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using OnlineShop.ViewModel;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly OnlineShopeEntity onlineShope;
        public ProductController(UserManager<User> _userManager, OnlineShopeEntity _onlineShope)
        {
            onlineShope = _onlineShope;
        }
        [Authorize(Roles ="admin")]
       public IActionResult Get()
        {
            var pto = onlineShope.Products.Include(i => i.ProductImagsss).Include(q=>q.category).ToList();


            return View(pto);
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult AddProduct()
        {
            //var cates=onlineShope.Categories.ToList().Select(i => new { i.CateName, i.CateId }).ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddProduct(AddproductViewModel addproduct)
        {
            if(ModelState.IsValid==false)
            {
                //var cates = onlineShope.Categories.ToList().Select(i => new { i.CateName, i.CateId }).ToList();
                //ViewBag.Categs = onlineShope.Categories.ToList().Select(i => new SelectListItem(i.CateName, i.CateName)).ToList();
                return View();
            }
            else
            {
               //ViewBag.Categs = onlineShope.Categories.ToList().Select(i => new SelectListItem(i.CateName, i.CateName)).ToList();
               var prod = new product();
               prod.ProName=addproduct.ProductName;
               prod.ProPrice = addproduct.ProductPrice;
               prod.Color=addproduct.Color;
                prod.CategoryId = addproduct.CategoryId;
               prod.QuantityForAddPro = addproduct.QuantityToProduct;
              
               // prod.category.CateName = addproduct.CategoryName;
                prod.Available=addproduct.Available;
                onlineShope.Products.Add(prod);
                onlineShope.SaveChanges();
                var prodImage=new ProductImage();
                prodImage.prottId=prod.Id;
                prodImage.path = addproduct.ImagePath;
                onlineShope.ProductImages.Add(prodImage);
                onlineShope.SaveChanges();
                return RedirectToAction("Get", "product");
            }

        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteProduct(int id)
        {
            var pro = onlineShope.Products.FirstOrDefault(e => e.Id == id);
            onlineShope.Products.Remove(pro);
            onlineShope.SaveChanges();
            return RedirectToAction("Get");
        }

         [HttpGet]
         [Authorize(Roles ="admin")]
         public IActionResult UpdateProduct(int id)
         {
            ViewBag.prod = onlineShope.Products.Include(q => q.category).FirstOrDefault(i => i.Id == id);
            return View();
         }

       // [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateProduct(int id, UpdateProductViewModel updateProduct)
        {
           // ViewBag.prod = onlineShope.Products.Include(q => q.category).FirstOrDefault(i => i.Id == id);
            if (ModelState.IsValid == false)
            {
               
                ViewBag.prod = onlineShope.Products.Include(q => q.category).FirstOrDefault(i => i.Id == id);
                return View();
            }
            else
            {
                var pro = onlineShope.Products.Include(Q => Q.category).FirstOrDefault(e => e.Id == id);
                pro.ProName = updateProduct.ProductName;
                pro.ProPrice = updateProduct.ProductPrice;
                pro.Color = updateProduct.Color;
                //pro.Image = updateProduct.Image;
                pro.QuantityForAddPro = updateProduct.QuantityToProduct;
                pro.CategoryId = updateProduct.CatId;
                pro.Available = updateProduct.Available;
                onlineShope.Products.Update(pro);
                onlineShope.SaveChanges();
                return RedirectToAction("Get");

            }
        }

        [Authorize(Roles = "admin")]
        public IActionResult ProductDetails(int id)
        {
            var pro = onlineShope.Products.Include(q => q.category).Include(s=>s.ProductImagsss).FirstOrDefault(a => a.Id == id);
            return View(pro);
        }


        public IActionResult Details(int id)
        {
            var produ = onlineShope.Products.Include(i => i.category).Include(Q=>Q.ProductImagsss).FirstOrDefault(i => i.Id == id);
            return View(produ);
        }

        [Authorize]
        public IActionResult AddToCart(int id, string userId)
        {
            Cart cart = onlineShope.Carts.Include(q => q.CartProducts).FirstOrDefault(a => a.UserId == userId);
            foreach(CartProduct cp in cart.CartProducts)
            {
                product pr = onlineShope.Products.Include(q=>q.category).FirstOrDefault(q=>q.Id==cp.ProductId);

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
                if(!IsProductExist)
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



            //}
            return RedirectToAction("Index", "Home");


            // foreach (var item in cart.Productss)
            //{
            //var cat = onlineShope.Categories.FirstOrDefault(i => i.CateId == item.CategoryId);
            // item.category = cat;
            //}

            //    if (cart.Productss.Count==0)
            //    {
            //        var pro = onlineShope.Products.Include(i => i.category).FirstOrDefault(a => a.Id == id);


            //        cart.Productss.Add(pro) ;
            //        foreach(product op in cart.Productss)
            //        {
            //            op.QuantityForCartPro++;
            //        }
            //        cart.TotalPrice = cart.TotalPrice + pro.ProPrice;
            //        onlineShope.Carts.Update(cart);
            //        onlineShope.SaveChanges();

            //        return View(cart);
            //    }
            //    else 
            //    {
            //        foreach(product prod in cart.Productss)
            //        {
            //            if(prod.Id == id)
            //            {
            //                prod.QuantityForCartPro++;
            //                cart.TotalPrice = cart.TotalPrice + prod.ProPrice;
            //                onlineShope.Carts.Update(cart);
            //                onlineShope.SaveChanges();
            //                return View(cart);
            //                break;
            //            }
            //        }

            //     var pro = onlineShope.Products.Include(i => i.category).FirstOrDefault(i => i.Id == id);
            //     cart.Productss.Add(pro);
            //        foreach (product op in cart.Productss)
            //        {
            //            if(op.Id == id)
            //            {
            //                op.QuantityForCartPro++;
            //            }

            //        }
            //        cart.TotalPrice = cart.TotalPrice + pro.ProPrice;
            //     onlineShope.Carts.Update(cart);
            //     onlineShope.SaveChanges();
            //     return View(cart);
            //    }


            //}
            // public IActionResult RemoveItem(int id,string userId)

            // List<Item> cart=ViewBag.cart;
            //int index=IndexProduct(id);
            // if(index!=-1)
            // {
            // if( cart[index].Quantity>1)
            // {
            //  cart[index].Quantity -= 1;

            // }
            // else 
            // {
            //  cart.Remove(new Item { productss = onlineShope.Products.Include(i => i.category).FirstOrDefault(m => m.Id == id), Quantity = 1 });
            // }
            // ViewBag.cart=cart;

            //var car=onlineShope.Carts.Include(i=>i.Productss).FirstOrDefault(a=>a.UserId==userId);
            // foreach (var item in car.Productss)
            //{
            // var cat = onlineShope.Categories.FirstOrDefault(i => i.CateId == item.CategoryId);
            // item.category = cat;
            // }
            // car.Productss.Remove(onlineShope.Products.Include(i => i.category).FirstOrDefault(a=>a.Id==id));
            // onlineShope.Carts.Update(car);
            //onlineShope.SaveChanges();
            //return RedirectToAction("AddToCart");

            //}


            //return RedirectToAction("Index");
            //}
            // public int IndexProduct(int prodf)
            //{
            //for (int i=0;i< prod.Count;i++)
            // {
            //if(prod[i].productss.Id == prodf)
            //{
            //  return i;
            //}

            // }
            // return -1;
            //}
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult addProductImage()
        {
            return View();
        }

            [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult addProductImage(AddProductImageViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                ProductImage productImage = new ProductImage();
                productImage.prottId=model.productId;
                productImage.path = model.Path;
                onlineShope.ProductImages.Add(productImage);
                onlineShope.SaveChanges();

                return RedirectToAction("Get", "Product");

            }
                
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult addCategory()
        {
          return View();
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public IActionResult addCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                Category category = new Category();
                category.CateName = model.CategoryName;
                onlineShope.Categories.Add(category);
                onlineShope.SaveChanges();
                return RedirectToAction("Get", "Product");
            }
        }
    }
}
