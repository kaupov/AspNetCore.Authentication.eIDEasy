using System;
using AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy;
using AspNetCore.Authentication.eIDEasy.IDCard.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Authentication.eIDEasy.IDCard
{
    public static class EIdEasyIdCardExtensions
    {
        public static AuthenticationBuilder AddEIdEasyIdCard<TUser>(this AuthenticationBuilder builder) where TUser : class =>
            builder.AddEIdEasyIdCard<TUser>(EIdEasyIdCardDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddEIdEasyIdCard<TUser>(this AuthenticationBuilder builder,
            Action<EIdEasyIdCardOptions> configureOptions) where TUser : class =>
            builder.AddEIdEasyIdCard<TUser>(EIdEasyIdCardDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddEIdEasyIdCard<TUser>(this AuthenticationBuilder builder, string authenticationScheme,
            Action<EIdEasyIdCardOptions> configureOptions) where TUser : class =>
            builder.AddEIdEasyIdCard<TUser>(authenticationScheme, EIdEasyIdCardDefaults.DisplayName, EIdEasyIdCardDefaults.ServerCertificatePublicKey, configureOptions);

        public static AuthenticationBuilder AddEIdEasyIdCard<TUser>(this AuthenticationBuilder builder,
            string authenticationScheme, string displayName, string serverCertificatePublicKey,
            Action<EIdEasyIdCardOptions> configureOptions) where TUser : class
        {
            builder.Services.AddHttpClient<EIdEasyClient>().ConfigurePrimaryHttpMessageHandler(() => new EIdEasyHttpClientHandler(serverCertificatePublicKey));
            builder.Services.AddTransient<IAuthenticationPropertiesProvider, AuthenticationPropertiesProvider<TUser>>();

            return builder.AddScheme<EIdEasyIdCardOptions, EIdEasyIdCardHandler>(authenticationScheme, displayName,
                configureOptions);
        }
    }
}