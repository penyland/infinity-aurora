using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Infinity.Web.Extensions;

public static class AuthenticationExtensions
{
    public static WebApplicationBuilder AddAuthenticationServices(this WebApplicationBuilder builder)
    {
        // Configure authentication and authorization
        var initialScopes = builder.Configuration["MicrosoftGraph:Scopes"]?.Split(' ');

        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
                    .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
                        .AddInMemoryTokenCaches();

        builder.Services.AddAuthentication()
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi()
                    .AddInMemoryTokenCaches();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Reader", policy => { policy.RequireRole("reader"); });
        });

        // This is required to be instantiated before the OpenIdConnectOptions starts getting configured.
        // By default, the claims mapping will map claim names in the old format to accommodate older SAML applications.
        // 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' instead of 'roles'
        // This flag ensures that the ClaimsIdentity claims collection will be built from the claims in the token
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        // Application should redirect to / when logged out.
        builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.Events.OnSignedOutCallbackRedirect = context =>
            {
                context.HttpContext.Response.Redirect(context.Options.SignedOutRedirectUri);
                context.HandleResponse();
                return Task.CompletedTask;
            };
        });

        return builder;
    }
}
