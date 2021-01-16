using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace AuthSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _sender;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public void SendConfirmEmail(string receiverName, string receiverEmail, string confirmurl)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BiodiverCityMAX", "biodivercitymax010@gmail.com"));
            message.To.Add(new MailboxAddress(receiverName,receiverEmail));
            message.Subject = "Confirm your email on BiodiverCityMAX!";
            message.Body = new TextPart("plain")
            {
                Text = "Click on the link below to confirm your email\n" + confirmurl
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("biodivercitymax010@gmail.com", "g5_!P29=3hJ7hUE");
                client.Send(message); 
                client.Disconnect(true);
            }
        }

        public async Task<IActionResult> OnGetAsync(string name, string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            SendConfirmEmail(name, email, EmailConfirmationUrl);

            return Page();
        }
    }
}
