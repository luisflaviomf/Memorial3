using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Memorial3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Memorial3.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<DefaultUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;

        public DownloadPersonalDataModel(
            UserManager<DefaultUser> userManager,
            ILogger<DownloadPersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("Usuário com ID '{UserId}' solicitou seus dados pessoais.", _userManager.GetUserId(User));

            // Inclui apenas dados pessoais para download
            var dadosPessoais = new Dictionary<string, string>();
            var propriedadesDadosPessoais = typeof(DefaultUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in propriedadesDadosPessoais)
            {
                dadosPessoais.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var logins = await _userManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                dadosPessoais.Add($"{l.LoginProvider} chave do provedor de login externo", l.ProviderKey);
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=DadosPessoais.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(dadosPessoais), "application/json");
        }
    }
}
