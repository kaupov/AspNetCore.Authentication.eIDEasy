using Microsoft.AspNetCore.Authentication;

namespace AspNetCore.Authentication.eIDEasy.IDCard
{
    public class EIdEasyIdCardOptions : AuthenticationSchemeOptions
    {
        public string ClientSecret { get; set; }

        public string ClientId { get; set; }

        // ee, be, lv, lt, pt, rs
        public string Country { get; set; }
    }
}