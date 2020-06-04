using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Homework09BirthdayCardAMalander.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.EnterCard cardInfo)
        {
            if (ModelState.IsValid)
            {
                return View("SuccessForm", cardInfo);

            }

            return View();

        }


        public IActionResult SuccessForm()
        {
            return View();
        }

    }
}