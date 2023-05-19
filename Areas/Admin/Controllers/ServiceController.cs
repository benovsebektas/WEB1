using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;

namespace WebFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ICollection<Service> members = await _context.Services.ToListAsync();
            return View(members);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Service service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Service result = await _context.Services.FirstOrDefaultAsync(t => t.Id == service.Id);
            if (result is null)
            {
                TempData["Exists"] = "Bu Member bazada yoxdur";
                return RedirectToAction(nameof(Index));
            }
            result.ServiceImages = service.ServiceImages;
            result.Price = service.Price;
            result.CategoryId = service.CategoryId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}


