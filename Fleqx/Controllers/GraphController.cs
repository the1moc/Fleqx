using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fleqx.Controllers
{
    public class GraphController : BaseController
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public GraphController()
            : this(new UserManager<User>(new UserStore<User>(new DatabaseContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public GraphController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get the graph view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Graph()
        {
            return View();
        }

        /// <summary>
        /// Gets the state of the personal task.
        /// </summary>
        /// <returns></returns>
        public string GetTaskChart(string tasksDesired)
        {
            ChartModel chartModel = CreateTaskBarChart(tasksDesired);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(chartModel, camelCaseFormatter);
            
        }

        /// <summary>
        /// Gets the graph for all the logged in users.
        /// </summary>
        /// <returns></returns>
        public string GetUserChart()
        {
            ChartModel chartModel = CreateLoggedInUserChart();

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(chartModel, camelCaseFormatter);
        }

        /// <summary>
        /// Gets the state of the personal task.
        /// </summary>
        /// <returns></returns>
        public string GetDaysChart()
        {
            ChartModel chartModel = CreateEstimatedDaysChart();

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(chartModel, camelCaseFormatter);
        }

        /// <summary>
        /// Creates the estimated days bar chart.
        /// </summary>
        /// <returns></returns>
        public ChartModel CreateEstimatedDaysChart()
        {
            using (var dbContext = GetDatabaseContext())
            {
                ChartModel chartModel = new ChartModel
                {
                    Type = "bar"
                };

                ChartDataModel chartDataModel = new ChartDataModel
                {
                    Labels = dbContext.Users.Select(user => user.UserName).ToList()
                };

                ChartDatasetModel chartDatasetModel = new ChartDatasetModel
                {
                    Data = new int[dbContext.Users.Count()],
                    BackgroundColor = new string[] { "#FF69B4" },
                    BorderColor = new string[] { "#FFFFFF" },
                    BorderWidth = 1
                };

                IEnumerable<User> users = dbContext.Users.ToList();
                int index = 0;

                foreach (User user in users)
                {
                    IEnumerable<Task> tasksWithinTimePeriod = user.AssignedTasks.Where(task => task.OriginalCreationDate >= DateTime.Now.AddDays(-14));

                    int totalEstimatedDays = tasksWithinTimePeriod != null ? tasksWithinTimePeriod.Select(task => task.EstimatedDaysTaken).Sum() : 0;
                    chartDatasetModel.Data[index] = totalEstimatedDays;
                    index++;
                }

                chartDataModel.Datasets = new List<ChartDatasetModel> { chartDatasetModel };
                chartModel.Data = chartDataModel;
                chartModel.Title = "Estimated Days for each User - Assigned Tasks";
                return chartModel;
            }
        }

        /// <summary>
        /// Creates the logged in user doughnut chart.
        /// </summary>
        /// <returns></returns>
        public ChartModel CreateLoggedInUserChart()
        {
            ChartModel chartModel = new ChartModel
            {
                Type = "doughnut"
            };

            ChartDataModel chartDataModel = new ChartDataModel
            {
                Labels = new List<string> { "Online", "Offline" }
            };

            ChartDatasetModel chartDatasetModel = new ChartDatasetModel
            {
                Data = new int[2],
                BackgroundColor = new string[] { "#66FF66", "#FF6666" },
                BorderColor = new string[] { "#ffffff", "#ffffff" },
                BorderWidth = 1
            };

            using (var dbContext = GetDatabaseContext())
            {
                int userCount = dbContext.Users.Count();

                chartDatasetModel.Data[0] = dbContext.Users.Where(user => user.IsLoggedIn == 1).Count();
                chartDatasetModel.Data[1] = dbContext.Users.Where(user => user.IsLoggedIn == 0).Count();
            }

            chartDataModel.Datasets = new List<ChartDatasetModel> { chartDatasetModel };
            chartModel.Data = chartDataModel;
            chartModel.Title = "Currently Logged in Users";
            return chartModel;
        }

        /// <summary>
        /// Creates the task bar chart.
        /// </summary>
        /// <param name="tasksDesired">The tasks desired.</param>
        /// <returns></returns>
        public ChartModel CreateTaskBarChart(string tasksDesired)
        {
            using (var dbContext = GetDatabaseContext())
            {
                // Initial blank filter model.
                TaskFilterModel filterModel = new TaskFilterModel
                {
                    OriginalCreationDateFrom = DateTime.Now.AddDays(-14),
                    OriginalCreationDateTo = DateTime.Now,
                    ActualFinishDateFrom = DateTime.Now.AddDays(-14),
                    ActualFinishDateTo = DateTime.Now,
                    StartedDate = DateTime.Now.AddDays(-14),
                    TasksDesired = tasksDesired,
                    AllUsers = dbContext.Users.ToList(),
                    CreatedUserId = "",
                    TaskPriority = -1
                };

                ChartModel chartModel = new ChartModel
                {
                    Type = "bar"
                };

                ChartDataModel chartDataModel = new ChartDataModel
                {
                    Labels = new List<string> { "Open", "Active", "Closed" }
                };

                ChartDatasetModel chartDatasetModel = new ChartDatasetModel
                {
                    Data = new int[3],
                    BackgroundColor = new string[] { "#FF6666", "#66FF66", "#C0C0C0" },
                    BorderColor = new string[] { "#FFFFFF" }
                };

                User currentUser = GetCurrentUser();
                List<Task> tasks;

                switch (tasksDesired)
                {
                    case "mytasks":
                        tasks = dbContext.Tasks.Where(task => task.AssignedUserId == currentUser.Id).ToList();
                        chartModel.Title = "Task State - My Tasks - Past Two Weeks";
                        break;
                    case "teamtasks":
                        tasks = dbContext.Tasks.Where(task => task.AssignedUser.TeamId == currentUser.TeamId).ToList();
                        chartModel.Title = "Task State - Team Tasks - Past Two Weeks";
                        break;
                    default:
                        tasks = dbContext.Tasks.ToList();
                        chartModel.Title = "Task State - All Tasks - Past Two Weeks";
                        break;
                }

                // Apply the dates filter.
                tasks = tasks.Where(task => task.OriginalCreationDate >= filterModel.OriginalCreationDateFrom
                                         && task.OriginalCreationDate <= filterModel.OriginalCreationDateTo
                                         || task.TaskStartedDate >= filterModel.StartedDate
                                         || task.ActualFinishDate >= filterModel.ActualFinishDateFrom
                                         && task.ActualFinishDate <= filterModel.ActualFinishDateTo).ToList();

                foreach (Task task in tasks)
                {
                    switch (task.TaskState.TaskStateCurrent)
                    {
                        case "Open":
                            chartDatasetModel.Data[0]++;
                            break;
                        case "Active":
                            chartDatasetModel.Data[1]++;
                            break;
                        case "Closed":
                            chartDatasetModel.Data[2]++;
                            break;
                        default:
                            break;
                    }
                }

                chartDataModel.Datasets = new List<ChartDatasetModel> { chartDatasetModel };
                chartModel.Data = chartDataModel;

                return chartModel;
            }
        }

        /// <summary>
        /// Gets the currently logged in user.
        /// </summary>
        /// <returns>The user ID</returns>
        public virtual User GetCurrentUser()
        {
            return userManager.FindById(User.Identity.GetUserId());
        }
    }
}