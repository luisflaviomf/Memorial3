using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Memorial3.Data;
using Memorial3.Models;
using Memorial3.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Memorial3.Controllers
{
    public class MediaController : Controller
    {
        private readonly Memorial3Context _context;

        public MediaController(Memorial3Context context)
        {
            _context = context;
        }

        // GET: Media/Upload/5
        public async Task<IActionResult> Upload(int memorialId)
        {
            var memorial = await _context.Memorial.FindAsync(memorialId);
            if (memorial == null)
            {
                return NotFound();
            }

            var mediaList = await _context.Media
                .Where(m => m.MemorialId == memorialId)
                .ToListAsync();

            var model = new MediaUploadViewModel
            {
                MemorialId = memorialId,
                MediaList = mediaList
            };

            return View(model);
        }

        // POST: Media/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(MediaUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    var filePath = Path.Combine("wwwroot/media", Path.GetFileName(model.File.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(stream);
                    }

                    var media = new Media
                    {
                        MemorialId = model.MemorialId,
                        FilePath = filePath,
                        FileType = model.File.ContentType
                    };

                    _context.Media.Add(media);
                    await _context.SaveChangesAsync();

                    // Redirect to the same upload page to refresh the media list
                    return RedirectToAction("Upload", new { memorialId = model.MemorialId });
                }
            }

            return View(model);
        }
    }
}
