using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Services;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    // Hakkımda tek kayıttan oluşur: ekleme/silme yerine güncelleme odaklı yönetim.
    [Area("Admin")]
    public class AboutController : Controller
    {
        private const string UploadFolder = "about";

        private readonly PortfolioContext _context;
        private readonly ImageUploadService _imageUploader;

        public AboutController(PortfolioContext context, ImageUploadService imageUploader)
        {
            _context = context;
            _imageUploader = imageUploader;
        }

        public IActionResult AboutList()
        {
            return View(_context.Abouts.FirstOrDefault());
        }

        [HttpGet]
        public IActionResult UpdateAbout()
        {
            return View(_context.Abouts.FirstOrDefault() ?? new About());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAbout(About about, IFormFile? imageFile)
        {
            var hasNewImage = imageFile is { Length: > 0 };
            if (hasNewImage)
            {
                var error = _imageUploader.Validate(imageFile!);
                if (error != null)
                    ModelState.AddModelError("imageFile", error);
            }

            var existing = _context.Abouts.FirstOrDefault();

            if (!ModelState.IsValid)
            {
                about.AboutMainImageUrl = existing?.AboutMainImageUrl;
                return View(about);
            }

            var oldImage = existing?.AboutMainImageUrl;
            if (existing == null)
            {
                existing = new About();
                _context.Abouts.Add(existing);
            }

            existing.AboutTitle = about.AboutTitle;
            existing.AboutContent = about.AboutContent;
            existing.AboutExperienceTitle = about.AboutExperienceTitle;
            existing.AboutExperienceValue = about.AboutExperienceValue;
            existing.AboutNameSurname = about.AboutNameSurname;
            existing.AboutLocation = about.AboutLocation;
            existing.AboutProfession = about.AboutProfession;
            existing.AboutCVUrl = about.AboutCVUrl;
            if (hasNewImage)
                existing.AboutMainImageUrl = await _imageUploader.SaveAsync(imageFile!, UploadFolder);

            await _context.SaveChangesAsync();
            if (hasNewImage)
                _imageUploader.Delete(oldImage);

            TempData["SuccessMessage"] = "Hakkımda bilgileri kaydedildi.";
            return RedirectToAction(nameof(AboutList));
        }
    }
}
