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
using System.Web.Security;
using System.Web.Caching;

namespace Fleqx.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private readonly UserManager<User> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityController"/> class.
        /// </summary>
        public SecurityController()
            : this(new UserManager<User>(new UserStore<User>(new DatabaseContext())))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public SecurityController(UserManager<User> userManager)
        {
            userManager.UserValidator = new UserValidator<User>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                
            };

            this.userManager = userManager;
        }

        /// <summary>
        /// Display the login screen view.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel
            {
                RequestedUrl = returnUrl
            };

            return View("LoginPage", model);
        }

        /// <summary>
        /// Attempt to login a user.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            // Return to the login page if the view model was invalid (incomplete data)
            if (!ModelState.IsValid)
            {
                return View("LoginPage");
            }

            User user = await userManager.FindAsync(loginModel.UserName, loginModel.Password);

            if (user != null)
            {
                ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                IOwinContext context = Request.GetOwinContext();
                context.Authentication.SignIn(identity);

                user.IsLoggedIn = 1;
                await userManager.UpdateAsync(user);

                if (string.IsNullOrEmpty(loginModel.RequestedUrl) || !Url.IsLocalUrl(loginModel.RequestedUrl))
                {
                    return Redirect(Url.Action("Home", "Home"));
                }

                ViewBag.Clear();
                return Redirect(loginModel.RequestedUrl);
            }

            ViewBag.Error = "Invalid credentials. Try again.";
            return View("LoginPage");
        }

        /// <summary>
        /// Signs up a user.
        /// </summary>
        /// <param name="signupModel">The signup model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SignUp(SignupModel signupModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(500, "The form was not filled out correctly, please check again");
            }

            // Create the user model.
            User user = new User
            {
                UserName  = signupModel.UserName,
                FirstName = signupModel.FirstName,
                LastName  = signupModel.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                TeamId    = signupModel.Team
            };

            // Check if the user already exists.
            User existingUser = await userManager.FindByNameAsync(user.UserName);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                return new HttpStatusCodeResult(500, "That user already exists");
            }

            // Add the user to the database.
            var result = await userManager.CreateAsync(user, signupModel.Password);

            if (result.Succeeded)
            {
                // Add them to the correct role.
                userManager.AddToRole(user.Id, ListControlHelper.GetRole(signupModel.Role));
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            return new HttpStatusCodeResult(500, "Error adding the user: " + result.Errors);
        }

        /// <summary>
        /// Logouts the user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            User user = userManager.FindById(HttpContext.User.Identity.GetUserId());
            user.IsLoggedIn = 0;
            await userManager.UpdateAsync(user);

            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return View("LoginPage");
        }
    }
}