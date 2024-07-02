using System;
using System.Configuration;

namespace DotNet4xTestWeb.Helpers
{
	public static class Globals
	{
		public const string IssuerClaim = "iss";
		public const string BasicSignInScopes = "openid profile email offline_access user.readbasic.all";
		public const string NameClaimType = "name";

		/// <summary>
		/// The Client ID is used by the application to uniquely identify itself to Azure AD.
		/// </summary>
		public static string ClientId { get; } = ConfigurationManager.AppSettings["ida:ClientId"];

		/// <summary>
		/// The ClientSecret is a credential used to authenticate the application to Azure AD.  Azure AD supports password and certificate credentials.
		/// </summary>
		public static string ClientSecret { get; } = ConfigurationManager.AppSettings["ida:AppSecret"];

		/// <summary>
		/// The Post Logout Redirect Uri is the URL where the user will be redirected after they sign out.
		/// </summary>
		public static string PostLogoutRedirectUri { get; } = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

		/// <summary>
		/// The TenantId is the DirectoryId of the Azure AD tenant being used in the sample
		/// </summary>
		public static string TenantId { get; } = ConfigurationManager.AppSettings["ida:TenantId"];

		public static string AADInstance { get; } = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:AADInstance"]);
		public static string Domain { get; } = ConfigurationManager.AppSettings["ida:Domain"];
		public static string RedirectUri { get; } = ConfigurationManager.AppSettings["ida:RedirectUri"];		
		public static string GraphResourceId { get; } = ConfigurationManager.AppSettings["ida:GraphAPIEndpoint"];
		public static string GraphScopes { get; } = ConfigurationManager.AppSettings["ida:AppScopes"];

		public static string Authority { get; } = AADInstance + Domain;

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
	}
}