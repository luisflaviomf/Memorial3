using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Memorial3.Data;
using Memorial3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Memorial3.Controllers
{
    
    public class MemorialsController : Controller
    {
        private readonly Memorial3Context _context;

        public MemorialsController(Memorial3Context context)
        {
            _context = context;
        }

        // GET: Memorials
        public async Task<IActionResult> Index()
        {
            return View(await _context.Memorial.ToListAsync());
        }

        // GET: Memorials/Details/5
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
        
        // GET: Memorials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memorials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Historia,DataNascimento,DataFalecimento,Formacao,Religiao,Hobbies,MemorialPictureURL,Coletivo")] Memorial memorial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memorial);
        }

        // GET: Memorials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memorial = await _context.Memorial.FindAsync(id);
            if (memorial == null)
            {
                return NotFound();
            }
            return View(memorial);
        }

        // POST: Memorials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Historia,DataNascimento,DataFalecimento,Formacao,Religiao,Hobbies,MemorialPictureURL,Coletivo")] Memorial memorial)
        {
            if (id != memorial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemorialExists(memorial.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(memorial);
        }

        // GET: Memorials/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Memorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memorial = await _context.Memorial.FindAsync(id);
            _context.Memorial.Remove(memorial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemorialExists(int id)
        {
            return _context.Memorial.Any(e => e.Id == id);
        }
    }
}
