using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;

namespace Fleqx.Controllers
{
    /// <summary>
    /// Controller that controls all interactions with announcements.
    /// </summary>
    /// <seealso cref="Fleqx.Controllers.BaseController" />
    [Authorize]
    public class AnnouncementController : BaseController
    {
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
                            User = announcement.User
                        };
                    }).ToList();

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
    }
}