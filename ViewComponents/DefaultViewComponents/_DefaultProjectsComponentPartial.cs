using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectsComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectsComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        // Filtre ve sıralama sorgu dizesinden gelir: /Default/Index?category=2&sort=oldest#projects
        public IViewComponentResult Invoke()
        {
            int? categoryId = int.TryParse(HttpContext.Request.Query["category"], out var parsed) ? parsed : null;
            var sort = HttpContext.Request.Query["sort"].ToString();

            // Pasif projeler sitede hiç listelenmez.
            var query = _context.Projects.Include(x => x.Category).Where(x => x.IsActive);

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            query = sort switch
            {
                "oldest" => query.OrderBy(x => x.CreatedDate).ThenBy(x => x.ProjectID),
                "title" => query.OrderBy(x => x.ProjectTitle),
                _ => query.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.ProjectID)
            };

            // Filtre butonlarında yalnızca aktif projesi olan kategoriler gösterilir.
            ViewBag.Categories = _context.Categories
                .Where(c => c.Projects.Any(p => p.IsActive))
                .OrderBy(c => c.CategoryName)
                .ToList();
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedSort = sort is "oldest" or "title" ? sort : "newest";

            return View(query.ToList());
        }
    }
}
