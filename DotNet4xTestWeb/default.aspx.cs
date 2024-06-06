using System;
using System.Configuration;

namespace DotNet4xTestWeb
{
    public partial class _default : System.Web.UI.Page
    {
        public string siteTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            siteTitle = ConfigurationManager.AppSettings["SiteTitle"];
        }
    }
}