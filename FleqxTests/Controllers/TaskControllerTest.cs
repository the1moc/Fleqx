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
		/// Test the announcement controller returns a partial view with personal tasks in it.
		/// </summary>
		[Test]
		public void GetTasks_Mine()
		{
			Mock<MockedTaskController> controller = new Mock<MockedTaskController>();
			Mock<IIdentity> mockIdentity = new Mock<IIdentity>();
			controller.CallBase = true;

			controller.SetupGet(c => c.User.Identity).Returns(mockIdentity.Object);

			var result = controller.Object.Tasks("mytasks") as ViewResult;
			Assert.That(result.Model, Has.Count.EqualTo(4));
			Assert.That(((List<TaskModel>)result.Model).First(), Has.Property("My First Personal Task"));
		}

		/// <summary>
		/// Test the announcement controller returns a partial view with the team tasks in it.
		/// </summary>
		[Test]
		public void GetTasks_Team()
		{
			Mock<MockedTaskController> controller = new Mock<MockedTaskController>();
			Mock<IIdentity> mockIdentity = new Mock<IIdentity>();
			controller.CallBase = true;

			controller.SetupGet(c => c.User.Identity).Returns(mockIdentity.Object);

			var result = controller.Object.Tasks("teamtasks") as ViewResult;
			Assert.That(result.Model, Has.Count.EqualTo(2));
			Assert.That(((List<TaskModel>)result.Model).First(), Has.Property("A Team Task"));
		}

		/// <summary>
		/// Test the announcement controller returns a partial view with the team tasks in it.
		/// </summary>
		[Test]
		public void GetTasks_Team_InvalidDbContext()
		{
			Mock<MockedTaskController> controller = new Mock<MockedTaskController>();
			MockDatabaseHelper.SetDbContextInvdalid();
			controller.CallBase = true;
	
			Assert.Throws<Exception>(() => controller.Object.Tasks("teamtasks"));
		}

		/// <summary>
		/// Returns a mocked EF database instance.
		/// </summary>
		/// <seealso cref="Fleqx.Controllers.TaskController" />
		class MockedTaskController : TaskController
		{
			public override DatabaseContext GetDatabaseContext()
			{
				return MockDatabaseHelper.MockTaskDatabaseCreator();
			}
		}
	}
}
