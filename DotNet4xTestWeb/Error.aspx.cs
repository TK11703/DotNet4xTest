using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNet4xTestWeb.Classes;

namespace DotNet4xTestWeb
{
	public partial class Error : System.Web.UI.Page
	{
		public string SiteTitle { get; set; } = string.Empty;

		private List<Alert> alerts = new List<Alert>();

		protected void Page_Load(object sender, EventArgs e)
		{
			SiteTitle = "An Error Occurred";
			
			alerts = Session[Alert.AlertKey] != null ? (List<Alert>)Session[Alert.AlertKey] : new List<Alert>();
			if(alerts.Count > 0)
			{
				AlertRepeater.DataSource = alerts;
				AlertRepeater.DataBind();
				ClearFlash();
			}
			else
			{
				Response.Redirect("Default.aspx");
			}
		}

		private void ClearFlash()
		{
			Session[Alert.AlertKey] = null;
		}
	}
}