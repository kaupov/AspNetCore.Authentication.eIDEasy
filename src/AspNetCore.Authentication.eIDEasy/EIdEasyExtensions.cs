using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Authentication.eIDEasy
{
    public static class EIdEasyExtensions
    {
        public static AuthenticationBuilder AddEIdEasy(this AuthenticationBuilder builder) =>
            builder.AddEIdEasy(EIdEasyDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddEIdEasy(this AuthenticationBuilder builder,
            Action<EIdEasyOptions> configureOptions) =>
            builder.AddEIdEasy(EIdEasyDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddEIdEasy(this AuthenticationBuilder builder, string authenticationScheme,
            Action<EIdEasyOptions> configureOptions) =>
            builder.AddEIdEasy(authenticationScheme, EIdEasyDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddEIdEasy(this AuthenticationBuilder builder, string authenticationScheme,
            string displayName, Action<EIdEasyOptions> configureOptions) =>
            builder.AddOAuth<EIdEasyOptions, EIdEasyHandler>(authenticationScheme, displayName, configureOptions);
    }
}