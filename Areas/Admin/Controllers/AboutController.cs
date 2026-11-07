using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;

        public AboutController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult AboutList()
        {
            var values = _context.Abouts.FirstOrDefault();
            return View(values);
        }
        
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAbout(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }
            _context.Abouts.Add(about);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Hakkında bilgisi başarıyla eklendi.";
            return RedirectToAction("AboutList");
        }
    }
}
