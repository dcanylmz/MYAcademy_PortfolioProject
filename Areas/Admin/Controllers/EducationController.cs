using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult EducationList()
        {
            // Devam eden eğitimler (EndYear boş) en üstte, sonra en yeni başlayandan eskiye.
            var values = _context.Educations
                .OrderBy(x => x.EndYear.HasValue)
                .ThenByDescending(x => x.StartYear)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View(new Education { StartYear = DateTime.Now.Year });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEducation(Education education)
        {
            ValidateYears(education);
            if (!ModelState.IsValid)
                return View(education);

            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Eğitim kaydı eklendi.";
            return RedirectToAction(nameof(EducationList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
                return NotFound();

            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEducation(Education education)
        {
            ValidateYears(education);
            if (!ModelState.IsValid)
                return View(education);

            var existing = await _context.Educations.FindAsync(education.EducationID);
            if (existing == null)
                return NotFound();

            existing.SchoolName = education.SchoolName;
            existing.Department = education.Department;
            existing.StartYear = education.StartYear;
            existing.EndYear = education.EndYear;
            existing.Description = education.Description;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Eğitim kaydı güncellendi.";
            return RedirectToAction(nameof(EducationList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                TempData["ErrorMessage"] = "Silinecek kayıt bulunamadı.";
                return RedirectToAction(nameof(EducationList));
            }

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Eğitim kaydı silindi.";
            return RedirectToAction(nameof(EducationList));
        }

        private void ValidateYears(Education education)
        {
            if (education.EndYear.HasValue && education.EndYear < education.StartYear)
                ModelState.AddModelError(nameof(Education.EndYear), "Bitiş yılı başlangıç yılından önce olamaz.");
        }
    }
}
