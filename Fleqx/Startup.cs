using Fleqx;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Fleqx
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "ApplicationCookie",
				LoginPath = new PathString("/Fleqx/Security/Login")
			});

			app.MapSignalR();
		}
	}
}