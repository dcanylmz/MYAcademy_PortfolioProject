using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Services;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    // Ana sayfa karşılama alanı (tek kayıt). Yazı animasyonu metinleri HeroTitleController'dadır.
    [Area("Admin")]
    public class HeroController : Controller
    {
        private const string UploadFolder = "hero";

        private readonly PortfolioContext _context;
        private readonly ImageUploadService _imageUploader;

        public HeroController(PortfolioContext context, ImageUploadService imageUploader)
        {
            _context = context;
            _imageUploader = imageUploader;
        }

        public IActionResult Index()
        {
            return View(_context.HeroSections.FirstOrDefault());
        }

        [HttpGet]
        public IActionResult UpdateHero()
        {
            return View(_context.HeroSections.FirstOrDefault() ?? new HeroSection());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateHero(HeroSection heroSection, IFormFile? imageFile)
        {
            var hasNewImage = imageFile is { Length: > 0 };
            if (hasNewImage)
            {
                var error = _imageUploader.Validate(imageFile!);
                if (error != null)
                    ModelState.AddModelError("imageFile", error);
            }

            var existing = _context.HeroSections.FirstOrDefault();

            if (!ModelState.IsValid)
            {
                heroSection.MainImageURL = existing?.MainImageURL;
                return View(heroSection);
            }

            var oldImage = existing?.MainImageURL;
            if (existing == null)
            {
                existing = new HeroSection();
                _context.HeroSections.Add(existing);
            }

            existing.Greeting = heroSection.Greeting;
            existing.Title = heroSection.Title;
            existing.Description = heroSection.Description;
            if (hasNewImage)
                existing.MainImageURL = await _imageUploader.SaveAsync(imageFile!, UploadFolder);

            await _context.SaveChangesAsync();
            if (hasNewImage)
                _imageUploader.Delete(oldImage);

            TempData["SuccessMessage"] = "Ana sayfa alanı kaydedildi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
