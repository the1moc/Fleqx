using System.Security.Claims;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Helper;

namespace Fleqx.Controllers
{
	[Authorize]
	public abstract class BaseController : Controller
	{
		// The current user.
		public UserClaims CurrentUser
		{
			get { return new UserClaims(User as ClaimsPrincipal); }
		}

		// Get a new instance of the database context.
		public virtual DatabaseContext GetDatabaseContext()
		{
			return new DatabaseContext();
		}
	}
}