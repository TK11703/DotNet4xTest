using System;
using System.Web;
using System.IO;

namespace DotNet4xTestWeb.HttpHandlers
{
	public class ImageViewer : IHttpHandler
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
			string imageRelPath = context.Request.QueryString["fn"];
			if (!string.IsNullOrEmpty(imageRelPath))
			{
				imageRelPath = imageRelPath.Replace("/", "\\");
				string rootDir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
				string imagePath = Path.Combine(rootDir, imageRelPath);
				if (File.Exists(imagePath))
				{
					context.Response.Cache.SetCacheability(HttpCacheability.Public);
					context.Response.ClearHeaders();
					context.Response.ContentType = "image/jpeg";
					try
					{
						byte[] imageData = File.ReadAllBytes(imagePath);
						using (MemoryStream memoryStream = new MemoryStream(imageData))
						{
							memoryStream.WriteTo(context.Response.OutputStream);
						}
					}
					catch (Exception ex)
					{
						OutputError("IMAGE COULD NOT BE DISPLAYED. DETAILS: " + ex.Message.ToUpper(), context);
					}
				}
				else
				{
					OutputError("IMAGE COULD NOT BE DISPLAYED. THE IMAGE DOES NOT EXIST.", context);
				}
			}
			else
			{
				OutputError("IMAGE COULD NOT BE DISPLAYED. THE IMAGE RELATIVE PATH WAS NOT VALID.", context);
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
