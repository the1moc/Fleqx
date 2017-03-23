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
        /// Return the desires tasks partial view with the default one week range filter only.
        /// </summary>
        /// <param name="desiredTasks">The type of tasks the user wants to get.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Tasks(string desiredTasks)
        {
            using (var dbContext = GetDatabaseContext())
            {
                TaskFilterModel filterModel = new TaskFilterModel
                {
                    EndDate = DateTime.Now,
                    StartDate = DateTime.Now.AddDays(-7),
                    TasksDesired = desiredTasks,
                    AllUsers = dbContext.Users.ToList(),
                    CreatedUserId = User.Identity.GetUserId()
                };

                List<Task> tasks;

                User currentUser = Startup.UserManager.FindById(User.Identity.GetUserId());
                switch (desiredTasks)
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
                // Apply the dates filter.
                tasks = tasks.Where(task => task.OriginalCreationDate >= filterModel.StartDate).ToList();

                // Create the view models to be passed to the task view
                List<TaskModel> models = tasks.Select(task =>
                {
                    return new TaskModel
                    {
                        TaskID = task.TaskID,
                        TaskTitle = task.TaskTitle,
                        TaskDescription = task.TaskDescription,
                        TaskPriority = task.TaskPriority,
                        OriginalCreationDate = task.OriginalCreationDate,
                        LastRenewalDate = task.LastRenewalDate,
                        CriticalFinishDate = task.CriticalFinishDate,
                        ActualFinishDate = task.ActualFinishDate,
                        EstimatedDaysTaken = task.EstimatedDaysTaken,
                        ActualDaysTaken = task.ActualDaysTaken,
                        CreatedUser = task.CreatedUser,
                        AssignedUser = task.AssignedUser,
                        TaskState = task.TaskState,
                        TaskStateId = task.TaskStateId,
                        AllUsers = null
                    };
                }).ToList();

                ViewBag.filterModel = filterModel;
                return PartialView(models);
            }
        }

        /// <summary>
        /// Return the desired tasks view, filtered with the filter model for all desires fields.
        /// </summary>
        /// <param name="tasksDesired">The tasks that have to be shown</param>
        /// <param name="filterModel">The model containing the filter parameters</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult FilterTasks(TaskFilterModel filterModel)
        {
            using (var dbContext = GetDatabaseContext())
            {
                List<Task> tasks;
                User currentUser = Startup.UserManager.FindById(User.Identity.GetUserId());
                switch (filterModel.TasksDesired)
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

                // Apply the filter.
                tasks = tasks.Where(task => task.OriginalCreationDate >= filterModel.StartDate &&
                                            task.OriginalCreationDate <= filterModel.EndDate).ToList();

                // Create the view models to be passed to the task view
                List<TaskModel> models = tasks.Select(task =>
                {
                    return new TaskModel
                    {
                        TaskID = task.TaskID,
                        TaskTitle = task.TaskTitle,
                        TaskDescription = task.TaskDescription,
                        TaskPriority = task.TaskPriority,
                        OriginalCreationDate = task.OriginalCreationDate,
                        LastRenewalDate = task.LastRenewalDate,
                        CriticalFinishDate = task.CriticalFinishDate,
                        ActualFinishDate = task.ActualFinishDate,
                        EstimatedDaysTaken = task.EstimatedDaysTaken,
                        ActualDaysTaken = task.ActualDaysTaken,
                        CreatedUser = task.CreatedUser,
                        AssignedUser = task.AssignedUser,
                        TaskState = task.TaskState,
                        TaskStateId = task.TaskStateId,
                        AllUsers = null
                    };
                }).ToList();

                ViewBag.filterModel = filterModel;
                return PartialView("Tasks", models);
            }
        }

        /// <summary>
        /// Get the filter form view with a populated model.
        /// </summary>
        /// <returns>The view that contains the partial view model.</returns>
        public ActionResult TaskFilterForm()
        {
            TaskFilterModel filterModel = new TaskFilterModel
            {
                StartDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now,
                TaskPriority = 6
            };

            return PartialView("_TaskFilterForm", filterModel);
        }

        /// <summary>
        /// Gets the edit modal for the specified task.
        /// </summary>
        /// <param name="taskModel">The task model for the view.</param>
        /// <returns></returns>
        public ActionResult GetEditModalView(int taskId)
        {
            using (var dbContext = GetDatabaseContext())
            {
                Task model = dbContext.Tasks.First(task => task.TaskID == taskId);
                TaskModel viewModel = new TaskModel
                {
                    TaskID = model.TaskID,
                    TaskTitle = model.TaskTitle,
                    TaskDescription = model.TaskDescription,
                    TaskPriority = model.TaskPriority,
                    OriginalCreationDate = model.OriginalCreationDate,
                    LastRenewalDate = model.LastRenewalDate,
                    CriticalFinishDate = model.CriticalFinishDate,
                    ActualFinishDate = model.ActualFinishDate,
                    EstimatedDaysTaken = model.EstimatedDaysTaken,
                    ActualDaysTaken = model.ActualDaysTaken,
                    CreatedUser = model.CreatedUser,
                    AssignedUser = model.AssignedUser,
                    AssignedUserId = model.AssignedUserId,
                    TaskState = model.TaskState,
                    TaskStateId = model.TaskStateId,
                    AllUsers = dbContext.Users.ToList()
                };
                return PartialView("_TaskEditForm", viewModel);
            }
        }

        /// <summary>
        /// Edit the model.
        /// </summary>
        /// <param name="task">The task model for the view.</param>
        /// <returns></returns>
        public ActionResult Edit(TaskModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dbContext = GetDatabaseContext())
                    {
                        Task dbModel = dbContext.Tasks.Find(viewModel.TaskID);

                        dbModel.TaskID = viewModel.TaskID;
                        dbModel.TaskTitle = viewModel.TaskTitle;
                        dbModel.TaskDescription = viewModel.TaskDescription;
                        dbModel.TaskPriority = viewModel.TaskPriority;
                        dbModel.LastRenewalDate = viewModel.LastRenewalDate;
                        dbModel.CriticalFinishDate = viewModel.CriticalFinishDate;
                        dbModel.ActualFinishDate = viewModel.ActualFinishDate;
                        dbModel.EstimatedDaysTaken = viewModel.EstimatedDaysTaken;
                        dbModel.ActualDaysTaken = viewModel.ActualDaysTaken;
                        dbModel.AssignedUserId = viewModel.AssignedUserId;
                        dbModel.TaskStateId = viewModel.TaskStateId;

                        // Update the changes.
                        dbContext.Entry(dbModel).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();

                        return new HttpStatusCodeResult(200);
                    }
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(500, "There was an error updating the task: " + e.Message);
                }
            }
            return new HttpStatusCodeResult(500, "The form was not filled out correctly");
        }

        /// <summary>
        /// Gets the add modal for the specified task.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddModalView()
        {
            using (var dbContext = GetDatabaseContext())
            {
                TaskModel viewModel = new TaskModel
                {
                    // Set some values to be passed to the new add form.
                    AllUsers = dbContext.Users.ToList(),
                    CriticalFinishDate = DateTime.Now,
                    ActualFinishDate = new DateTime(2050, 1, 1)
                };

                return PartialView("_TaskAddForm", viewModel);
            }
        }

        /// <summary>
        /// Add a task.
        /// </summary>
        /// <param name="task">The task model for the view.</param>
        /// <returns></returns>
        public ActionResult Add(TaskModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dbContext = GetDatabaseContext())
                    {
                        Task dbModel = new Task();

                        dbModel.TaskTitle = viewModel.TaskTitle;
                        dbModel.TaskDescription = viewModel.TaskDescription;
                        dbModel.TaskPriority = viewModel.TaskPriority;
                        dbModel.OriginalCreationDate = DateTime.Now;
                        dbModel.LastRenewalDate = DateTime.Now;
                        dbModel.CriticalFinishDate = viewModel.CriticalFinishDate;
                        dbModel.ActualFinishDate = new DateTime(2050, 1, 1);
                        dbModel.EstimatedDaysTaken = viewModel.EstimatedDaysTaken;
                        dbModel.CreatedUserId = viewModel.CreatedUserId;
                        dbModel.AssignedUserId = viewModel.AssignedUserId;
                        dbModel.TaskStateId = viewModel.TaskStateId;

                        // Update the changes.
                        dbContext.Tasks.Add(dbModel);
                        dbContext.SaveChanges();

                        return new HttpStatusCodeResult(200);
                    }
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(500, "There was an error adding the task:" + e.Message);
                }
            }
            return new HttpStatusCodeResult(500, "The form was not filled out correctly");
        }
    }
}