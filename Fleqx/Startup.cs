using Fleqx;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Fleqx
{
	public class Startup
	{
		public static UserManager<User> UserManager { get; private set; }

		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "ApplicationCookie",
				LoginPath = new PathString("/Security/Login")
			});

			UserManager = new UserManager<User>(new UserStore<User>(new DatabaseContext()));

			UserManager.UserValidator = new UserValidator<User>(UserManager)
			{
				AllowOnlyAlphanumericUserNames = false
			};

			app.MapSignalR();
		}
	}
}