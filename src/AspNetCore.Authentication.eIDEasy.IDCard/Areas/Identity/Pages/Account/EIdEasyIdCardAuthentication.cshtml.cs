using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.eIDEasy.IDCard.Areas.Identity.Pages.Account
{
    public class EIdEasyIdCardAuthentication : PageModel
    {
        private readonly IOptionsMonitor<EIdEasyIdCardOptions> _optionsMonitor;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        private EIdEasyIdCardOptions _options;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Token { get; set; }
        }

        public string ReturnUrl { get; set; }

        public string CardUrl => $"https://{_options.Country}.eideasy.com";

        public string ClientId => _options.ClientId;

        public EIdEasyIdCardAuthentication(IOptionsMonitor<EIdEasyIdCardOptions> optionsMonitor, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _optionsMonitor = optionsMonitor;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        public override async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            var authenticationSchemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
            var authenticationScheme = authenticationSchemes.Single(scheme => scheme.HandlerType == typeof(EIdEasyIdCardHandler));
            _options = _optionsMonitor.Get(authenticationScheme.Name);

            await base.OnPageHandlerSelectionAsync(context);
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public IActionResult OnPost(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var properties = new AuthenticationProperties { RedirectUri = returnUrl };
                properties.Items["LoginProvider"] = EIdEasyIdCardDefaults.AuthenticationScheme;
                properties.SetString("Token", Input.Token);

                return new ChallengeResult(EIdEasyIdCardDefaults.AuthenticationScheme, properties);
            }

            return Page();
        }
    }
}
