using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialController : Controller
    {
        private readonly PortfolioContext _context;

        public SocialController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SocialList()
        {
            return View(_context.Socials.OrderBy(x => x.SocialID).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSocial(Social social)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction(nameof(SocialList));
            }

            _context.Socials.Add(social);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Sosyal medya bağlantısı eklendi.";
            return RedirectToAction(nameof(SocialList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSocial(int id)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null)
                return NotFound();

            return View(social);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSocial(Social social)
        {
            if (!ModelState.IsValid)
                return View(social);

            var existing = await _context.Socials.FindAsync(social.SocialID);
            if (existing == null)
                return NotFound();

            existing.SocialPlatform = social.SocialPlatform;
            existing.SocialURL = social.SocialURL;
            existing.SocialIcon = social.SocialIcon;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Bağlantı güncellendi.";
            return RedirectToAction(nameof(SocialList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSocial(int id)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null)
            {
                TempData["ErrorMessage"] = "Silinecek bağlantı bulunamadı.";
                return RedirectToAction(nameof(SocialList));
            }

            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Bağlantı silindi.";
            return RedirectToAction(nameof(SocialList));
        }
    }
}
