using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Localization;

namespace AuthSystem.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly AuthDbContext _context;
        private readonly IStringLocalizer _localizer;

        public string currentAction = "";

        public HomeController(ILogger<HomeController> logger, AuthDbContext context, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _context = context;
            _localizer = localizer;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        public IActionResult Chat()
        {

            return View();
        }

        public IActionResult Aboutus()
        {

            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
