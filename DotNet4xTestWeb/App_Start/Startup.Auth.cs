using DotNet4xTestWeb.Helpers;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNet4xTestWeb
{
	public partial class Startup
	{
		public void ConfigureAuth(IAppBuilder app)
		{
			//IdentityModelEventSource.ShowPII = true;
			app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

			app.UseCookieAuthentication(new CookieAuthenticationOptions { });

			app.UseOpenIdConnectAuthentication(
				new OpenIdConnectAuthenticationOptions
				{
					Authority = Globals.Authority,
					ClientId = Globals.ClientId,
					Scope = Globals.BasicSignInScopes, // a basic set of permissions for user sign in & profile access
					RedirectUri = Globals.RedirectUri,
					PostLogoutRedirectUri = Globals.PostLogoutRedirectUri,
					TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						NameClaimType = "name",
						ValidIssuers = new List<string>
						{
							string.Format("https://sts.windows.net/{0}/", Globals.TenantId)
						}
					},
					Notifications = new OpenIdConnectAuthenticationNotifications()
					{
						AuthenticationFailed = OnAuthenticationFailed,
						AuthorizationCodeReceived = OnAuthorizationCodeReceivedAsync,
						SecurityTokenValidated = OnSecurityTokenValidated
					},
					// Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/samesite/owin-samesite
					CookieManager = new SameSiteCookieManager(new SystemWebCookieManager())
				}
			);

			// This makes any middleware defined above this line run before the Authorization rule is applied in web.config. Use this is you want to authenticate right away.
			//app.UseStageMarker(PipelineStage.Authenticate);
		}

		private static Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context)
		{
			string redirect = $"/ErrorHandler?message={context.Exception.Message}";
			if (context.ProtocolMessage != null && !string.IsNullOrEmpty(context.ProtocolMessage.ErrorDescription))
			{
				redirect += $"&debug={context.ProtocolMessage.ErrorDescription}";
			}
			context.OwinContext.Response.Redirect(redirect);
			context.HandleResponse();
			return Task.FromResult(0);
		}

		private async Task OnAuthorizationCodeReceivedAsync(AuthorizationCodeReceivedNotification context)
		{
			/*
			 The `MSALPerUserMemoryTokenCache` is created and hooked in the `UserTokenCache` used by `IConfidentialClientApplication`.
			 At this point, if you inspect `ClaimsPrinciple.Current` you will notice that the Identity is still unauthenticated and it has no claims,
			 but `MSALPerUserMemoryTokenCache` needs the claims to work properly. Because of this sync problem, we are using the constructor that
			 receives `ClaimsPrincipal` as argument and we are getting the claims from the object `AuthorizationCodeReceivedNotification context`.
			 This object contains the property `AuthenticationTicket.Identity`, which is a `ClaimsIdentity`, created from the token received from 
			 Azure AD and has a full set of claims.
			 */
			IConfidentialClientApplication confidentialClient = MsalAppBuilder.BuildConfidentialClientApplication(new ClaimsPrincipal(context.AuthenticationTicket.Identity));

			// Upon successful sign in, get & cache a token using MSAL
			AuthenticationResult result = await confidentialClient.AcquireTokenByAuthorizationCode(new[] { "user.readbasic.all" }, context.Code).ExecuteAsync();
		}

		private Task OnSecurityTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context)
		{
			// Verify the user signing in is a business user, not a consumer user.
			string[] issuer = context.AuthenticationTicket.Identity.FindFirst(Globals.IssuerClaim).Value.Split('/');
			string tenantId = issuer[(issuer.Length - 2)];
			if (tenantId != Globals.TenantId)
			{
				throw new System.IdentityModel.Tokens.SecurityTokenValidationException("Only Entra accounts within this tenant are supported. Please log in with an account found within the tenant.");
			}

			var claims = context.AuthenticationTicket.Identity.Claims;
			var groups = from c in claims
						 where c.Type == "groups"
						 select c;
			foreach (var group in groups)
			{
				context.AuthenticationTicket.Identity.AddClaim(new Claim(ClaimTypes.Role, group.Value));
			}
			return Task.FromResult(0);
		}
	}
}