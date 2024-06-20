using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNet4xTestWeb
{
	public partial class Startup
	{
		private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
		private static string aadInstance = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);
		private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
		private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
		private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
		private static string appSecret = ConfigurationManager.AppSettings["ida:AppSecret"];
		private static string graphScopes = ConfigurationManager.AppSettings["ida:AppScopes"];

		string authority = aadInstance + tenantId;

		public void ConfigureAuth(IAppBuilder app)
		{
			app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				CookieSameSite = SameSiteMode.None,
				CookieSecure = CookieSecureOption.Always,
				CookieHttpOnly = true,
				ExpireTimeSpan = TimeSpan.FromMinutes(60)
			});

			app.UseOpenIdConnectAuthentication(
			new OpenIdConnectAuthenticationOptions
			{
				ClientId = clientId,
				Authority = authority,
				Scope = $"openid email profile offline_access {graphScopes}",
				ResponseType = OpenIdConnectResponseType.IdToken,
				RedirectUri = redirectUri,
				PostLogoutRedirectUri = postLogoutRedirectUri,				

				Notifications = new OpenIdConnectAuthenticationNotifications()
				{
					AuthenticationFailed = OnAuthenticationFailedAsync,
					AuthorizationCodeReceived = OnAuthorizationCodeReceivedAsync,
					SecurityTokenValidated = (context) =>
					{
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
				},
				ProtocolValidator = new OpenIdConnectProtocolValidator
				{
					RequireState = false,
					NonceLifetime = TimeSpan.FromMinutes(60),
					RequireNonce = true,
					RequireStateValidation = false
				},
				CookieManager = new SystemWebCookieManager()
			});

			// This makes any middleware defined above this line run before the Authorization rule is applied in web.config
			app.UseStageMarker(PipelineStage.Authenticate);
		}

		private static string EnsureTrailingSlash(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}

			if (!value.EndsWith("/", StringComparison.Ordinal))
			{
				return value + "/";
			}

			return value;
		}

		private static Task OnAuthenticationFailedAsync(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
		{
			notification.HandleResponse();
			string redirect = $"/ErrorHandler?message={notification.Exception.Message}";
			if (notification.ProtocolMessage != null && !string.IsNullOrEmpty(notification.ProtocolMessage.ErrorDescription))
			{
				redirect += $"&debug={notification.ProtocolMessage.ErrorDescription}";
			}
			notification.Response.Redirect(redirect);
			return Task.FromResult(0);
		}

		private async Task OnAuthorizationCodeReceivedAsync(AuthorizationCodeReceivedNotification notification)
		{
			notification.HandleCodeRedemption();

			var idClient = ConfidentialClientApplicationBuilder.Create(clientId)
				.WithAuthority(EnsureTrailingSlash(aadInstance) + tenantId)
				.WithRedirectUri(redirectUri)
				.WithClientSecret(appSecret)
				.Build();

			string message;
			string debug;

			try
			{
				string[] scopes = graphScopes.Split(' ');

				var result = await idClient.AcquireTokenByAuthorizationCode(scopes, notification.Code).ExecuteAsync();

				message = "Access token retrieved.";
				debug = result.AccessToken;
				notification.HandleCodeRedemption(result.AccessToken, result.IdToken);
			}
			catch (MsalException ex)
			{
				message = "AcquireTokenByAuthorizationCodeAsync threw an exception";
				var queryString = $"message={message}&debug={ex.Message}";
				if (queryString.Length > 2048)
				{
					queryString = queryString.Substring(0, 2040) + "...";
				}

				notification.HandleResponse();
				notification.Response.Redirect($"/ErrorHandler?{queryString}");
			}
		}
	}
}