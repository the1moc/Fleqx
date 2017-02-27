using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Fleqx.Controllers;
using System.Web.Mvc;

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
			HomeController controller = new HomeController();
			var result                = controller.Home();
			Assert.IsInstanceOf(typeof(ViewResult), result);
			Assert.AreEqual("Home", ((ViewResult)result).ViewName);
		}
	}
}
