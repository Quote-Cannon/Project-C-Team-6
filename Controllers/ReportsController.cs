using System;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using AuthSystem.Areas.Identity.Data;

using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.TagHelpers;


namespace AuthSystem.Controllers
{
    public class ReportsController : Controller
    {
        //private readonly IHtmlLocalizer<ProductsController> _localizer;
        private readonly AuthDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public ReportsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AuthDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }




        public async Task<IActionResult> Index()
        {
            var reports = from p in _context.Reports
                           select p;

            return View(await reports.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        public async Task<IActionResult> Ban(string id)
        {
            return RedirectToRoute("~/Identity/Pages/Account/CreateBan", id);

        }

        public async Task<IActionResult> BannedConfirm(string xemail)
        {
            ViewData["bannedemail"] = xemail;
            return View();

        }


        //[HttpGet("{repid}")]
        public async Task<IActionResult> Create(string repitemid)
        {
            int itemid =  Int32.Parse(repitemid);
            var repid = _context.Products.FirstOrDefault(x => x.Id == itemid).UserId;
            var email = _userManager.FindByIdAsync(repid).Result.Email;
            ViewData["ReportedEmail"] = email;
            ViewData["ReportedItemId"] = repitemid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportedUserId,Reporter,ReportedItemId,Subject")] Report report)
        {

            _context.Add(report);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ReportFinished));

        }

        public IActionResult ReportFinished()
        {
            return View();
        }


        public async Task<IActionResult> ShowBanAsync()
        {

            string reason = _userManager.GetUserAsync(User).Result.BannedReason;
            await _signInManager.SignOutAsync();
            return RedirectToAction("BannedMessage","Reports", new { r = reason});
        }
        public async Task<IActionResult> BannedMessage(string r)
        {
            ViewData["reason"] = r;
            return View();
        }


    }
}
