using System.Web.Mvc;

namespace Fleqx
{
	public class Filters
	{
		// Add more flobal filters.
		public static void AddGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new AuthorizeAttribute());
		}
	}
}