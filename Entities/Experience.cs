namespace MYAcademy_PortfolioProject.Entities
{
    public class Experience
    {
        public int ExperienceID { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }

    }
}
