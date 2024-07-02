using DotNet4xTestWeb.Helpers;
using System;
using System.Configuration;

namespace DotNet4xTestWeb
{
    public partial class _default : System.Web.UI.Page
    {
		public string SiteTitle { get; set; } = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
        {
            SiteTitle = ConfigurationManager.AppSettings["SiteTitle"];
		}
	}
}