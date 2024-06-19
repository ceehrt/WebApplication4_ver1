using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Context;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class SProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.SProducts.ToListAsync());
        }

        // GET: SProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sProduct = await _context.SProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sProduct == null)
            {
                return NotFound();
            }

            return View(sProduct);
        }

        // GET: SProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Price,StockQuantity,Picture")] SProduct sProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sProduct);
        }

        // GET: SProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sProduct = await _context.SProducts.FindAsync(id);
            if (sProduct == null)
            {
                return NotFound();
            }
            return View(sProduct);
        }

        // POST: SProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Price,StockQuantity,Picture")] SProduct sProduct)
        {
            if (id != sProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SProductExists(sProduct.Id))
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
            return View(sProduct);
        }

        // GET: SProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sProduct = await _context.SProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sProduct == null)
            {
                return NotFound();
            }

            return View(sProduct);
        }

        // POST: SProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sProduct = await _context.SProducts.FindAsync(id);
            if (sProduct != null)
            {
                _context.SProducts.Remove(sProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SProductExists(int id)
        {
            return _context.SProducts.Any(e => e.Id == id);
        }
    }
}
