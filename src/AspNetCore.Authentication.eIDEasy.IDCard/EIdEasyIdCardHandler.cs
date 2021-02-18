using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.eIDEasy.IDCard
{
    public class EIdEasyIdCardHandler : AuthenticationHandler<EIdEasyIdCardOptions>
    {
        private readonly EIdEasyClient _eIdEasyClient;

        public EIdEasyIdCardHandler(IOptionsMonitor<EIdEasyIdCardOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, EIdEasyClient eIdEasyClient) : base(options, logger,
            encoder, clock)
        {
            _eIdEasyClient = eIdEasyClient;
        }

        protected override Task InitializeHandlerAsync()
        {
            _eIdEasyClient.Options = Options;

            return base.InitializeHandlerAsync();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            if (string.IsNullOrEmpty(properties.RedirectUri))
            {
                properties.RedirectUri = OriginalPathBase + OriginalPath + Request.QueryString;
            }

            var token = properties.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                Context.Response.Redirect(
                    $"/Identity/Account/EIdEasyIdCardAuthentication?returnUrl={UrlEncoder.Default.Encode(properties.RedirectUri)}");
                return;
            }

            await LoginCompletionAsync(token, properties);
        }

        private async Task LoginCompletionAsync(string token, AuthenticationProperties properties)
        {
            var userData = await _eIdEasyClient.PostIdCardComplete(token);

            userData.EnsureValid();

            await Context.SignInAsync(IdentityConstants.ExternalScheme,
                new ClaimsPrincipal(userData.GetIdentity()), properties);
            Context.Response.Redirect(properties.RedirectUri);
        }
    }
}