using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Services;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private const string UploadFolder = "projects";

        private readonly PortfolioContext _context;
        private readonly ImageUploadService _imageUploader;

        public ProjectController(PortfolioContext context, ImageUploadService imageUploader)
        {
            _context = context;
            _imageUploader = imageUploader;
        }

        // Admin listesi aktif ve pasif tüm projeleri gösterir.
        public IActionResult ProjectList(int? categoryId, string sort = "newest")
        {
            var query = _context.Projects.Include(x => x.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            query = sort switch
            {
                "oldest" => query.OrderBy(x => x.CreatedDate).ThenBy(x => x.ProjectID),
                "title" => query.OrderBy(x => x.ProjectTitle),
                _ => query.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.ProjectID)
            };

            FillCategories(categoryId);
            ViewBag.SelectedSort = sort;
            return View(query.ToList());
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            FillCategories();
            return View(new Project());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(Project project, IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                ModelState.AddModelError("imageFile", "Proje görseli zorunludur.");
            else
                ValidateImage(imageFile);

            ValidateCategory(project.CategoryId);

            if (!ModelState.IsValid)
            {
                FillCategories(project.CategoryId);
                return View(project);
            }

            project.ImageUrl = await _imageUploader.SaveAsync(imageFile!, UploadFolder);
            project.CreatedDate = DateTime.Now;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Proje başarıyla eklendi.";
            return RedirectToAction(nameof(ProjectList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            FillCategories(project.CategoryId);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProject(Project project, IFormFile? imageFile)
        {
            var existing = await _context.Projects.FindAsync(project.ProjectID);
            if (existing == null)
                return NotFound();

            var hasNewImage = imageFile is { Length: > 0 };
            if (hasNewImage)
                ValidateImage(imageFile!);

            ValidateCategory(project.CategoryId);

            if (!ModelState.IsValid)
            {
                project.ImageUrl = existing.ImageUrl; // formda mevcut görsel önizlemesi kaybolmasın
                FillCategories(project.CategoryId);
                return View(project);
            }

            // Formdan gelen entity'yi doğrudan Update etmek yerine alanları tek tek kopyalıyoruz:
            // CreatedDate gibi formda olmayan alanlar ezilmez.
            existing.ProjectTitle = project.ProjectTitle;
            existing.Description = project.Description;
            existing.Technology = project.Technology;
            existing.CategoryId = project.CategoryId;
            existing.IsActive = project.IsActive;

            var oldImage = existing.ImageUrl;
            if (hasNewImage)
                existing.ImageUrl = await _imageUploader.SaveAsync(imageFile!, UploadFolder);

            await _context.SaveChangesAsync();

            if (hasNewImage)
                _imageUploader.Delete(oldImage); // eski dosya, kayıt başarılı olduktan sonra silinir

            TempData["SuccessMessage"] = "Proje başarıyla güncellendi.";
            return RedirectToAction(nameof(ProjectList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleProjectStatus(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            project.IsActive = !project.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = project.IsActive
                ? "Proje aktif yapıldı, sitede listelenecek."
                : "Proje pasif yapıldı, sitede listelenmeyecek.";
            return RedirectToAction(nameof(ProjectList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                TempData["ErrorMessage"] = "Silinecek proje bulunamadı.";
                return RedirectToAction(nameof(ProjectList));
            }

            var imageUrl = project.ImageUrl;
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            _imageUploader.Delete(imageUrl);

            TempData["SuccessMessage"] = "Proje silindi.";
            return RedirectToAction(nameof(ProjectList));
        }

        private void FillCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(
                _context.Categories.OrderBy(x => x.CategoryName), "CategoryId", "CategoryName", selectedId);
        }

        private void ValidateImage(IFormFile imageFile)
        {
            var error = _imageUploader.Validate(imageFile);
            if (error != null)
                ModelState.AddModelError("imageFile", error);
        }

        // Dropdown dışında bir id gönderilirse foreign key hatası yerine form hatası gösterilir.
        private void ValidateCategory(int categoryId)
        {
            if (categoryId > 0 && !_context.Categories.Any(x => x.CategoryId == categoryId))
                ModelState.AddModelError(nameof(Project.CategoryId), "Seçilen kategori bulunamadı.");
        }
    }
}
