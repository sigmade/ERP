using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebERP.Models;

namespace WebERP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {


            if (HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Cookies"))
            {
                ViewData["Message"] = "Вы находитесь в системе";
                ViewData["Message_btn"] = "Выйти";
            }
            else
            {
                ViewData["Message"] = "Войдите в систему";
                ViewData["Message_btn"] = "Войти";
            }
            return View();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
