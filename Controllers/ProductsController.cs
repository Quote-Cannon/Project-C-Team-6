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


namespace AuthSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AuthDbContext _context;
        public ProductsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string[] productOffer, string[] productType)
        {
            //var plants = await _context.Products.ToListAsync();
            //return View(plants);
            var products = from p in _context.Products
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString) || s.Kind.Contains(searchString) || s.Type.Contains(searchString) || s.LatinName.Contains(searchString));
            }

            if (productOffer.Length != 0 || productOffer != null)
            {
                foreach(var item in productOffer)
                {
                    products = products.Where(p => p.Kind.Contains(item));
                }
            }

            if (productType.Length != 0 || productType != null)
            {
                foreach(var item in productType)
                {
                    products = products.Where(p => p.Type.Contains(item));
                }
            }

           
            return View(await products.ToListAsync());
        }
        public async Task<IActionResult> SomeUserProducts(string publisher)
        {
           /* var pub = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == publisher);
            if (pub == null)
            {
                return NotFound();
            }*/
            IEnumerable<Product> products = from p in _context.Products
                           select p;
            List<Product> newProds = products.ToList();
            List<Product> newerProds = new List<Product>();
            for (int i = 0; i < newProds.Count; i++)
            {
                if (newProds[i].UserId == publisher)
                    newerProds.Add(newProds[i]);
            }
            IEnumerable<Product> qry = newerProds.AsEnumerable();
            return View( qry.ToList());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LatinName,Description,Kind,Type,Water,Light,ProductDate,Trade,UserId,PublisherName, Delivery")] Product product, IFormFile Picture, ApplicationUser DezeUser)
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
                    _context.Add(product);
                    DezeUser.AddProductToUser(product);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LatinName,Description,Kind,Type,Water,Light,ProductDate,Trade, Delivery")] Product product, IFormFile Picture)
        {
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
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
    }


}
