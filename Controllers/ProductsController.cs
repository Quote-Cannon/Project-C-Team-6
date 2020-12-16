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
        public async Task<IActionResult> Index(string searchString)
        {
            //var plants = await _context.Products.ToListAsync();
            //return View(plants);
            var products = from p in _context.Products
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString) || s.Kind.Contains(searchString) || s.Type.Contains(searchString) || s.LatinName.Contains(searchString));
            }
            return View(await products.ToListAsync());
        }

        // filter
        public async Task<IActionResult> Filter(string[] seeds, string cutting, string bud, string climber, string creeper, string herb, string shrub, string tree)
        {
            var products = from p in _context.Products
                           select p;

            if (seeds.Length != 0 || seeds != null)
            {
                products = from p in products
                               where seeds.Contains(p.Kind)
                               select p;
            }
            return View(products);
        }

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
        public async Task<IActionResult> Create([Bind("Id,Name,LatinName,Description,Kind,Type,Water,Light,ProductDate,Trade")] Product product, IFormFile Picture)
        {
            //var file = HttpContext.Request.Form.Files;
            byte[] streamOutput;
            string output = "";
            using (MemoryStream ms = new MemoryStream())
            {
                Picture.CopyTo(ms);
                streamOutput = ms.ToArray();
            }
            foreach (byte b in streamOutput)
            {
                string number = Convert.ToString(Convert.ToInt32(b));
                while (number.Length < 3)
                    number = "0" + number;
                output += number;
            }
            //This clause is supposed to check if the only error is an empty Picture field, since I (Mattias) can't find a way to get rid of it. Any other error should still trigger the clause.
            if (ModelState["Picture"].RawValue == null && ModelState.ErrorCount == 1)
            {
                product.Picture = output;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LatinName,Description,Picture,Kind,Type,Water,Light,ProductDate,Trade")] Product product)
        {
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
    }
}
