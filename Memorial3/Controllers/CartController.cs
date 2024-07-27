using Microsoft.AspNetCore.Mvc;
using Memorial3.Models;
using Memorial3.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Memorial3.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly Memorial3Context _context;
        private readonly Cart _cart;
        public CartController(Memorial3Context context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var Items = _cart.GetAllCartItems();
            _cart.CartItems = Items;
            return View(_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selectedMemorial = GetMemorialById(id);

            if (selectedMemorial != null)
            {
                _cart.AddToCart(selectedMemorial, 1);
            }
            return RedirectToAction("Index", "Store");
        }

        public IActionResult RemoveFromCart(int id) {
            var selectedMemorial = GetMemorialById(id);
            if (selectedMemorial != null) { 
                _cart.RemoveFromCart(selectedMemorial);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(int id)
        {
            var selectedMemorial = GetMemorialById(id);
            if (selectedMemorial != null)
            {
                _cart.ReduceQuantity(selectedMemorial);
            }
            return RedirectToAction("Index");
        }
        public IActionResult IncreaseQuantity(int id)
        {
            var selectedMemorial = GetMemorialById(id);
            if (selectedMemorial != null)
            {
                _cart.IncreaseQuantity(selectedMemorial);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart() { 
            _cart.ClearCart();
            return RedirectToAction("Index");
        }
        public Memorial GetMemorialById(int id) { 
            return _context.Memorial.FirstOrDefault(b => b.Id == id);
        }
    }
}
