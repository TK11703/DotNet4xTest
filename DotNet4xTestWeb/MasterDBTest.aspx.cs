using System;
using System.Text;
using System.Configuration;
using System.Web.Hosting;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace DotNet4xTestWeb
{
    public partial class MasterDBTest : System.Web.UI.Page
    {
        public string siteTitle = string.Empty;
		private string connectionString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            siteTitle = ConfigurationManager.AppSettings["SiteTitle"];
			connectionString = ConfigurationManager.ConnectionStrings["SQLConnection_Master"].ConnectionString;
			GatherServerInformation();
        }

		protected void SubmitSelectionButton_Click(object sender, EventArgs e)
        {
			this.ResultsView.Visible = false;
            if(this.IdentitySelection.Value.ToLower().Equals("webidentity"))
			{
				PerformDBTestCommandAsIdentity();
			}
			else if(this.IdentitySelection.Value.ToLower().Equals("useridentity"))
			{
				PerformDBTestCommandAsUser();
			}
			else
			{
				this.executedAs.Text = "No identity was selected.";
			}
			this.ResultsView.Visible = true;
		}


		private void PerformDBTestCommandAsIdentity()
        {
			object scalarResult = null; 
			if (!string.IsNullOrEmpty(connectionString))
			{
				try
				{
					using (HostingEnvironment.Impersonate())
					{
						this.executedAs.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
						using (SqlConnection conn = new SqlConnection(connectionString))
						{
							string sql = "SELECT getdate()";
							SqlCommand cmd = new SqlCommand(sql, conn);
							if (conn.State != System.Data.ConnectionState.Open)
							{
								conn.Open();
								scalarResult = cmd.ExecuteScalar();
							}
						}

						if (scalarResult != null && scalarResult != DBNull.Value)
						{
							this.dbResult.Text = Convert.ToDateTime(scalarResult).ToString();
						}
						else
						{
							this.dbResult.Text = "The value returned from the database call was empty or null.";
						}
					}
				}
				catch (Exception ex)
				{
					this.dbResult.Text = GetExceptionMessages(ex, "<br/>");
				}
			}
			else
			{
				this.dbResult.Text = "A connection string was not found within this application's configuration. The test was not processed any further.";
			}
		}

		private void PerformDBTestCommandAsUser()
		{
			object scalarResult = null;
			if (!string.IsNullOrEmpty(connectionString))
			{
				try
				{
					this.executedAs.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
					using (SqlConnection conn = new SqlConnection(connectionString))
					{
						string sql = "SELECT getdate()";
						SqlCommand cmd = new SqlCommand(sql, conn);
						if (conn.State != System.Data.ConnectionState.Open)
						{
							conn.Open();
							scalarResult = cmd.ExecuteScalar();
						}
					}

					if (scalarResult != null && scalarResult != DBNull.Value)
					{
						this.dbResult.Text = Convert.ToDateTime(scalarResult).ToString();
					}
					else
					{
						this.dbResult.Text = "The value returned from the database call was empty or null.";
					}
				}
				catch (Exception ex)
				{
					this.dbResult.Text = GetExceptionMessages(ex, "<br/>");
				}
			}
			else
			{
				this.dbResult.Text = "A connection string was not found within this application's configuration. The test was not processed any further.";
			}
		}

		private void GatherServerInformation()
        {
            StringBuilder sb = new StringBuilder("<ul>");
            try
            {
                if (Request.ServerVariables != null && Request.ServerVariables.Count > 0)
                {
                    if (Request.ServerVariables["AUTH_TYPE"] != null)
                    {
                        sb.AppendFormat("<li><strong>{0}:</strong>&nbsp;{1}</li>", "AUTH_TYPE", Request.ServerVariables["AUTH_TYPE"]);
                    }
                    if (Request.ServerVariables["HTTP_USER_AGENT"] != null)
                    {
                        sb.AppendFormat("<li><strong>{0}:</strong>&nbsp;{1}</li>", "HTTP_USER_AGENT", Request.ServerVariables["HTTP_USER_AGENT"]);
                    }
                }
                else
                {
                    sb.Append("<li>No server variables were detected...</li>");
                }
            }
            catch (Exception ex)
            {
                sb.AppendFormat("<li><strong>Exception Information:</strong>&nbsp;{0}</li>", GetExceptionMessages(ex, "<br/>"));
            }
            sb.Append("</ul>");
            this.serverVariableResults.Text = sb.ToString();
        }

        public string GetExceptionMessages(Exception err, string seperator)
        {
            string ret = string.Empty;
            if (err.InnerException == null)
            {
                return seperator + err.Message;
            }
            else
            {
                ret += seperator + err.Message + GetExceptionMessages(err.InnerException, seperator);
            }
            return ret;
        }
    }
}