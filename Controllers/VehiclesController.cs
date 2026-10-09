using microfundamento_8_desenvolvimento_web_back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace microfundamento_8_desenvolvimento_web_back_end.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly AppDbContext _context;
        public VehiclesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = await _context.Vehicles.ToListAsync();
            return View(vehicles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            // I'm returning the data to the form on view
            return View(vehicle);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Report(int? id)
        {
            if (id == null) return NotFound();
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            List<Consume> consumes = await _context.Consumes.Where(c => c.VehicleId == id).OrderByDescending(c => c.Date).ToListAsync();

            float total = consumes.Sum(c => c.Value);

            ViewBag.Vehicle = vehicle;
            ViewBag.Total = total;

            return View(consumes);
        }
    }
}