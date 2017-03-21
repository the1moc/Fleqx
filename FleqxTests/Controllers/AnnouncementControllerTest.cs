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
            Mock<AnnouncementController> controller = GetMockedAnnouncementController();
            var result = controller.Object.Announcements() as ViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(2));
            Assert.That(((List<AnnouncementModel>)result.Model).First(), Has.Property("AnnouncementContent"));
        }

        /// <summary>
        /// Test the announcement controller returns specific announcements when filtering by importance.
        /// </summary>
        [Test]
        public void Announcement_ReturnsFilteredItems_OnImportance()
        {
            Mock<AnnouncementController> controller = GetMockedAnnouncementController();
            AnnouncementFilterModel filterModel = new AnnouncementFilterModel
            {
                AnnouncementImportance = 1,
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2050, 12, 31)
            };

            var result = controller.Object.FilterAnnouncements(filterModel) as ViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
            Assert.AreEqual(((List<AnnouncementModel>)result.Model).First().AnnouncementID, 3);
        }

        /// <summary>
        /// Test the announcement controller returns specific announcements when filtering by date.
        /// </summary>
        [Test]
        public void Announcement_ReturnsFilteredItems_OnDate()
        {
            Mock<AnnouncementController> controller = GetMockedAnnouncementController();
            AnnouncementFilterModel filterModel = new AnnouncementFilterModel
            {
                AnnouncementImportance = 3,
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(2)
            };

            var result = controller.Object.FilterAnnouncements(filterModel) as ViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
        }

        /// <summary>
        /// Test the announcement controller adds a new model correctly.
        /// </summary>
        [Test]
        public void CreateAnnouncement_ValidModel()
        {
            Mock<AnnouncementController> controller = GetMockedAnnouncementController();
            var result = controller.Object.CreateAnnouncement(new Mock<AnnouncementFormModel>().Object) as HttpStatusCodeResult;
            Assert.AreEqual(result.StatusCode, 200);
        }

        /// <summary>
        /// Test the announcement controller raises an error with an invalid model.
        /// </summary>
        [Test]
        public void CreateAnnouncement_InvalidModel()
        {
            Mock<AnnouncementController> controller = GetMockedAnnouncementController();
            controller.Object.ModelState.AddModelError("Error", "error");
            var result = controller.Object.CreateAnnouncement(new Mock<AnnouncementFormModel>().Object) as HttpStatusCodeResult;
            Assert.AreEqual(result.StatusCode, 500);
        }

        /// <summary>
        /// Gets the mocked security controller, with usermanager and request calls mocked.
        /// </summary>
        /// <returns></returns>
        public Mock<AnnouncementController> GetMockedAnnouncementController()
        {
            Mock<AnnouncementController> controller = new Mock<AnnouncementController>();
            controller.Setup(c => c.GetDatabaseContext()).Returns(MockDatabaseHelper.MockAnnouncementDatabaseCreator());
            controller.CallBase = true;

            return controller;
        }
    }
}
