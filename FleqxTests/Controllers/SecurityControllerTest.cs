using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Fleqx.Controllers;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;
using FleqxTests.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;

namespace FleqxTests.Controllers
{
	[TestFixture]
	class SecurityControllerTest
	{
		/// <summary>
		/// Test the security controller returns the login view.
		/// </summary>
		[Test]
		public void Shows_LoginView()
		{
			Mock<SecurityController> controller = GetMockedSecurityController();

			var result                = controller.Object.Login("/Mock/Return/Url");
			Assert.IsInstanceOf(typeof(ViewResult), result);
			Assert.AreEqual("LoginPage", ((ViewResult)result).ViewName);
		}

		/// <summary>
		/// Test the login functionality returns the login page with an invalid model given.
		/// </summary>
		[Test]
		public void Shows_LoginView_OnInvalidForm()
		{
			Mock<SecurityController> controller = GetMockedSecurityController();
			controller.Object.ModelState.AddModelError("Error", "error");

			var result = controller.Object.Login(new LoginModel());
			Assert.IsInstanceOf(typeof(ViewResult), result.Result);
			Assert.AreEqual("LoginPage", ((ViewResult)result.Result).ViewName);
		}

		/// <summary>
		/// Test the login functionality that displays a message if the user is not found.
		/// </summary>
		[Test]
		public void Shows_HomeViewAndError_OnUserLoggedIn()
		{
			Mock<SecurityController> controller = GetMockedSecurityController();

			Task<ActionResult> result = controller.Object.Login(new LoginModel() { UserName = "TestUserName", Password = "password" });
			result.Wait();

			Assert.IsInstanceOf(typeof(ViewResult), result.Result);
			Assert.AreEqual("LoginPage", ((ViewResult)result.Result).ViewName);
			Assert.AreEqual("Invalid credentials. Try again.", controller.Object.ViewBag.Error);
		}

		/// <summary>
		/// Gets the mocked security controller, with usermanager and request calls mocked.
		/// </summary>
		/// <returns></returns>
		public Mock<SecurityController> GetMockedSecurityController()
		{
			Mock<UserManager<User>> userManager = MockUserManager.GetMockUserManager();

			Mock<SecurityController> controller = new Mock<SecurityController>(userManager.Object);
			controller.CallBase = true;

			return controller;
		}
	}
}
