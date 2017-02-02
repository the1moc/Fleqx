using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fleqx.Controllers
{
	// Controller for all task logic
	public class TaskController : BaseController
	{
		/// <summary>
		/// Taskses the specified tasks.
		/// </summary>
		/// <param name="tasks">The tasks that have to be shown</param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Tasks(string tasksDesired)
		{
			using (DatabaseContext dbContext = new DatabaseContext())
			{
				List<Task> tasks;
				User currentUser = Startup.UserManager.FindById(User.Identity.GetUserId());
				switch (tasksDesired)
				{
					case "mytasks":
						tasks = dbContext.Tasks.Where(task => task.AssignedUserId == currentUser.Id).ToList();
						ViewBag.Title = "My Tasks";
						break;
					case "teamtasks":
						tasks = dbContext.Tasks.Where(task => task.AssignedUser.TeamId == currentUser.TeamId).ToList();
						ViewBag.Title = "Team Tasks";
						break;
					default:
						tasks = dbContext.Tasks.ToList();
						ViewBag.Title = "All Tasks";
						break;
				}

				// Create the view models to be passed to the task view
				List<TaskModel> models = tasks.Select(task =>
				{
					return new TaskModel
					{
						TaskID = task.TaskID,
						TaskTitle = task.TasktTitle,
						TaskDescription = task.TaskDescription,
						TaskPriority = task.TaskPriority,
						OriginalCreationDate = task.OriginalCreationDate,
						LastRenewalDate = task.LastRenewalDate,
						CriticalFinishDate = task.CriticalFinishDate,
						EstimatedDays = task.EstimatedDays,
						CreatedUser = task.CreatedUser,
						AssignedUser = task.AssignedUser,
						TaskState = task.TaskState
					};
				}).ToList();

				return PartialView(models);
			}
		}

		/// <summary>
		/// Gets the edit modal for the specified task.
		/// </summary>
		/// <param name="taskModel">The task model for the view.</param>
		/// <returns></returns>
		public ActionResult GetModalView(int taskId)
		{
			using (DatabaseContext dbContext = new DatabaseContext())
			{
				Task model = dbContext.Tasks.First(task => task.TaskID == taskId);
				TaskModel viewModel = new TaskModel
				{
					TaskID = model.TaskID,
					TaskTitle = model.TasktTitle,
					TaskDescription = model.TaskDescription,
					TaskPriority = model.TaskPriority,
					OriginalCreationDate = model.OriginalCreationDate,
					LastRenewalDate = model.LastRenewalDate,
					CriticalFinishDate = model.CriticalFinishDate,
					EstimatedDays = model.EstimatedDays,
					CreatedUser = model.CreatedUser,
					AssignedUser = model.AssignedUser,
					TaskState = model.TaskState
				};
				return PartialView("_TaskEditForm", viewModel);
			}
		}
	}
}