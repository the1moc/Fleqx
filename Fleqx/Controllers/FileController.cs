using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
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
    public class FileController : BaseController
    {
        protected UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        public FileController()
            : this(new UserManager<User>(new UserStore<User>(new DatabaseContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public FileController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get the current files.
        /// </summary>
        /// <returns></returns>
        public ActionResult Files()
        {
            using (var dbContext = GetDatabaseContext())
            {
                List<DataFile> files = dbContext.Files.ToList();

                List<FileModel> viewModels = files.Select(f => new FileModel
                    {
                        FileId = f.DataFileId,
                        FilePath = f.FilePath,
                        FileName = f.FileName,
                        Username = f.User.UserName
                    }).ToList();

                ViewBag.Title = "Files";

                return View("File", viewModels);
            }
        }

        /// <summary>
        /// Upload a file
        /// </summary>
        /// <returns></returns>
        public string UploadFile()
        {
            using (var dbContext = GetDatabaseContext())
            {
                string filePath = Server.MapPath("~/App_Data/");
                foreach (string nameOfFile in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[nameOfFile];
                    if (!System.IO.File.Exists(filePath + "/" + nameOfFile))
                    {
                        file.SaveAs(filePath + file.FileName);
                        dbContext.Files.Add(new DataFile
                        {
                            FileName = file.FileName,
                            UserId = GetCurrentUser().Id,
                            FilePath = filePath + file.FileName
                        });

                        dbContext.SaveChanges();
                    }


                }
            }

            // Temp, return a valid JSON object
            return JsonConvert.SerializeObject("Upload Success");
        }

        /// <summary>
        /// Download a file
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadFile(string fileId)
        {
            using (var dbContext = GetDatabaseContext())
            {
                int parsedNumber = Int32.Parse(fileId);
                DataFile file = dbContext.Files.First(f => f.DataFileId == parsedNumber);
                return File(file.FilePath, MimeMapping.GetMimeMapping(file.FileName), file.FileName);
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