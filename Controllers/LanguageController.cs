using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.Extensions.Localization;


namespace AuthSystem.Controllers
{
//    public class LanguageController : Controller
//    {
//        public ActionResult SetLanguage()
//       {
//            if (Thread.CurrentThread.CurrentCulture.Name == "en")
//            {
//                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl");
//                ViewBag.CultBtn = "En";
//            }
//            else
//            {
//                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl");
//                ViewBag.CultBtn = "en";
//            }

//            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
//            [HttpPost]
//            Httpcook

//            return RedirectToAction("Index", "Home");
//      }
//    protected void Button2_Click(object sender, EventArgs e)
//    {
//        string dil = "en-US";
//        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(dil);
//        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
//        HttpCookie hc = new HttpCookie("dil");
//        hc.Expires = DateTime.Now.AddDays(30);
//        hc.Value = dil;
//        HttpContext.Current.Response.Cookies.Add(hc);
//    }
//}
    }
