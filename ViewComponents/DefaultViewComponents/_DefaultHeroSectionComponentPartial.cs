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
            var hero = _context.HeroSections.FirstOrDefault();

            // Tablo boşsa sayfa hata vermez, varsayılan metinlerle açılır.
            ViewBag.greeting = hero?.Greeting ?? "Merhaba";
            ViewBag.title = hero?.Title ?? "Portfolyoma Hoş Geldiniz";
            ViewBag.description = hero?.Description ?? string.Empty;
            ViewBag.mainImageURL = hero?.MainImageURL;

            var phrases = _context.HeroTitles
                .Where(x => x.IsActive)
                .OrderBy(x => x.Order)
                .Select(x => x.Title)
                .ToList();
            ViewBag.phrases = phrases.Count > 0 ? phrases : new List<string> { "Yazılım Geliştiricisi" };

            return View();
        }
    }
}
