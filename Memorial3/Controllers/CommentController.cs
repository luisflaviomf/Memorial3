using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Adicione esta diretiva
using Memorial3.Data;
using Memorial3.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using Memorial3.ViewModels;

public class CommentController : Controller
{
    private readonly Memorial3Context _context;
    private readonly UserManager<DefaultUser> _userManager;

    public CommentController(Memorial3Context context, UserManager<DefaultUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize]
    public async Task<IActionResult> Create(int memorialId)
    {
        var comments = await _context.Comment
            .Where(c => c.MemorialId == memorialId)
            .Include(c => c.User)
            .ToListAsync();

        var model = new CommentViewModel
        {
            MemorialId = memorialId,
            Comments = comments
        };

        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CommentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            var comment = new Comment
            {
                Content = model.Content,
                DatePosted = DateTime.Now,
                MemorialId = model.MemorialId,
                UserId = user.Id
            };

            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", new { memorialId = model.MemorialId });
        }

        model.Comments = await _context.Comment
            .Where(c => c.MemorialId == model.MemorialId)
            .Include(c => c.User)
            .ToListAsync();

        return View(model);
    }
}

