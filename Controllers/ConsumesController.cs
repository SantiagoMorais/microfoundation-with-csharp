using microfundamento_8_desenvolvimento_web_back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace microfundamento_8_desenvolvimento_web_back_end.Controllers
{
    [Authorize]
    public class ConsumesController : Controller
    {
        private readonly AppDbContext _context;

        public ConsumesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Consume
        public async Task<IActionResult> Index()
        {
            List<Consume> consumes = await _context.Consumes.Include(c => c.Vehicle).ToListAsync();
            return View(consumes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consumes == null) return NotFound();

            Consume consume = await _context.Consumes
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consume == null) return NotFound();

            return View(consume);
        }

        // GET: Consume/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Name");
            return View();
        }

        // POST: Consume/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Date,Value,Milage,FuelType,VehicleId")] Consume consume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Name", consume.VehicleId);
            return View(consume);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consumes == null) return NotFound();

            Consume consume = await _context.Consumes.FindAsync(id);
            if (consume == null) return NotFound();

            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Name", consume.VehicleId);
            return View(consume);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Date,Value,Milage,FuelType,VehicleId")] Consume consume)
        {
            if (id != consume.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumeExists(consume.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Name", consume.VehicleId);
            return View(consume);
        }

        private bool ConsumeExists(int id)
        {
            return _context.Consumes.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consumes == null) return NotFound();

            Consume consume = await _context.Consumes
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consume == null) return NotFound();

            return View(consume);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consumes == null) return Problem("Entity set 'AppDbContext.Consumes'  is null.");
            var consume = await _context.Consumes.FindAsync(id);
            if (consume != null)
            {
                _context.Consumes.Remove(consume);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
