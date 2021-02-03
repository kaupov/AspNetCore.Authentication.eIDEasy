using System;
using AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Authentication.eIDEasy.IDCard
{
    public static class EIdEasyIdCardExtensions
    {
        public static AuthenticationBuilder AddEIdEasyIdCard(this AuthenticationBuilder builder) =>
            builder.AddEIdEasyIdCard(EIdEasyIdCardDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddEIdEasyIdCard(this AuthenticationBuilder builder,
            Action<EIdEasyIdCardOptions> configureOptions) =>
            builder.AddEIdEasyIdCard(EIdEasyIdCardDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddEIdEasyIdCard(this AuthenticationBuilder builder, string authenticationScheme,
            Action<EIdEasyIdCardOptions> configureOptions) =>
            builder.AddEIdEasyIdCard(authenticationScheme, EIdEasyIdCardDefaults.DisplayName, EIdEasyIdCardDefaults.ServerCertificatePublicKey, configureOptions);

        public static AuthenticationBuilder AddEIdEasyIdCard(this AuthenticationBuilder builder,
            string authenticationScheme, string displayName, string serverCertificatePublicKey,
            Action<EIdEasyIdCardOptions> configureOptions)
        {
            builder.Services.AddHttpClient<EIdEasyClient>().ConfigurePrimaryHttpMessageHandler(() => new EIdEasyHttpClientHandler(serverCertificatePublicKey));

            return builder.AddScheme<EIdEasyIdCardOptions, EIdEasyIdCardHandler>(authenticationScheme, displayName,
                configureOptions);
        }
    }
}