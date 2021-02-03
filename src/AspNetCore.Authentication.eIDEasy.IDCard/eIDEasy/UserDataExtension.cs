using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy
{
    public static class UserDataExtension
    {
        public static ClaimsIdentity GetIdentity(this UserData userData)
        {
            var identity = new ClaimsIdentity(new List<Claim>(), EIdEasyIdCardDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, $"{userData.Country}-{userData.Idcode}"));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, userData.Firstname));
            identity.AddClaim(new Claim(ClaimTypes.Surname, userData.Lastname));
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, userData.Idcode));
            identity.AddClaim(new Claim(ClaimTypes.Country, userData.Country));

            return identity;
        }

        public static void EnsureValid(this UserData userData)
        {
            if (userData.Status != "OK")
                throw new EIdEasyTroubleException("Invalid status");

            if (userData.CurrentLoginInfo.ValidFrom > DateTime.UtcNow)
                throw new EIdEasyTroubleException("Document not valid yet");
            
            if (userData.CurrentLoginInfo.ValidTo < DateTime.UtcNow)
                throw new EIdEasyTroubleException("Document expired");
        }
    }
}
