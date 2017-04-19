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
    public class CalendarController : BaseController
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarController"/> class.
        /// </summary>
        public CalendarController()
            : this(new UserManager<User>(new UserStore<User>(new DatabaseContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public CalendarController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get the calendar view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Calendar()
        {
            ViewBag.Title = "Calendar";
            return View();
        }

        /// <summary>
        /// Get the calendar tasks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetCalendarTasks()
        {
            CalendarModel calendarModel = new CalendarModel();

            using (var dbContext = GetDatabaseContext())
            {
                string currentUserId = GetCurrentUser().Id;
                List<Task> tasks = dbContext.Tasks.Where(task => task.AssignedUserId == currentUserId && task.TaskStateId == 2).ToList();

                List<CalendarModel> calendarModels = tasks.Select(task => { return new CalendarModel { Title = task.TaskTitle, Start = task.TaskStartedDate.ToString("yyyy-MM-dd") }; }).ToList();

                return JsonConvert.SerializeObject(calendarModels);
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