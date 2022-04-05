using INTEXimdone.Models;
using INTEXimdone.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace INTEXimdone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ICrashRepository repo;
        public HomeController(ICrashRepository temp)
        {
            repo = temp;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult PrivacyPolicy()
        {
            return View("PrivacyPolicy");
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Temp(int pageNum)
        {
            int pageSize = 10;

            //var temp = repo.Crashes.Take(10).ToList();
            var temp = new CrashesViewModel
            {
                Crashes = repo.Crashes.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList(),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = (repo.Crashes.Count()),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(temp);
        }
    }
}
