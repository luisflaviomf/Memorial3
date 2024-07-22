using Microsoft.AspNetCore.Mvc;
using Memorial3.Data;
using Memorial3.Models;
using System;

namespace Memorial3.Controllers
{
    public class OrderController : Controller
    {
        private readonly Memorial3Context _context;
        private readonly Cart _cart;

        public OrderController(Memorial3Context context, Cart cart)
        {
            _context = context;
            _cart = cart;

        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order) {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if (_cart.CartItems.Count == 0) {
                ModelState.AddModelError("", "Cart is empty, please add a memorial first.");
            }

            if (ModelState.IsValid) {
                CreateOrder(order);
                _cart.ClearCart();
                return View("CheckoutComplete", order);
            }

            return View(order);

        }
        public IActionResult CheckoutComplete(Order order) { 
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var cartItems = _cart.CartItems;

            foreach (var item in cartItems) {
                var orderItem = new OrderItem
                {
                    Quantity = item.Quantity,
                    MemorialId = item.Memorial.Id,
                    OrderId = order.Id,
                    Price = item.Memorial.Id * item.Quantity,
                };
                order.OrderItems.Add(orderItem);
                order.OrderTotal += orderItem.Price;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
