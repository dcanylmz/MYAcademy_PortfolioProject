using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultSocialLinksComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSocialLinksComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        // style: "pill" (yuvarlak ikon butonları) veya "footer" (ikon + metin)
        public IViewComponentResult Invoke(string style = "pill")
        {
            ViewBag.Style = style;
            return View(_context.Socials.OrderBy(x => x.SocialID).ToList());
        }
    }
}
