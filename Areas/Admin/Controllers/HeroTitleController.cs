using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    // Ana sayfadaki daktilo animasyonunda dönen unvanlar.
    [Area("Admin")]
    public class HeroTitleController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroTitleController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult HeroTitleList()
        {
            var values = _context.HeroTitles.OrderBy(x => x.Order).ThenBy(x => x.HeroTitleID).ToList();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeroTitle(HeroTitle heroTitle)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction(nameof(HeroTitleList));
            }

            heroTitle.IsActive = true;
            _context.HeroTitles.Add(heroTitle);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Unvan eklendi.";
            return RedirectToAction(nameof(HeroTitleList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHeroTitle(int id)
        {
            var heroTitle = await _context.HeroTitles.FindAsync(id);
            if (heroTitle == null)
                return NotFound();

            return View(heroTitle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateHeroTitle(HeroTitle heroTitle)
        {
            if (!ModelState.IsValid)
                return View(heroTitle);

            var existing = await _context.HeroTitles.FindAsync(heroTitle.HeroTitleID);
            if (existing == null)
                return NotFound();

            existing.Title = heroTitle.Title;
            existing.Order = heroTitle.Order;
            existing.IsActive = heroTitle.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Unvan güncellendi.";
            return RedirectToAction(nameof(HeroTitleList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteHeroTitle(int id)
        {
            var heroTitle = await _context.HeroTitles.FindAsync(id);
            if (heroTitle == null)
            {
                TempData["ErrorMessage"] = "Silinecek unvan bulunamadı.";
                return RedirectToAction(nameof(HeroTitleList));
            }

            _context.HeroTitles.Remove(heroTitle);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Unvan silindi.";
            return RedirectToAction(nameof(HeroTitleList));
        }
    }
}
