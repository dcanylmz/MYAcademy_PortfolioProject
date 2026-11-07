using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Models;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private static readonly string[] DayLabels = { "Paz", "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt" };

        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                // Count / Where
                TotalProjects = _context.Projects.Count(),
                ActiveProjects = _context.Projects.Count(x => x.IsActive),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                TotalMessages = _context.Messages.Count(),
                UnreadMessages = _context.Messages.Count(x => !x.IsRead),

                // OrderByDescending / Take
                LastMessages = _context.Messages
                    .OrderByDescending(x => x.SendDate)
                    .ThenByDescending(x => x.MessageID)
                    .Take(5)
                    .ToList(),

                LastProjects = _context.Projects
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.CreatedDate)
                    .ThenByDescending(x => x.ProjectID)
                    .Take(3)
                    .ToList(),

                // GroupBy: kategori başına proje sayısı
                CategorySummary = _context.Projects
                    .GroupBy(x => x.Category!.CategoryName)
                    .Select(g => new CategoryProjectCount { CategoryName = g.Key, ProjectCount = g.Count() })
                    .OrderByDescending(x => x.ProjectCount)
                    .ToList()
            };

            model.Last7Days = GetLast7Days();
            return View(model);
        }

        // Son 7 günün her günü için (mesaj yoksa 0) gelen mesaj sayısı.
        private List<DailyMessageCount> GetLast7Days()
        {
            var today = DateTime.Today;
            var start = today.AddDays(-6);

            var countsByDay = _context.Messages
                .Where(x => x.SendDate >= start)
                .AsEnumerable() // Date özelliğiyle gruplamayı bellekte yapıyoruz (en fazla birkaç yüz kayıt)
                .GroupBy(x => x.SendDate.Date)
                .ToDictionary(g => g.Key, g => g.Count());

            return Enumerable.Range(0, 7)
                .Select(i => start.AddDays(i))
                .Select(day => new DailyMessageCount
                {
                    Label = DayLabels[(int)day.DayOfWeek],
                    Count = countsByDay.TryGetValue(day, out var c) ? c : 0
                })
                .ToList();
        }
    }
}
