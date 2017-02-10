using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Fleqx.Controllers;
using System.Web.Mvc;
using Moq;
using Fleqx.Data;
using FleqxTests.Helpers;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;

namespace FleqxTests.Controllers
{
	[TestFixture]
	class AnnouncementControllerTest
	{
		/// <summary>
		/// Test the announcement controller returns the generic announcements view.
		/// </summary>
		[Test]
		public void Announcement_ReturnMainView()
		{
			AnnouncementController controller = new MockedAnnouncementController();
			var result = controller.Announcements() as ViewResult;
			Assert.That(result.Model, Has.Count.EqualTo(1));
			Assert.That(((List<AnnouncementModel>)result.Model).First(), Has.Property("AnnouncementContent"));
		}

		/// <summary>
		/// Test the announcement controller adds a new model correctly.
		/// </summary>
		[Test]
		public void CreateAnnouncement_ValidModel()
		{
			AnnouncementController controller = new MockedAnnouncementController();
			var result = controller.CreateAnnouncement(new Mock<AnnouncementFormModel>().Object) as HttpStatusCodeResult;
			Assert.AreEqual(result.StatusCode, 205);
		}

		/// <summary>
		/// Test the announcement controller raises an error with an invalid model.
		/// </summary>
		[Test]
		public void CreateAnnouncement_InvalidModel()
		{
			AnnouncementController controller = new MockedAnnouncementController();
			controller.ModelState.AddModelError("Error", "error");
			var result = controller.CreateAnnouncement(new Mock<AnnouncementFormModel>().Object) as HttpStatusCodeResult;
			Assert.AreEqual(result.StatusCode, 500);
		}
	}

	/// <summary>
	/// Returns a mocked EF database instance.
	/// </summary>
	/// <seealso cref="Fleqx.Controllers.AnnouncementController" />
	class MockedAnnouncementController : AnnouncementController
	{
		protected override DatabaseContext GetDatabaseContext()
		{
			return MockDatabaseHelper.MockAnnouncementDatabaseCreator();
		}
	}
}
