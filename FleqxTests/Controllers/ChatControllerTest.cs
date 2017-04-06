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
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace FleqxTests.Controllers
{
    [TestFixture]
    class ChatControllerTest
    {
        /// <summary>
        /// Test the initial task controller call returns a blank chat view without any models.
        /// </summary>
        [Test]
        public void GetChat_NoModel()
        {
            Mock<ChatController> controller = GetMockedChatController();
            controller.CallBase = true;

            var result = controller.Object.Chat() as ViewResult;
            Assert.IsNull(result.Model);
        }

        /// <summary>
        /// Gets the mocked chat controller.
        /// </summary>
        /// <returns></returns>
        public Mock<ChatController> GetMockedChatController()
        {
            Mock<ChatController> controller = new Mock<ChatController>();
            controller.CallBase = true;
            controller.Setup(c => c.GetDatabaseContext()).Returns(MockDatabaseHelper.MockChatDatabaseContext());

            return controller;
        }
    }
}
