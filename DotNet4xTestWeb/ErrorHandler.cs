using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using DotNet4xTestWeb.Classes;

namespace DotNet4xTestWeb
{
	public class ErrorHandler : IHttpHandler, IRequiresSessionState
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
			Flash(context, context.Request.QueryString["message"], context.Request.QueryString["debug"]);
			context.Response.Redirect("Error.aspx");
		}

		#endregion

		protected void Flash(HttpContext context, string message, string debug = null)
		{
			var sessionAlerts = context.Session[Alert.AlertKey] != null ? (List<Alert>)context.Session[Alert.AlertKey] : new List<Alert>();

			sessionAlerts.Add(new Alert
			{
				Message = message,
				Debug = debug
			});

			context.Session[Alert.AlertKey] = sessionAlerts;
		}
	}
}
