using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Models
{
    // Farklı entity'lerden gelen verileri tek ekrana taşımak için kullanılır.
    public class DashboardViewModel
    {
        // İstatistik kartları
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int TotalMessages { get; set; }
        public int UnreadMessages { get; set; }

        public List<Message> LastMessages { get; set; } = new();
        public List<Project> LastProjects { get; set; } = new();
        public List<CategoryProjectCount> CategorySummary { get; set; } = new();
        public List<DailyMessageCount> Last7Days { get; set; } = new();

        public int ReadMessages => TotalMessages - UnreadMessages;
        public int ReadRate => TotalMessages == 0 ? 0 : ReadMessages * 100 / TotalMessages;
    }

    public class CategoryProjectCount
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ProjectCount { get; set; }
    }

    public class DailyMessageCount
    {
        public string Label { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
