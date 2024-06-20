using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(DotNet4xTestWeb.Startup))]

namespace DotNet4xTestWeb
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}