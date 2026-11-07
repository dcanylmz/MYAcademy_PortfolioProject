using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillsComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillsComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Skills
                .Include(x => x.SkillItems)
                .OrderByDescending(x => x.SkillLevel)
                .ThenBy(x => x.Title)
                .ToList();
            return View(values);
        }
    }
}
