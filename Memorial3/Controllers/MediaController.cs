using Microsoft.AspNetCore.Mvc;
using Memorial3.Data;
using Memorial3.Models;
using Memorial3.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class MediaController : Controller
{
    private readonly Memorial3Context _context;

    public MediaController(Memorial3Context context)
    {
        _context = context;
    }

    // GET: Media/Upload
    public IActionResult Upload(int memorialId)
    {
        var viewModel = new MediaUploadViewModel
        {
            MemorialId = memorialId,
            MediaList = _context.Media.Where(m => m.MemorialId == memorialId).ToList()
        };
        return View(viewModel);
    }

    // POST: Media/Upload
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(MediaUploadViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.File != null && model.File.Length > 0)
            {
                // Defina o caminho para salvar o arquivo
                var fileName = Path.GetFileName(model.File.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/media", fileName);

                // Salve o arquivo
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                // Crie o novo objeto Media
                var media = new Media
                {
                    FilePath = "/media/" + fileName,
                    FileType = model.MediaName, // Use o nome fornecido pelo usuário
                    MemorialId = model.MemorialId
                };

                // Adicione o objeto Media ao contexto
                _context.Media.Add(media);
                await _context.SaveChangesAsync();

                // Redirecione para a ação Index com o ID do memorial
                return RedirectToAction("Upload", new { memorialId = model.MemorialId });
            }
        }

        // Se o modelo não for válido, recarregue a view com o modelo atual
        model.MediaList = _context.Media.Where(m => m.MemorialId == model.MemorialId).ToList();
        return View(model);
    }

    // POST: Media/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, int memorialId)
    {
        var media = await _context.Media.FindAsync(id);
        if (media != null)
        {
            // Remove o arquivo do disco
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/media", Path.GetFileName(media.FilePath));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Remove o objeto Media do contexto
            _context.Media.Remove(media);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Upload", new { memorialId = memorialId });
    }
}
