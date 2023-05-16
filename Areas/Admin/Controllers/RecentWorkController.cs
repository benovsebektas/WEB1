using Microsoft.AspNetCore.Mvc;
using WebFrontToBack.DAL;
using WebFrontToBack.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;


namespace WebFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RecentWorkController : Controller
    {
        private readonly AppDbContext _context;

        public RecentWorkController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<RecentWork> recentWorks = await _context.RecentWorks.ToListAsync();
            return View(recentWorks);
        }

       

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecentWork recentWork)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _context.RecentWorks.AddAsync(recentWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        public IActionResult Update(int Id)
        {
            RecentWork? recentWork = _context.RecentWorks.Find(Id);

            if (recentWork == null)
            {
                return NotFound();
            }

            return View(recentWork);
        }

        [HttpPost]
        public IActionResult Update(RecentWork recentWork)
        {
            RecentWork? editedRecentWork = _context.RecentWorks.Find(recentWork.Id);
            if (editedRecentWork == null)
            {
                TempData["Exists"] = "Bu Member bazada yoxdur";
                return RedirectToAction(nameof(Index));
            }
            editedRecentWork.Title = recentWork.Title;
            editedRecentWork.Description = recentWork.Description;
            editedRecentWork.ImagePath = recentWork.ImagePath;
            _context.RecentWorks.Update(editedRecentWork);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            RecentWork? recentWork = _context.RecentWorks.Find(Id);
            if (recentWork == null)
            {
                return NotFound();
            }
            _context.RecentWorks.Remove(recentWork);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
