using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace Fleqx.Controllers
{
    // Controller for all activity logic
    public class ActivityController : BaseController
    {
        protected UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        public ActivityController()
            : this(new UserManager<User>(new UserStore<User>(new DatabaseContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public ActivityController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Saves the activity.
        /// </summary>
        /// <param name="activityType">Type of the activity.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Invalid activity type</exception>
        public ActionResult SaveActivity(string activityType)
        {
            using (var dbContext = GetDatabaseContext())
            {
                Activity activity;
                ActivityType parsedEnum = (ActivityType)Enum.Parse(typeof(ActivityType), activityType);
                switch (parsedEnum)
                {
                    case ActivityType.AnnouncementCreated:
                        activity = new Activity
                        {
                            ActivityContent = "Created an Annouuncement",
                            UserId = User.Identity.GetUserId()
                        };
                        break;
                    case ActivityType.AnnouncementEdited:
                        activity = new Activity
                        {
                            ActivityContent = "Edited an Annouuncement",
                            UserId = User.Identity.GetUserId()
                        };
                        break;
                    case ActivityType.AnnouncementDeleted:
                        activity = new Activity
                        {
                            ActivityContent = "Deleted an Annouuncement",
                            UserId = User.Identity.GetUserId()
                        };
                        break;
                    case ActivityType.TaskEdited:
                        activity = new Activity
                        {
                            ActivityContent = "Edited a Task",
                            UserId = User.Identity.GetUserId()
                        };
                        break;
                    case ActivityType.TaskCreated:
                        activity = new Activity
                        {
                            ActivityContent = "Created a Task",
                            UserId = User.Identity.GetUserId()
                        };
                        break;
                    case ActivityType.UserCreated:
                        activity = new Activity
                        {
                            ActivityContent = "Added a New User",
                            UserId = User.Identity.GetUserId()
                        };
                        break;
                    case ActivityType.UserEdited:
                        activity = new Activity
                        {
                            ActivityContent = "Edited a user",
                            UserId = User.Identity.GetUserId()
                        };
                        break;
                    default:
                        throw new Exception("Invalid activity type");
                }
                activity.Date = DateTime.Now;

                dbContext.Activity.Add(activity);
                dbContext.SaveChanges();
                return new HttpStatusCodeResult(200, "Activity logged");
            }
        }

        /// <summary>
        /// Gets the recent activity.
        /// </summary>
        /// <returns></returns>
        public string GetRecentActivity()
        {
            using (var dbContext = GetDatabaseContext())
            {
                List<Activity> activities = dbContext.Activity.OrderBy(a => a.ActivityId).Take(10).ToList();
                List<ActivityModel> viewModels = activities.Select(a => new ActivityModel
                {
                    ActivityContent = a.ActivityContent,
                    UserName = a.User.UserName,
                    Date = a.Date.ToString("yyyy-MM-dd H:mm:ss")
                }).ToList();

                return JsonConvert.SerializeObject(viewModels);
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