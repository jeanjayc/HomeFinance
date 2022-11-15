using HomeFinance.Domain.Models;
using HomeFinance.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeFinance.MVC.Controllers
{
    public class FinancesController : Controller
    {
        private readonly AppDbContext _context;

        public FinancesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
              return View(await _context.Finances.ToListAsync());
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Finances == null)
            {
                return NotFound();
            }

            var finances = await _context.Finances
                .FirstOrDefaultAsync(m => m.FinancesId == id);
            if (finances == null)
            {
                return NotFound();
            }

            return View(finances);
        }
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FinancesId,FinanceName,DueDate,Price")] Finances finances)
        {
            if (ModelState.IsValid)
            {
                finances.FinancesId = Guid.NewGuid();
                _context.Add(finances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finances);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Finances == null)
            {
                return NotFound();
            }

            var finances = await _context.Finances.FindAsync(id);
            if (finances == null)
            {
                return NotFound();
            }
            return View(finances);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FinancesId,FinanceName,DueDate,Price")] Finances finances)
        {
            if (id != finances.FinancesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancesExists(finances.FinancesId))
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
            return View(finances);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Finances == null)
            {
                return NotFound();
            }

            var finances = await _context.Finances
                .FirstOrDefaultAsync(m => m.FinancesId == id);
            if (finances == null)
            {
                return NotFound();
            }

            return View(finances);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Finances == null)
            {
                return Problem("Entity set 'AppDbContext.Finances'  is null.");
            }
            var finances = await _context.Finances.FindAsync(id);
            if (finances != null)
            {
                _context.Finances.Remove(finances);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancesExists(Guid id)
        {
          return _context.Finances.Any(e => e.FinancesId == id);
        }
    }
}
