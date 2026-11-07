using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Models;
using System.Diagnostics;

namespace MYAcademy_PortfolioProject.Controllers
{
    public class HomeController : Controller
    {
        // Kök adres (/) şablondaki "Welcome" sayfası yerine portfolyo sayfasına yönlenir.
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Default");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
