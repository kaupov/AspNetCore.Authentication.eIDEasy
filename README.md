# AspNetCore.Authentication.eIDEasy

**AspNetCore.Authentication.eIDEasy** has a eIDEasy security middleware that you can use in your **ASP.NET Core** application to support eIDEasy provided authentications.

eIDEasy provides several eID authentication methods like ID-card, Mobile-ID and Smart-ID. See more at their website [eideasy.com](https://eideasy.com/developer-documentation/).

## Getting started
Register youself at **[eideasy.com](https://eideasy.com/)** and configure your site there.

Add redirect URI ending with `/signin-eideasy` like `https://localhost:5001/signin-eideasy` and take notes of **Oauth client_id/secret**, you will need them.

Add your client id and client secret to your configuration.

Add following lines to your `Startup` class:
```AspNetCore
public void ConfigureServices(IServiceCollection services)
{
    services.AddAuthentication()
        .AddEIdEasy(options =>
        {
            options.ClientId = Configuration["eIDEasyClientId"];
            options.ClientSecret = Configuration["eIDEasyClientSecret"];
        });
}

public void Configure(IApplicationBuilder app)
{
    app.UseAuthentication();
    app.UseAuthorization();
}
```
See the [/sample](https://github.com/kaupov/AspNetCore.Authentication.eIDEasy/tree/main/sample) directory for a complete sample **using ASP.NET Core MVC**.