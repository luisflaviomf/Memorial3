using Memorial3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Memorial3.Models
{
    public class Cart
    {
        private readonly Memorial3Context _context;

        public Cart(Memorial3Context context)
        {
            _context = context;
        }

        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<Memorial3Context>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);
            return new Cart(context) {Id = cartId};
        }

        public CartItem GetCartItem(Memorial memorial)
        {
            return _context.CartItems.SingleOrDefault(ci =>
            ci.Memorial.Id == memorial.Id && ci.CartId == Id);
        }
        public void AddToCart(Memorial memorial, int quantity)
        {
            var cartItem = GetCartItem(memorial);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Memorial = memorial,
                    Quantity = quantity,
                    CartId = Id
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();
        }

        public int ReduceQuantity(Memorial memorial) {
            var cartItem = GetCartItem(memorial);
            var remainingQuantity = 0;

            if (cartItem != null) {
                if (cartItem.Quantity > 1) { 
                    remainingQuantity = --cartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();
            
            return remainingQuantity;
        }

        public int IncreaseQuantity(Memorial memorial)
        {
            var cartItem = GetCartItem(memorial);
            var remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 0)
                {
                    remainingQuantity = ++cartItem.Quantity;
                }
            }
            _context.SaveChanges();

            return remainingQuantity;
        }

        public void RemoveFromCart(Memorial memorial) { 
            var cartItem = GetCartItem(memorial);

            if (cartItem != null) {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public List<CartItem> GetAllCartItems()
        {
            return CartItems ??
                (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                .Include(ci => ci.Memorial)
                .ToList());
        }

        public int GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CartId == Id)
                .Select(ci => ci.Memorial.Id * ci.Quantity)
                .Sum();
        }
    }
}
