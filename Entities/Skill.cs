namespace MYAcademy_PortfolioProject.Entities
{
    public class Skill
    {
        public int SkillID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconURL { get; set; }
        public string SkillValue { get; set; }
        public List<SkillItem> SkillItems { get; set; }
    }
}
