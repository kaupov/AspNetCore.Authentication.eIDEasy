using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.Authentication.eIDEasy
{
    public class EIdEasyHandler : OAuthHandler<EIdEasyOptions>
    {
        public EIdEasyHandler(IOptionsMonitor<EIdEasyOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"An error occurred when retrieving eIDEasy user information ({response.StatusCode}). Please check if the authentication information is correct.");
            }

            using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload.RootElement);
            context.RunClaimActions();
            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var state = Options.StateDataFormat.Protect(properties);
            var parameters = new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "redirect_uri", redirectUri },
                { "response_type", "code" },
                { "state", state },
                { "lang", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName }
            };

            if (!string.IsNullOrEmpty(Options.Method))
                parameters.Add("method", Options.Method);

            return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, parameters);
        }
    }
}