using DotNet4xTestWeb.Classes;
using DotNet4xTestWeb.Helpers;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace DotNet4xTestWeb.Tests.Graph
{
	public partial class EntraUsers : System.Web.UI.Page
	{
		public string SiteTitle { get; set; } = string.Empty;

		public UserData authenticatedUserData { get; set;} = new UserData();

		protected async void Page_Load(object sender, EventArgs e)
		{
			SiteTitle = ConfigurationManager.AppSettings["SiteTitle"];
			if (!IsPostBack && Request.IsAuthenticated)
			{
				await GetUsersFromGraph();
			}
		}

		protected async void RefreshUserDataButton_Click(object sender, EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				await GetUsersFromGraph();
			}
		}

		private async Task GetUsersFromGraph()
		{
			if (Request.IsAuthenticated)
			{
				try
				{
					List<string> users = await GraphHelper.GetUserListAsync(Globals.GraphScopes.Split(' '));
					this.userList.DataSource = users;
					this.userList.DataBind();
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