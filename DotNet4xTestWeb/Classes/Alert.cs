using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNet4xTestWeb.Classes
{
	public class Alert
	{
		public const string AlertKey = "TempDataAlerts";
		public string Message { get; set; }
		public string Debug { get; set; }
	}
}