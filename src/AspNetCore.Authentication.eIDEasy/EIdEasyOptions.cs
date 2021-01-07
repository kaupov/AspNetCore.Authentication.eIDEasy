using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Authentication.eIDEasy
{
    public class EIdEasyOptions : OAuthOptions
    {
        public EIdEasyOptions()
        {
            CallbackPath = new PathString("/signin-eideasy");
            AuthorizationEndpoint = EIdEasyDefaults.AuthorizationEndpoint;
            TokenEndpoint = EIdEasyDefaults.TokenEndpoint;
            UserInformationEndpoint = EIdEasyDefaults.UserInformationEndpoint;

            ClaimActions.MapCustomJson(ClaimTypes.NameIdentifier,
                o => o.GetString("country") + o.GetString("idcode"));
            ClaimActions.MapJsonKey(ClaimTypes.Country, "country");
            ClaimActions.MapJsonKey(ClaimTypes.SerialNumber, "idcode");
            ClaimActions.MapJsonKey(ClaimTypes.GivenName, "firstname");
            ClaimActions.MapJsonKey(ClaimTypes.Surname, "lastname");
        }

        // See EIdEasyMethods
        public string Method { get; set; }
    }
}