using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AuthSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class CreateBanModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public CreateBanModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string BanReason { get; set; }
            public string BannedId { get; set; }
        }

        public async Task OnGetAsync(string rid)
        {
            ViewData["id"] = rid;
        }

        public async Task<IActionResult> OnPostAsync()
        {


            var user = _userManager.FindByIdAsync(Input.BannedId).Result;
            user.Banned = true;
            user.BannedReason = Input.BanReason;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);


            return RedirectToAction("UserDelete", "Products", new { uid = user.Id });
        }
    }
}
