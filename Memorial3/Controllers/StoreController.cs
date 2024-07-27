using Microsoft.AspNetCore.Mvc;
using Memorial3.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;


namespace Memorial3.Controllers
{
    public class StoreController : Controller
    {
        private readonly Memorial3Context _context;

        public StoreController(Memorial3Context context) { 
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice)
        {
            var memoriais = _context.Memorial.Select(b => b);

            if (!string.IsNullOrEmpty(searchString)) { 
                memoriais = memoriais.Where(b => b.Name.Contains(searchString) || b.Coletivo.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(minPrice))
            {
                var min = int.Parse(minPrice);
                memoriais = memoriais.Where(b => b.Id >= min);
            }
            if (!string.IsNullOrEmpty(maxPrice)) { 
                var max = int.Parse(maxPrice);
                memoriais = memoriais.Where(b => b.Id >= max);
            }

            return View(await memoriais.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memorial = await _context.Memorial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memorial == null)
            {
                return NotFound();
            }

            return View(memorial);
        }
    }

}
