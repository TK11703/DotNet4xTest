using DotNet4xTestWeb.Classes;
using DotNet4xTestWeb.Helpers;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DotNet4xTestWeb.Tests.Graph
{
	public partial class CurrentUserDetail : System.Web.UI.Page
	{
		public string SiteTitle { get; set; } = string.Empty;

		public UserData authenticatedUserData { get; set;} = new UserData();

		protected async void Page_Load(object sender, EventArgs e)
		{
			SiteTitle = ConfigurationManager.AppSettings["SiteTitle"];
			if (!IsPostBack && Request.IsAuthenticated)
			{
				await GetUserDataFromGraph();
			}
		}

		protected async void RefreshUserDataButton_Click(object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				await GetUserDataFromGraph();
			}
		}

		private async Task GetUserDataFromGraph()
		{
			if (Request.IsAuthenticated)
			{
				try
				{
					authenticatedUserData = await GraphHelper.GetUserDetailsAsync(Globals.GraphScopes.Split(' '));						
				}
				catch (Exception ex)
				{
					string redirect = $"/ErrorHandler?message={ex.Message}";
					Response.Redirect(redirect, false);
				}
			}
		}
	}
}