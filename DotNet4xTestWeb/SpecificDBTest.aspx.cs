using System;
using System.Text;
using System.Configuration;
using System.Web.Hosting;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace DotNet4xTestWeb
{
    public partial class SpecificDBTest : System.Web.UI.Page
    {
        public string siteTitle = string.Empty;
		private string connectionString = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
        {
            siteTitle = ConfigurationManager.AppSettings["SiteTitle"];
			connectionString = ConfigurationManager.ConnectionStrings["SQLConnection_Specific"].ConnectionString;
			GatherServerInformation();
        }

		protected void SubmitSelectionButton_Click(object sender, EventArgs e)
        {
			this.ResultsView.Visible = false;
			bool asWebIdentity = this.IdentitySelection.Value.ToLower().Equals("webidentity");
			bool asUserIdentity = this.IdentitySelection.Value.ToLower().Equals("useridentity");
			bool executeGetDate = this.CommandSelection.Value.ToLower().Equals("getdate");
			bool executeCMD = this.CommandSelection.Value.ToLower().Equals("getallusers_cmd");
			bool executeSP = this.CommandSelection.Value.ToLower().Equals("getallusers_sp");

			if(asWebIdentity && executeGetDate)
			{
				PerformGetDateCommandAsIdentity();
			}
			else if(asWebIdentity && executeCMD)
			{
				PerformCommandAsIdentity();
			}
			else if(asWebIdentity && executeSP)
			{
				PerformSPAsIdentity();
			}
			else if(asUserIdentity && executeGetDate)
			{
				PerformGetDateCommandAsUser();
			}
			else if(asUserIdentity && executeCMD)
			{
				PerformCommandAsUser();
			}
			else if(asUserIdentity && executeSP)
			{
				PerformSPAsUser();
			}
			else
			{
				this.executedAs.Text = "A command type AND identity selection need to be selected.";
			}
			this.ResultsView.Visible = true;
		}


		private void PerformGetDateCommandAsIdentity()
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

						// Use scalarResult here
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

		private void PerformGetDateCommandAsUser()
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

					// Use scalarResult here
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

		private void PerformCommandAsIdentity()
		{
			List<string> values = new List<string>();
			if (!string.IsNullOrEmpty(connectionString))
			{
				try
				{
					using (HostingEnvironment.Impersonate())
					{
						this.executedAs.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
						using (SqlConnection conn = new SqlConnection(connectionString))
						{
							string sql = "SELECT ID, FirstName, LastName from Customer";
							SqlCommand cmd = new SqlCommand(sql, conn);
							if (conn.State != System.Data.ConnectionState.Open)
							{
								conn.Open();
								SqlDataReader rdr = cmd.ExecuteReader();
								while (rdr.Read())
								{
									values.Add(string.Format("{{ \"Id\":{0}, \"FirstName\":\"{1}\", \"LastName\":\"{2}\"}}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
								}
							}
						}

						if (values.Count > 0)
						{
							this.dbResult.Text = string.Join("<br/>", values.ToArray());
						}
						else
						{
							this.dbResult.Text = "There were no results returned from the database command.";
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

		private void PerformCommandAsUser()
		{
			List<string> values = new List<string>();
			if (!string.IsNullOrEmpty(connectionString))
			{
				try
				{
					this.executedAs.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
					using (SqlConnection conn = new SqlConnection(connectionString))
					{
						string sql = "SELECT ID, FirstName, LastName from Customer";
						SqlCommand cmd = new SqlCommand(sql, conn);
						if (conn.State != System.Data.ConnectionState.Open)
						{
							conn.Open();
							SqlDataReader rdr = cmd.ExecuteReader();
							while (rdr.Read())
							{
								values.Add(string.Format("{{ \"Id\":{0}, \"FirstName\":\"{1}\", \"LastName\":\"{2}\"}}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
							}
						}
					}

					if (values.Count > 0)
					{
						this.dbResult.Text = string.Join("<br/>", values.ToArray());
					}
					else
					{
						this.dbResult.Text = "There were no results returned from the database command.";
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

		private void PerformSPAsIdentity()
		{
			List<string> values = new List<string>();
			if (!string.IsNullOrEmpty(connectionString))
			{
				try
				{
					using (HostingEnvironment.Impersonate())
					{
						this.executedAs.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
						using (SqlConnection conn = new SqlConnection(connectionString))
						{
							SqlCommand cmd = new SqlCommand("spCustomer_GetAll", conn);
							cmd.CommandType = System.Data.CommandType.StoredProcedure;
							if (conn.State != System.Data.ConnectionState.Open)
							{
								conn.Open();
								SqlDataReader rdr = cmd.ExecuteReader();
								while (rdr.Read())
								{
									values.Add(string.Format("{{ \"Id\":{0}, \"FirstName\":\"{1}\", \"LastName\":\"{2}\"}}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
								}
							}
						}

						if (values.Count > 0)
						{
							this.dbResult.Text = string.Join("<br/>", values.ToArray());
						}
						else
						{
							this.dbResult.Text = "There were no results returned from the database command.";
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

		private void PerformSPAsUser()
		{
			List<string> values = new List<string>();
			if (!string.IsNullOrEmpty(connectionString))
			{
				try
				{
					this.executedAs.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
					using (SqlConnection conn = new SqlConnection(connectionString))
					{
						SqlCommand cmd = new SqlCommand("spCustomer_GetAll", conn);
						cmd.CommandType = System.Data.CommandType.StoredProcedure;
						if (conn.State != System.Data.ConnectionState.Open)
						{
							conn.Open();
							SqlDataReader rdr = cmd.ExecuteReader();
							while (rdr.Read())
							{
								values.Add(string.Format("{{ \"Id\":{0}, \"FirstName\":\"{1}\", \"LastName\":\"{2}\"}}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
							}
						}
					}

					if (values.Count > 0)
					{
						this.dbResult.Text = string.Join("<br/>", values.ToArray());
					}
					else
					{
						this.dbResult.Text = "There were no results returned from the database command.";
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