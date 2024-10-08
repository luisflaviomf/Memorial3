﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Memorial3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Memorial3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly UserManager<DefaultUser> _userManager;
        private readonly SignInManager<DefaultUser> _signInManager;

        public ConfirmEmailChangeModel(UserManager<DefaultUser> userManager, SignInManager<DefaultUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                StatusMessage = "Erro ao alterar o e-mail.";
                return Page();
            }

            // Em nossa interface, e-mail e nome de usuário são um e o mesmo, então, quando atualizamos o e-mail
            // precisamos atualizar o nome de usuário.
            var setUserNameResult = await _userManager.SetUserNameAsync(user, email);
            if (!setUserNameResult.Succeeded)
            {
                StatusMessage = "Erro ao alterar o nome de usuário.";
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Obrigado por confirmar a alteração do seu e-mail.";
            return Page();
        }
    }
}
