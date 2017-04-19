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
using Newtonsoft.Json;

namespace FleqxTests.Controllers
{
    [TestFixture]
    class ChartControllerTest
    {
        /// <summary>
        /// Test the initial graph controller call returns the graph view.
        /// </summary>
        [Test]
        public void GetChart_View()
        {
            Mock<ChartController> controller = GetMockedChartController();
            controller.CallBase = true;

            var result = controller.Object.Chart();
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        /// <summary>
        /// Test the correct tasks are retrieved in a chart model.
        /// </summary>
        /// <returns></returns>
        [Test]
        public void Get_Chart_MyTasks()
        {
            Mock<ChartController> controller = GetMockedChartController();
            controller.CallBase = true;

            string result = controller.Object.GetTaskChart("mytasks");
            ChartModel parsedChartModel = JsonConvert.DeserializeObject<ChartModel>(result);
            Assert.AreEqual(1, parsedChartModel.Data.Datasets.Count);
            Assert.AreEqual(3, parsedChartModel.Data.Datasets[0].Data[0]);
        }

        /// <summary>
        /// Test the user completed days are retrieved in a chart model.
        /// </summary>
        /// <returns></returns>
        [Test]
        public void Get_Chart_CompletedDays()
        {
            Mock<ChartController> controller = GetMockedChartController();
            controller.CallBase = true;

            string result = controller.Object.GetCompletedDays();
            ChartModel parsedChartModel = JsonConvert.DeserializeObject<ChartModel>(result);
            Assert.AreEqual("bar", parsedChartModel.Type);
            Assert.AreEqual("User Completed Days - Past Two Weeks", parsedChartModel.Title);
            Assert.AreEqual(1, parsedChartModel.Data.Datasets.Count);
            Assert.AreEqual(19, parsedChartModel.Data.Datasets[0].Data[0]);
        }

        /// <summary>
        /// Test the created tasks are retrieved in a chart model.
        /// </summary>
        /// <returns></returns>
        [Test]
        public void Get_Chart_CreatedTasks()
        {
            Mock<ChartController> controller = GetMockedChartController();
            controller.CallBase = true;

            string result = controller.Object.GetCreatedTasks();
            ChartModel parsedChartModel = JsonConvert.DeserializeObject<ChartModel>(result);
            Assert.AreEqual("line", parsedChartModel.Type);
            Assert.AreEqual("Tasks Created - Past Week", parsedChartModel.Title);
            Assert.AreEqual(2, parsedChartModel.Data.Datasets.Count);
            Assert.AreEqual(4, parsedChartModel.Data.Datasets[0].Data[6]);
            Assert.AreEqual(4, parsedChartModel.Data.Datasets[1].Data[6]);
        }

        /// <summary>
        /// Test the estimated days are retrieved in a chart model.
        /// </summary>
        /// <returns></returns>
        [Test]
        public void Get_Chart_EstimatedDays()
        {
            Mock<ChartController> controller = GetMockedChartController();
            controller.CallBase = true;

            string result = controller.Object.GetDaysChart();
            ChartModel parsedChartModel = JsonConvert.DeserializeObject<ChartModel>(result);
            Assert.AreEqual("bar", parsedChartModel.Type);
            Assert.AreEqual("Estimated Days for each User - Active Tasks", parsedChartModel.Title);
            Assert.AreEqual(1, parsedChartModel.Data.Datasets.Count);
            Assert.AreEqual(2, parsedChartModel.Data.Labels.Count);
            Assert.AreEqual(20, parsedChartModel.Data.Datasets[0].Data[0]);
        }

        /// <summary>
        /// Gets the mocked chart controller.
        /// </summary>
        /// <returns></returns>
        public Mock<ChartController> GetMockedChartController()
        {
            Mock<ChartController> controller = new Mock<ChartController>(MockUserManager.GetMockUserManager().Object);
            controller.CallBase = true;
            controller.Setup(c => c.GetCurrentUser()).Returns(new User() { UserName = "TestUser", Id = "1" });
            controller.Setup(c => c.GetDatabaseContext()).Returns(MockDatabaseHelper.MockTaskDatabaseCreator());

            return controller;
        }
    }
}
