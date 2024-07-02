using DotNet4xTestWeb.Helpers;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using System;
using System.Web;

namespace DotNet4xTestWeb
{
	public partial class authenticationDisplay : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Nothing displayed here on load
		}

		protected void LoginButton_Click(object sender, EventArgs e)
		{
			var redirectUrl = Globals.RedirectUri ?? "/Users";
			// Trigger a sign in using a basic set of scopes
			Request.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, OpenIdConnectAuthenticationDefaults.AuthenticationType);
		}

		protected void LogoutButton_Click(object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				MsalAppBuilder.ClearUserTokenCache().Wait();
				Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
				Response.Redirect("/");
			}
		}
	}
}