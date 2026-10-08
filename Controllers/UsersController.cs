using microfundamento_8_desenvolvimento_web_back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace microfundamento_8_desenvolvimento_web_back_end.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = await _context.Users
                .AsNoTracking()
                .OrderBy(user => user.Name)
                .ToListAsync();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            User user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            User user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            User user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            User user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
