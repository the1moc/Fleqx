using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;

namespace FleqxTests.Helpers
{
	public static class MockUserManager
	{
		/// <summary>
		/// Provide a mock user manager with a mock user inside it.
		/// </summary>
		/// <returns></returns>
		public static Mock<UserManager<User>> GetMockUserManager()
		{
			var mockUser      = new User() { UserName = "TestUserName" };
			var mockUserStore = new Mock<IUserStore<User>>();
			var userManager   = new Mock<UserManager<User>>(mockUserStore.Object);

			return userManager;
		}
	}
}
