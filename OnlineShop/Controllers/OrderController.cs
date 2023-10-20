using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using OnlineShop.ViewModel;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly OnlineShopeEntity onlineShope;
        public OrderController(OnlineShopeEntity _onlineShope,UserManager<User> _userManager)
        {
            onlineShope = _onlineShope;
            userManager = _userManager;
        }
        [HttpGet]
        [Authorize]
        public IActionResult MakeOrder()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult MakeOrder(AddOrderViewModel addOrder)
        {
            string IdUser = userManager.GetUserId(User);
            Cart MyCart = onlineShope.Carts.Include(i => i.CartProducts).FirstOrDefault(a => a.UserId == IdUser);

            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                var ord = new Order();
                ord.Address = addOrder.Address;
                ord.Email = addOrder.Email;
                ord.PhoneNumber = addOrder.PhoneNumber;
                ord.OrderDate = DateTime.Now;
                ord.Name = addOrder.Name;
                
                onlineShope.Orders.Add(ord);
                onlineShope.SaveChanges();
                var orderss = onlineShope.Orders.Include(q => q.OrderProducts).FirstOrDefault(w => w.Id == ord.Id);
                foreach (CartProduct p in MyCart.CartProducts)
                {

                    orderss.OrderProducts.Add(new OrderProduct { OrderId = ord.Id, ProductId = p.ProductId,Quantity=p.Quantity });
                    //onlineShope.CartProducts.Remove(p);
                    //onlineShope.SaveChanges();
                    
                }
                onlineShope.Orders.Update(orderss);
                onlineShope.SaveChanges();

                //foreach(CartProduct c in MyCart.CartProducts)
                //{
                // MyCart.CartProducts.Remove(c);
                // }

               // for (int i = 0; i < MyCart.CartProducts.Count; i++)
                //{

                // MyCart.CartProducts.RemoveAt(i);

                //}

               // onlineShope.Carts.Update(MyCart);
               // onlineShope.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            

        }
        [Authorize]
       public IActionResult GetAllOrders()
        {
            IEnumerable<Order> ord = onlineShope.Orders.Include(q => q.OrderProducts).ToList();
            foreach(Order ou in ord)
            {
                foreach(OrderProduct orderProduct in ou.OrderProducts)
                {
                    product rpo = onlineShope.Products.Include(e=>e.category).FirstOrDefault(q => q.Id == orderProduct.ProductId);
                    orderProduct.product = rpo;
                }
            }
            return View(ord);
        }
        [Authorize(Roles ="admin")]
        public IActionResult DeleteOrder(int id)
        {
            var ordd=onlineShope.Orders.FirstOrDefault(q=>q.Id == id); 
            onlineShope.Orders.Remove(ordd);
            onlineShope.SaveChanges();
            return RedirectToAction("GetAllOrders");
        }
    }
}
