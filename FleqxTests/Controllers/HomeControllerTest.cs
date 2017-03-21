using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Fleqx.Controllers;
using System.Web.Mvc;
using Moq;

namespace FleqxTests.Controllers
{
	[TestFixture]
	class HomeControllerTest
	{
		/// <summary>
		/// Test the home controller is returning a home view.
		/// </summary>
		[Test]
		public void TestHomeView()
		{
			Mock<HomeController> controller = new Mock<HomeController>();
			controller.CallBase = true;

			var result                = controller.Object.Home();
			Assert.IsInstanceOf(typeof(ViewResult), result);
			Assert.AreEqual("Home", ((ViewResult)result).ViewName);
		}
	}
}
