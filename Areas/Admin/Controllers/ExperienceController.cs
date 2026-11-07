using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences
                .OrderByDescending(x => x.IsActive)
                .ThenByDescending(x => x.ExperienceID)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View(new Experience { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
                return View(experience);

            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Deneyim başarıyla eklendi.";
            return RedirectToAction(nameof(ExperienceList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
                return NotFound();

            return View(experience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
                return View(experience);

            var existing = await _context.Experiences.FindAsync(experience.ExperienceID);
            if (existing == null)
                return NotFound();

            existing.Title = experience.Title;
            existing.CompanyName = experience.CompanyName;
            existing.Description = experience.Description;
            existing.CategoryName = experience.CategoryName;
            existing.IsActive = experience.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Deneyim güncellendi.";
            return RedirectToAction(nameof(ExperienceList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleExperienceStatus(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
                return NotFound();

            experience.IsActive = !experience.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = experience.IsActive ? "Deneyim aktif yapıldı." : "Deneyim pasif yapıldı.";
            return RedirectToAction(nameof(ExperienceList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
            {
                TempData["ErrorMessage"] = "Silinecek deneyim bulunamadı.";
                return RedirectToAction(nameof(ExperienceList));
            }

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Deneyim silindi.";
            return RedirectToAction(nameof(ExperienceList));
        }
    }
}
