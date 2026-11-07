using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var values = _context.Categories
                .Include(x => x.Projects)
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(values);
        }

        // Ekleme formu liste sayfasının üstünde olduğu için hata durumunda listeye mesajla dönülür.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .First();
                return RedirectToAction(nameof(CategoryList));
            }

            category.CategoryName = category.CategoryName.Trim();

            if (await _context.Categories.AnyAsync(x => x.CategoryName == category.CategoryName))
            {
                TempData["ErrorMessage"] = "Bu isimde bir kategori zaten var.";
                return RedirectToAction(nameof(CategoryList));
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CategoryName = category.CategoryName.Trim();

                var isDuplicate = await _context.Categories
                    .AnyAsync(x => x.CategoryName == category.CategoryName && x.CategoryId != category.CategoryId);
                if (isDuplicate)
                    ModelState.AddModelError(nameof(Category.CategoryName), "Bu isimde bir kategori zaten var.");
            }

            if (!ModelState.IsValid)
                return View(category);

            var existing = await _context.Categories.FindAsync(category.CategoryId);
            if (existing == null)
                return NotFound();

            existing.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Kategori güncellendi.";
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Silinecek kategori bulunamadı.";
                return RedirectToAction(nameof(CategoryList));
            }

            // EF Core zorunlu ilişkilerde varsayılan olarak cascade delete uygular:
            // kontrol etmezsek kategoriyle birlikte projeler de silinirdi.
            if (await _context.Projects.AnyAsync(x => x.CategoryId == id))
            {
                TempData["ErrorMessage"] = "Bu kategoriye bağlı projeler var. Önce projeleri başka kategoriye taşıyın veya silin.";
                return RedirectToAction(nameof(CategoryList));
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Kategori silindi.";
            return RedirectToAction(nameof(CategoryList));
        }
    }
}
