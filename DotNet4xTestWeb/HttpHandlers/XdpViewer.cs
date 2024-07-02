using System;
using System.Web;
using System.IO;

namespace DotNet4xTestWeb.HttpHandlers
{
	public class XdpViewer : IHttpHandler
	{
		/// <summary>
		/// You will need to configure this handler in the Web.config file of your 
		/// web and register it with IIS before being able to use it. For more information
		/// see the following link: https://go.microsoft.com/?linkid=8101007
		/// </summary>
		#region IHttpHandler Members

		public bool IsReusable
		{
			// Return false in case your Managed Handler cannot be reused for another request.
			// Usually this would be false in case you have some state information preserved per request.
			get { return true; }
		}

		public void ProcessRequest(HttpContext context)
		{
			try
			{
				string fileName = "Generated.xdp";
				byte[] dataToSend = File.ReadAllBytes(context.Server.MapPath("/Tests/PDF/example.xdp"));
				if (dataToSend.Length > 0)
				{
					context.Response.ClearHeaders();
					context.Response.ContentType = "application/vnd.adobe.xdp+xml";
					context.Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
					context.Response.Charset = string.Empty;
					context.Response.Buffer = true;
					context.Response.BinaryWrite(dataToSend);
				}
				else
				{
					throw new InvalidOperationException("The file contents were empty.");
				}
			}
			catch (Exception ex)
			{
				OutputError("FILE CONTENTS COULD NOT BE DISPLAYED. DETAILS: " + ex.Message.ToUpper(), context);
			}
			finally
			{
				context.Response.End();
			}
		}

		#endregion

		private void OutputError(string error, HttpContext context)
		{
			context.Response.ClearHeaders();
			context.Response.Write(error);
		}
	}
}
