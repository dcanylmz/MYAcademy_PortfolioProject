using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillList()
        {
            var values = _context.Skills.OrderByDescending(x => x.SkillLevel).ThenBy(x => x.Title).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View(new Skill { SkillLevel = 50 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
                return View(skill);

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Yetenek başarıyla eklendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
                return View(skill);

            var existing = await _context.Skills.FindAsync(skill.SkillID);
            if (existing == null)
                return NotFound();

            existing.Title = skill.Title;
            existing.Description = skill.Description;
            existing.IconURL = skill.IconURL;
            existing.SkillLevel = skill.SkillLevel;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Yetenek güncellendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                TempData["ErrorMessage"] = "Silinecek yetenek bulunamadı.";
                return RedirectToAction(nameof(SkillList));
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Yetenek silindi.";
            return RedirectToAction(nameof(SkillList));
        }
    }
}
