using Microsoft.AspNetCore.Mvc;
using Memorial3.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Memorial3.Controllers
{
    public class StoreController : Controller
    {
        private readonly Memorial3Context _context;

        public StoreController(Memorial3Context context) { 
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Memorial.ToListAsync());
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
