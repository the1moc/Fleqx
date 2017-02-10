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
		/// Show the announcements view.
		/// </summary>
		/// <returns></returns>
		public ActionResult Announcements()
		{
			using (var dbContext = GetDatabaseContext())
			{
				List<Announcement> announcements = dbContext.Announcements.ToList();

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
		/// Add a new announcement to the database.
		/// </summary>
		/// <param name="model">The announcement model.</param>
		/// <returns></returns>
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