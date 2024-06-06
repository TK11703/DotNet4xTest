using System;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DotNet4xTestWeb
{
    public partial class ServerVariables : System.Web.UI.Page
    {
        public string siteTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            siteTitle = ConfigurationManager.AppSettings["SiteTitle"];
            GatherServerInformation();
        }

        private void GatherServerInformation()
        {
            this.ServerVariableDetails.DataSource = Request.ServerVariables;
            this.ServerVariableDetails.DataBind();
        }

        protected void ServerVariableDetails_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label variableKey = (Label)e.Item.FindControl("VariableIDLabel");
            Label variableValue = (Label)e.Item.FindControl("VariableValueLabel");
            if (!string.IsNullOrEmpty(variableKey.Text))
            {
                if (variableKey.Text.ToUpper() == "AUTH_PASSWORD")
                {
                    variableValue.Text = "[REMOVED FOR SECURITY PURPOSES]";
                }
            }
        }
    }
}