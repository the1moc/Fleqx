using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;

namespace Fleqx.Controllers
{
	/// <summary>
	/// The controller that resides over getting specific user information or all users.
	/// </summary>
	/// <seealso cref="Fleqx.Controllers.BaseController" />
	public class UserController : BaseController
	{
		/// <summary>
		/// Return all the users in a list.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public List<User> GetUsers()
		{
			using (var dbContext = GetDatabaseContext())
			{
				return dbContext.Users.ToList();
			}
		}

		/// <summary>
		/// Return all the users in a format for list dropdown controls.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<SelectListItem> GetUsersSelectList()
		{
			using (var dbContext = GetDatabaseContext())
			{
				List<User> userList = dbContext.Users.ToList();
				return userList.Select(user => new SelectListItem
				{
					Text  = user.UserName,
					Value = user.Id
				});
			}
		}
	}
}