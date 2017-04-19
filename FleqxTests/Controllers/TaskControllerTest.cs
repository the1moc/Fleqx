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
    class TaskControllerTest
    {
        /// <summary>
        /// Test the initial task controller call returns appropriate tasks.
        /// </summary>
        [Test]
        public void GetTasks_Mine()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            var result = controller.Object.Tasks("mytasks") as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(3));
            Assert.AreEqual(((List<TaskModel>)result.Model).First().TaskTitle, "Test Title");
        }

        /// <summary>
        /// Test the initial task controller call returns all tasks, filtered within the last week.
        /// </summary>
        [Test]
        public void GetTasks_All()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            var result = controller.Object.Tasks("alltasks") as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(4));
            Assert.AreEqual(((List<TaskModel>)result.Model).Last().TaskTitle, "Test Title Different User");
        }

        /// <summary>
        /// Test the initial task controller call returns all tasks, filtered within the last week.
        /// </summary>
        [Test]
        public void GetTasks_InvalidModel()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            var result = controller.Object.Tasks("alltasks") as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(4));
            Assert.AreEqual(((List<TaskModel>)result.Model).Last().TaskTitle, "Test Title Different User");
        }

        /// <summary>
        /// Test the task filter with no extra filter model options given.
        /// </summary>
        [Test]
        public void GetTasks_Filter_BlankModel()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            TaskFilterModel taskFilterModel = new TaskFilterModel
            {
                OriginalCreationDateFrom = DateTime.Now.AddDays(-1),
                OriginalCreationDateTo = DateTime.Now.AddDays(6),
                TasksDesired = "alltasks",
                TaskPriority = -1
            };

            var result = controller.Object.FilterTasks(taskFilterModel) as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(3));
            Assert.IsTrue(((List<TaskModel>)result.Model).First().OriginalCreationDate >= taskFilterModel.OriginalCreationDateFrom);
            Assert.IsTrue(((List<TaskModel>)result.Model).First().OriginalCreationDate <= taskFilterModel.OriginalCreationDateTo);
        }

        /// <summary>
        /// Test the task filter with no a custom priority.
        /// </summary>
        [Test]
        public void GetTasks_Filter_TaskPriority()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            TaskFilterModel taskFilterModel = new TaskFilterModel
            {
                OriginalCreationDateFrom = DateTime.Now.AddDays(-1),
                OriginalCreationDateTo = DateTime.Now.AddDays(6),
                TasksDesired = "alltasks",
                TaskPriority = 2
            };

            var result = controller.Object.FilterTasks(taskFilterModel) as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
            Assert.IsTrue(((List<TaskModel>)result.Model).First().TaskPriority == 2);
        }

        /// <summary>
        /// Test the task filter with a custom title.
        /// </summary>
        [Test]
        public void GetTasks_Filter_TaskTitle()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            TaskFilterModel taskFilterModel = new TaskFilterModel
            {
                OriginalCreationDateFrom = DateTime.Now.AddDays(-1),
                OriginalCreationDateTo = DateTime.Now.AddDays(6),
                TasksDesired = "alltasks",
                TaskPriority = -1,
                TaskTitle = "Custom Title"
            };

            var result = controller.Object.FilterTasks(taskFilterModel) as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
            Assert.IsTrue(((List<TaskModel>)result.Model).First().TaskTitle == "Custom Title");
        }

        /// <summary>
        /// Test the task filter with a custom created by field.
        /// </summary>
        [Test]
        public void GetTasks_Filter_CreatedBy()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            TaskFilterModel taskFilterModel = new TaskFilterModel
            {
                OriginalCreationDateFrom = DateTime.Now.AddDays(-1),
                OriginalCreationDateTo = DateTime.Now.AddDays(6),
                TasksDesired = "alltasks",
                TaskPriority = -1,
                CreatedUserId = "2"
            };

            var result = controller.Object.FilterTasks(taskFilterModel) as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
            Assert.IsTrue(((List<TaskModel>)result.Model).First().CreatedUser.Id == "2");
        }

        /// <summary>
        /// Test the task filter with a custom estimated days.
        /// </summary>
        [Test]
        public void GetTasks_Filter_EstimatedDaysTaken()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            TaskFilterModel taskFilterModel = new TaskFilterModel
            {
                OriginalCreationDateFrom = DateTime.Now.AddDays(-1),
                OriginalCreationDateTo = DateTime.Now.AddDays(6),
                TasksDesired = "alltasks",
                TaskPriority = -1,
                EstimatedDaysTaken = 3
            };

            var result = controller.Object.FilterTasks(taskFilterModel) as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
            Assert.IsTrue(((List<TaskModel>)result.Model).First().EstimatedDaysTaken == 3);
        }

        /// <summary>
        /// Test the task filter with a custom actual days.
        /// </summary>
        [Test]
        public void GetTasks_Filter_ActualDaysTaken()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            TaskFilterModel taskFilterModel = new TaskFilterModel
            {
                OriginalCreationDateFrom = DateTime.Now.AddDays(-1),
                OriginalCreationDateTo = DateTime.Now.AddDays(6),
                TasksDesired = "alltasks",
                TaskPriority = -1,
                ActualDaysTaken = 7
            };

            var result = controller.Object.FilterTasks(taskFilterModel) as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
            Assert.IsTrue(((List<TaskModel>)result.Model).First().ActualDaysTaken == 7);
        }

        /// <summary>
        /// Test the task filter with a custom assigned user.
        /// </summary>
        [Test]
        public void GetTasks_Filter_AssignedUser()
        {
            Mock<TaskController> controller = GetMockedTaskController();
            controller.CallBase = true;

            TaskFilterModel taskFilterModel = new TaskFilterModel
            {
                OriginalCreationDateFrom = DateTime.Now.AddDays(-1),
                OriginalCreationDateTo = DateTime.Now.AddDays(6),
                TasksDesired = "alltasks",
                TaskPriority = -1,
                AssignedUserId = "2"
            };

            var result = controller.Object.FilterTasks(taskFilterModel) as PartialViewResult;
            Assert.That(result.Model, Has.Count.EqualTo(1));
            Assert.IsTrue(((List<TaskModel>)result.Model).First().AssignedUser.Id == "2");
        }

        /// <summary>
        /// Gets the mocked task controller.
        /// </summary>
        /// <returns></returns>
        public Mock<TaskController> GetMockedTaskController()
        {
            Mock<TaskController> controller = new Mock<TaskController>(MockUserManager.GetMockUserManager().Object);
            controller.CallBase = true;
            controller.Setup(c => c.GetCurrentUser()).Returns(new User() { UserName = "TestUser", Id = "1" });
            controller.Setup(c => c.GetDatabaseContext()).Returns(MockDatabaseHelper.MockTaskDatabaseCreator());

            return controller;
        }
    }
}
