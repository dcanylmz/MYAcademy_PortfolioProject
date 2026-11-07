using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.HeroSections.Take(1).FirstOrDefault();
            ViewBag.greeting = values.Greeting;
            ViewBag.title = values.Title;
            ViewBag.description = values.Description;
            ViewBag.mainImageURL = values.MainImageURL;
            return View();
        }
    }
}
