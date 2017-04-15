using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Fleqx.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
            : this(new UserManager<User>(new UserStore<User>(new DatabaseContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public AccountController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get the account settings view.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Signup()
        {
            return View("_Signup", new SignupModel());
        }

        /// <summary>
        /// Get the account settings view.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit()
        {
            using (var dbContext = GetDatabaseContext())
            {
                User user = dbContext.Users.Find(User.Identity.GetUserId());

                AccountModel model = new AccountModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Team = user.TeamId,
                    UserName = user.UserName
                };

                return View("_Settings", model);
            }
        }

        /// <summary>
        /// Edit the user details.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AccountModel accountModel)
        {
            using (var dbContext = GetDatabaseContext())
            {
                User user = userManager.FindById(accountModel.Id);

                user.UserName = accountModel.UserName;
                user.FirstName = accountModel.FirstName;
                user.LastName = accountModel.LastName;
                user.TeamId = accountModel.Team;
                user.SecurityStamp = Guid.NewGuid().ToString();

                if (accountModel.Password != null)
                {
                    user.PasswordHash = new PasswordHasher().HashPassword(accountModel.Password);
                }
                userManager.Update(user);
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
        }
    }
}