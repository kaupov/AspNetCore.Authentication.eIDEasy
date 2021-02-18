﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Authentication.eIDEasy.IDCard.Helpers
{
    internal class AuthenticationPropertiesProvider<TUser> : IAuthenticationPropertiesProvider where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;

        public AuthenticationPropertiesProvider(SignInManager<TUser> signInManager, UserManager<TUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public AuthenticationProperties ConfigureProperties(string returnUrl, ClaimsPrincipal user, string token)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                EIdEasyIdCardDefaults.AuthenticationScheme, returnUrl, _userManager.GetUserId(user));
            properties.SetString("Token", token);

            return properties;
        }
    }

    public interface IAuthenticationPropertiesProvider
    {
        AuthenticationProperties ConfigureProperties(string returnUrl, ClaimsPrincipal user, string token);
    }
}