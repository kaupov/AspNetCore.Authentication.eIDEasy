using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Authentication.eIDEasy.IDCard.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.eIDEasy.IDCard.Areas.Identity.Pages.Account
{
    public class EIdEasyIdCardAuthentication : PageModel
    {
        private readonly IAuthenticationPropertiesProvider _authenticationPropertiesProvider;
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

        public string CardUrl { get; set; }

        public string ClientId { get; set; }

        public EIdEasyIdCardAuthentication(IOptionsMonitor<EIdEasyIdCardOptions> optionsMonitor, IAuthenticationSchemeProvider authenticationSchemeProvider, IAuthenticationPropertiesProvider authenticationPropertiesProvider)
        {
            _optionsMonitor = optionsMonitor;
            _authenticationSchemeProvider = authenticationSchemeProvider;
            _authenticationPropertiesProvider = authenticationPropertiesProvider;
        }

        public override async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            var authenticationSchemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
            var authenticationScheme = authenticationSchemes.Single(scheme => scheme.HandlerType == typeof(EIdEasyIdCardHandler));
            _options = _optionsMonitor.Get(authenticationScheme.Name);

            CardUrl = $"https://{_options.Country}.eideasy.com";
            ClientId = _options.ClientId;

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
                var properties = _authenticationPropertiesProvider.ConfigureProperties(returnUrl, User, Input.Token);

                return new ChallengeResult(EIdEasyIdCardDefaults.AuthenticationScheme, properties);
            }

            return Page();
        }
    }
}
