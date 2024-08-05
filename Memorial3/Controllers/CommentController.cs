using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        var user = await _userManager.GetUserAsync(User);

        var model = new CommentViewModel
        {
            MemorialId = memorialId,
            Comments = comments,
            CurrentUserId = user.Id,
            IsAdmin = await IsUserAdmin(user)
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _context.Comment.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var isAdmin = await IsUserAdmin(user);

        if (comment.UserId != user.Id && !isAdmin)
        {
            return Forbid();
        }

        _context.Comment.Remove(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Create", new { memorialId = comment.MemorialId });
    }

    private async Task<bool> IsUserAdmin(DefaultUser user)
    {
        return await _userManager.IsInRoleAsync(user, "Admin");
    }
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var comment = await _context.Comment
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        var isAdmin = await IsUserAdmin(user);

        if (comment.UserId != user.Id && !isAdmin)
        {
            return Forbid();
        }

        var model = new EditCommentViewModel
        {
            Id = comment.Id,
            MemorialId = comment.MemorialId,
            Content = comment.Content,
            CurrentUserId = user.Id,
            IsAdmin = isAdmin
        };

        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(EditCommentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var comment = await _context.Comment.FindAsync(model.Id);
            if (comment == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await IsUserAdmin(user);

            if (comment.UserId != user.Id && !isAdmin)
            {
                return Forbid();
            }

            comment.Content = model.Content;
            _context.Update(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Create", new { memorialId = model.MemorialId });
        }

        return View(model);
    }
}
