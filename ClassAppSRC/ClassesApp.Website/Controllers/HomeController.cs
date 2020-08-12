using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassesApp.Website.Models;
using ClassApp.Business;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Html;

namespace ClassesApp.Website.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserManager userManager;
		private readonly IClassesManager classesManager;
        private readonly IUserClassManager userClassManager;

		public HomeController(IUserManager userManager, IClassesManager classesManager, IUserClassManager userClassManager)
		{
			this.userManager = userManager;
			this.classesManager = classesManager;
            this.userClassManager = userClassManager;
		}



		public ActionResult ClassList()
		{
			var classes = classesManager.Classes
							.Select(t => new Website.Models.ClassWebsiteModel(t.Id, t.Name, t.Description, t.Price));
						
			return View(classes);
		}


        public ActionResult AddAClass()
        {
            var loginUser = HttpContext.Session.GetString("User");

            if (loginUser == null)
            {
                return View("LogIn");
            }
            else
            {
                var classesDropDown = classesManager.Classes
                        .Select(t => new Website.Models.ClassWebsiteModel(t.Id, t.Name, t.Description, t.Price));

                return View(classesDropDown);
            }

        }

       
       [HttpGet]
       public ActionResult EnrolledClasses()
        {
            var loginUser = HttpContext.Session.GetString("User");

            if (loginUser == null)
            {
                return View("LogIn");
            }
            else
            {

                var user = JsonConvert.DeserializeObject<Models.UserWebsiteModel>(HttpContext.Session.GetString("User"));

             
                var enrolledClasses = userClassManager.GetAll(user.Id)
                    .Select(t => new UserClassesWebsiteModel
                    {
                        UserID = t.UserId,
                        ClassID = t.ClassId,
                        ClassName = t.ClassName

                    })
                    .ToArray();
                return View(enrolledClasses);
            }
            
        }


        [HttpPost]
        public ActionResult EnrolledClasses(int id)
        {
            var selectedValue = Request.Form["ClassId"];
            
            bool isInt  = Int32.TryParse(selectedValue, out id);


            var user = JsonConvert.DeserializeObject<Models.UserWebsiteModel>(HttpContext.Session.GetString("User"));

            var classItem = userClassManager.Add(user.Id, id);
            var enrolledClasses = userClassManager.GetAll(user.Id)
                .Select(t => new UserClassesWebsiteModel
                {
                    UserID = t.UserId,
                    ClassID = t.ClassId,
                    ClassName = t.ClassName
                
                })
                .ToArray();
            return View(enrolledClasses);
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterWebsiteModel registerModel)
        {
            if (ModelState.IsValid)
            {
                userManager.Register(registerModel.UserEmail, registerModel.Password);

                return Redirect("~/");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginWebsiteModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserEmail, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new ClassesApp.Website.Models.UserWebsiteModel
                    {
                        Id = user.Id,
                        Name = user.Email
                    });

                    HttpContext.Session.SetString("User", json);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "User"),
                };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }


        public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
