using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers
{
    public class GradController : Controller
    {
        private readonly BookMarketplaceDbContext _context;

        public GradController(BookMarketplaceDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var gradovi = await _context.Gradovi
                .Include(g => g.Oglasi)
                .ToListAsync();

            return View(gradovi);
        }

        public async Task<IActionResult> Details(int id)
        {
            var grad = await _context.Gradovi
                .Include(g => g.Oglasi)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grad is null)
                return NotFound();

            return View(grad);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Grad grad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grad);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var grad = await _context.Gradovi.FindAsync(id);
            if (grad is null)
                return NotFound();

            return View(grad);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Grad grad)
        {
            if (id != grad.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(grad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grad);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grad = await _context.Gradovi.FindAsync(id);
            if (grad is null)
                return NotFound();

            return View(grad);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grad = await _context.Gradovi.FindAsync(id);
            if (grad is null)
                return NotFound();

            _context.Gradovi.Remove(grad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
