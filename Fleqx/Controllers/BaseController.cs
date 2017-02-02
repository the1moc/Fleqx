using System.Security.Claims;
using System.Web.Mvc;
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
	}
}