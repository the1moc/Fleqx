using System.Web.Mvc;

namespace Fleqx.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		/// <summary>
		/// Return the home view.
		/// </summary>
		/// <returns></returns>
		public ActionResult Home()
		{
			return View("Home");
		}
	}
}