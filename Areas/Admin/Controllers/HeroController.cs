using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.HeroSections.FirstOrDefault();
            return View(values);
        }
        [HttpGet]
        public IActionResult UpdateHero(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateHero(HeroSection heroSection)
        {
            _context.HeroSections.Update(heroSection);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
