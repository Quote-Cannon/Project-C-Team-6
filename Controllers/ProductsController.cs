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
    public class ProductsController : Controller
    {
        //private readonly IHtmlLocalizer<ProductsController> _localizer;
        private readonly AuthDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public ProductsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            AuthDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string[] productOffer, string[] productType, string[] productTrade, string[] productDelivery)
        {
            var products = from p in _context.Products
                           select p;

            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.PublisherName.Contains(searchString) || s.Name.Contains(searchString) || s.Description.Contains(searchString) || s.Kind.Contains(searchString) || s.Type.Contains(searchString) || s.LatinName.Contains(searchString) || s.Trade.Contains(searchString) || s.Delivery.Contains(searchString));
            }

            //filter
            if (productOffer.Length != 0 || productOffer != null)
            {
                foreach (var item in productOffer)
                {
                    products = products.Where(p => p.Kind.Contains(item));
                }
            }

            if (productType.Length != 0 || productType != null)
            {
                foreach (var item in productType)
                {
                    products = products.Where(p => p.Type.Contains(item));
                }
            }

            if (productTrade.Length != 0 || productTrade != null)
            {
                foreach (var item in productTrade)
                {
                    products = products.Where(p => p.Trade.Contains(item));
                }
            }

            if (productDelivery.Length != 0 || productDelivery != null)
            {
                foreach (var item in productDelivery)
                {
                    products = products.Where(p => p.Delivery.Contains(item));
                }
            }

            return View(await products.ToListAsync());
        }

        public async Task<IActionResult> SomeUserProducts(string searchString, string[] productOffer, string[] productType, string[] productTrade, string[] productDelivery, string publisher)
        {
            
            /* var pub = await _context.Users
                 .FirstOrDefaultAsync(m => m.Id == publisher);
             if (pub == null)
             {
                 return NotFound();
             }*/
            IEnumerable<Product> products = from p in _context.Products
                                            select p;

            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.PublisherName.Contains(searchString) || s.Name.Contains(searchString) || s.Description.Contains(searchString) || s.Kind.Contains(searchString) || s.Type.Contains(searchString) || s.LatinName.Contains(searchString) || s.Trade.Contains(searchString) || s.Delivery.Contains(searchString));
            }

            //filter
            if (productOffer.Length != 0 || productOffer != null)
            {
                foreach (var item in productOffer)
                {
                    products = products.Where(p => p.Kind.Contains(item));
                }
            }

            List<Product> oldprods = products.ToList();
            List<Product> newprods = new List<Product>();
            for (int i = 0; i < oldprods.Count; i++)
            {
                if (oldprods[i].UserId == publisher)
                    newprods.Add(oldprods[i]);
            }
            IEnumerable<Product> qry = newprods.AsEnumerable();
            ViewData["id"] = publisher;
            return View(qry.ToList());
        }

        /* public async Task<IActionResult> SomeUserProducts()
         {
             var products = from p in _context.Products
                            select p;
             return View(await products.ToListAsync());
         }*/
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }
        

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LatinName,Description,Kind,Type,Water,Light,ProductDate,Trade,UserId,PublisherName, Delivery")] Product product, IFormFile Picture, IFormFile PictureTwo, IFormFile PictureThree)
        {
            //var file = httpcontext.request.form.files;
            //byte[] streamoutput;
            //string output = "";
            //using (memorystream ms = new memorystream())
            //{
            //    picture.copyto(ms);
            //    streamoutput = ms.toarray();
            //}
            //foreach (byte b in streamoutput)
            //{
            //    string number = convert.tostring(convert.toint32(b));
            //    while (number.length < 3)
            //        number = "0" + number;
            //    output += number;
            //}
            ////this clause is supposed to check if the only error is an empty picture field, since i (mattias) can't find a way to get rid of it. any other error should still trigger the clause.
            //if (modelstate["picture"].rawvalue == null && modelstate.errorcount == 1)
            //{
            //    product.picture = output;
            //    _context.add(product);
            //    await _context.savechangesasync();
            //    return redirecttoaction(nameof(index));
            //}

            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        byte[] p1 = null;
                        byte[] p2 = null;
                        byte[] p3 = null;
                        using (var fs1 = Picture.OpenReadStream())
                        {
                            using (var ms = new MemoryStream())
                            {
                                fs1.CopyTo(ms);
                                p1 = ms.ToArray();
                            }
                            product.Picture = p1;
                        }
                        if (PictureTwo != null)
                        {
                            using (var fs2 = PictureTwo.OpenReadStream())
                            {
                                using (var ms = new MemoryStream())
                                {
                                    fs2.CopyTo(ms);
                                    p2 = ms.ToArray();
                                }
                                product.PictureTwo = p2;
                            }
                        }
                        if (PictureThree != null)
                        {
                            using (var fs3 = PictureThree.OpenReadStream())
                            {
                                using (var ms = new MemoryStream())
                                {
                                    fs3.CopyTo(ms);
                                    p3 = ms.ToArray();
                                }
                                product.PictureThree = p3;
                            }
                        }
                    }
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LatinName,Description,Kind,Type,Water,Light,ProductDate,Trade, Delivery,UserId,PublisherName")] Product product, IFormFile Picture, IFormFile PictureTwo, IFormFile PictureThree)
        {
            var pp = _context.Products.FirstOrDefault(p => p.Id.Equals(id));
            // Avoid overriding the EF tracking by first finding the right product, 
            //setting the image variable and detach the tracked product before updating the newer tracked product later on
            byte[] image = pp.Picture;
            if (pp != null)
            {
                // detach
                _context.Entry(pp).State = EntityState.Detached;
            }

            //if (id != product.Id)
            //{
            //    return NotFound();
            //}

            //byte[] streamOutput;
            //string output = "";
            //try
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        Picture.CopyTo(ms);
            //        streamOutput = ms.ToArray();
            //    }
            //    foreach (byte b in streamOutput)
            //    {
            //        string number = Convert.ToString(Convert.ToInt32(b));
            //        while (number.Length < 3)
            //            number = "0" + number;
            //        output += number;
            //    }
            //}
            //catch (NullReferenceException)
            //{
            //    output = "";
            //}
            ////This clause is supposed to check if the only error is an empty Picture field, since I (Mattias) can't find a way to get rid of it. Any other error should still trigger the clause.
            //if (ModelState["Picture"].RawValue == null && ModelState.ErrorCount == 1)
            //{
            //    try
            //    {
            //        product.Picture = output;
            //        _context.Update(product);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProductExists(product.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        byte[] p1 = null;
                        using (var fs1 = Picture.OpenReadStream())
                        {
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            product.Picture = p1;
                        }
                    }
                }
                if (Picture == null)
                {
                    product.Picture = image;
                }
                if (PictureTwo != null)
                {
                    if (PictureTwo.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        byte[] p2 = null;
                        using (var fs2 = PictureTwo.OpenReadStream())
                        {
                            using (var ms2 = new MemoryStream())
                            {
                                fs2.CopyTo(ms2);
                                p2 = ms2.ToArray();
                            }
                            product.PictureTwo = p2;
                        }
                    }
                }
                if (PictureTwo == null)
                {
                    product.PictureTwo = image;
                }
                if (PictureThree != null)
                {
                    if (PictureThree.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        byte[] p3 = null;
                        using (var fs3 = PictureThree.OpenReadStream())
                        {
                            using (var ms3 = new MemoryStream())
                            {
                                fs3.CopyTo(ms3);
                                p3 = ms3.ToArray();
                            }
                            product.PictureThree = p3;
                        }
                    }
                }
                if (PictureTwo == null)
                {
                    product.PictureTwo = image;
                }
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        //public FileContentResult getImg(int id)
        //{
        //    byte[] byteArray = _context.Products.Find(id).Picture;
        //    return new FileContentResult(byteArray, "image/jpg");
        //    //return byteArray != null
        //    //    ? new FileContentResult(byteArray, "image/jpg")
        //    //    : null;
        //}
        //public ActionResult GetImage(int i)
        //{
        //    byte[] bytes = db.GetImage(i); //Get the image from your database
        //    return File(bytes, "image/png"); //or "image/jpeg", depending on the format
        //}
        public async Task<IActionResult> UserDelete(string uid)
        {
            bool signout = false;
            
            // Deleting the user
            ApplicationUser thisUser = _userManager.FindByIdAsync(uid).Result;
            bool banned = thisUser.Banned;
            string email = thisUser.Email;
            if (uid == _userManager.GetUserAsync(User).Result.Id)
            {
                signout = true;
                var result = await _userManager.DeleteAsync(thisUser);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{uid}'.");
                }
            }
            
            // Deleting their products
            var products = from p in _context.Products
                           select p;
            foreach (var product in products)
            {
                if (product.UserId == uid)
                {
                    _context.Products.Remove(product);
                }
            }
            // Save changes to db.
            await _context.SaveChangesAsync();
            //Signing them out

            //_logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);
            //return RedirectToPage("/Account/Logout", new { returnUrl = urr }) ;
            if (signout)
            {
                await _signInManager.SignOutAsync();
            }

            if (banned)
            {
                return RedirectToAction("BannedConfirm","Reports",new {xemail = email });
            }
            return Redirect("~/");
        }
/*
        [HttpPost("{userId}")]
        public async Task<IActionResult> DeleteUserProducts(string userId)
        {
          
            var products = from p in _context.Products
                           select p;

            foreach (var product in products)
            {
                if (product.UserId == userId)
                {
                    _context.Products.Remove(product);
                }
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }*/
    }


}
