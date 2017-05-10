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
    /// <summary>
    /// Controller that controls all interactions with announcements.
    /// </summary>
    /// <seealso cref="Fleqx.Controllers.BaseController" />
    [Authorize]
    public class AnnouncementController : BaseController
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
        /// </summary>
        public AnnouncementController()
            : this(new UserManager<User>(new UserStore<User>(new DatabaseContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public AnnouncementController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get the announcements view with announcements from the past week.
        /// </summary>
        /// <returns>The announcements view.</returns>
        [HttpGet]
        public ActionResult Announcements()
        {
            using (var dbContext = GetDatabaseContext())
            {
                DateTime lastWeek = DateTime.Now.AddDays(-7);
                List<Announcement> announcements = dbContext.Announcements.Where(annoucement => annoucement.Created >= lastWeek).ToList();

                // Create the view models to be passed to the announcements view
                List<AnnouncementModel> models =
                    announcements.OrderByDescending(announcement => announcement.Created).Select(announcement =>
                    {
                        return new AnnouncementModel
                        {
                            AnnouncementID = announcement.AnnouncementID,
                            AnnouncementContent = announcement.AnnouncementContent,
                            AnnouncementImportance = announcement.AnnouncementImportance,
                            AnnouncementTitle = announcement.AnnouncementTitle,
                            Created = announcement.Created,
                            User = announcement.User,
                            CanEdit = (announcement.UserId == GetCurrentUser().Id || userManager.GetRoles(GetCurrentUser().Id).First() == "Admin")
                    };
                    }).ToList();

                ViewBag.Title = "Announcements";
                return View(models);
            }
        }

        /// <summary>
        /// Get the announcements view with announcements according to the filter.
        /// </summary>
        /// <returns>The announcements view.</returns>
        [HttpPost]
        public ActionResult FilterAnnouncements(AnnouncementFilterModel filterModel)
        {
            DateTime trueEndDate = filterModel.EndDate.AddDays(1);
            using (var dbContext = GetDatabaseContext())
            {
                IEnumerable<Announcement> announcements = dbContext.Announcements.Where(announcement =>
                    announcement.Created >= filterModel.StartDate
                    && announcement.Created < trueEndDate);

                if(filterModel.AnnouncementImportance != 6)
                {
                    announcements = announcements.Where(announcement => (announcement.AnnouncementImportance == filterModel.AnnouncementImportance));
                }

                // Create the view models to be passed to the announcements view
                List<AnnouncementModel> models =
                    announcements.OrderByDescending(announcement => announcement.Created).Select(announcement =>
                    {
                        return new AnnouncementModel
                        {
                            AnnouncementID = announcement.AnnouncementID,
                            AnnouncementContent = announcement.AnnouncementContent,
                            AnnouncementImportance = announcement.AnnouncementImportance,
                            AnnouncementTitle = announcement.AnnouncementTitle,
                            Created = announcement.Created,
                            User = announcement.User
                        };
                    }).ToList();
                ViewBag.FilterModel = filterModel;
                return View("Announcements", models);
            }
        }

        /// <summary>
        /// Get the filter form view with a populated model/
        /// </summary>
        /// <returns>The view that contaisn the partial view model.;;</returns>
        public ActionResult AnnouncementFilterForm()
        {
            AnnouncementFilterModel filterModel = new AnnouncementFilterModel
            {
                StartDate = DateTime.Now.AddDays(-7),
                EndDate = DateTime.Now,
                AnnouncementImportance = 6
            };

            return PartialView("_AnnouncementFilterForm", filterModel);
        }

        /// <summary>
        /// Gets the edit modal for the specified announcement.
        /// </summary>
        /// <param name="announcementModel">The announcement model for the view..</param>
        /// <returns></returns>
        public ActionResult GetEditModalView(string announcementId)
        {
            using (var dbContext = GetDatabaseContext())
            {
                Announcement announcement = dbContext.Announcements.First(a => a.AnnouncementID.ToString() == announcementId);
                AnnouncementModel model = new AnnouncementModel
                {
                    AnnouncementID = announcement.AnnouncementID,
                    AnnouncementContent = announcement.AnnouncementContent,
                    AnnouncementImportance = announcement.AnnouncementImportance,
                    AnnouncementTitle = announcement.AnnouncementTitle,
                    Created = announcement.Created,
                    User = announcement.User
                };
                return PartialView("_AnnouncementEditForm", model);
            }
        }

        /// <summary>
        /// Gets the delete modal for announcements.
        /// </summary>
        /// <param name="announcementModel">The announcement model for the view..</param>
        /// <returns></returns>
        public ActionResult GetDeleteModalView(string announcementId)
        {
            ViewBag.AnnouncementId = announcementId;
            return PartialView("_AnnouncementDeleteForm");
        }

        /// <summary>
        /// Gets the add modal for a new announcement.
        /// </summary>
        /// <param name="announcementModel">The announcement model for the view..</param>
        /// <returns></returns>
        public ActionResult GetAddModalView()
        {
            AnnouncementFormModel model = new AnnouncementFormModel();
            return PartialView("_AnnouncementForm", model);
        }

        /// <summary>
        /// Add a new announcement to the database.
        /// </summary>
        /// <param name="model">The announcement model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAnnouncement(AnnouncementFormModel model)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext dbContext = GetDatabaseContext())
                {
                    dbContext.Announcements.Add(new Announcement
                    {
                        AnnouncementContent = model.AnnouncementContent,
                        AnnouncementImportance = model.AnnouncementImportance,
                        AnnouncementTitle = model.AnnouncementTitle,
                        Created = DateTime.Now,
                        UserId = model.UserId
                    });

                    dbContext.SaveChanges();
                }
                return new HttpStatusCodeResult(200);
            }

            ViewBag.Error = "Please correctly fill out the form.";
            return new HttpStatusCodeResult(500);
        }

        /// <summary>
        /// Edit the announcement.
        /// </summary>
        /// <param name="task">The task model for the view.</param>
        /// <returns></returns>
        public ActionResult Edit(AnnouncementFormModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dbContext = GetDatabaseContext())
                    {
                        Announcement dbModel = dbContext.Announcements.Find(viewModel.AnnouncementID);

                        dbModel.AnnouncementContent = viewModel.AnnouncementContent;
                        dbModel.AnnouncementImportance = viewModel.AnnouncementImportance;
                        dbModel.AnnouncementTitle = viewModel.AnnouncementTitle;

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
        /// Delete the announcement.
        /// </summary>
        /// <param name="announcementId">The announcement id.</param>
        /// <returns></returns>
        public ActionResult Delete(int announcementId)
        {
            try
            {
                using (var dbContext = GetDatabaseContext())
                {
                    Announcement announcement = dbContext.Announcements.First(a => a.AnnouncementID == announcementId);
                    dbContext.Announcements.Remove(announcement);
                    dbContext.SaveChanges();
                    return new HttpStatusCodeResult(200);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpStatusCodeResult(500, "There was an error deleting the announcement");
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