namespace MYAcademy_PortfolioProject.Entities
{
    public class SkillItem
    {
        public int SkillItemID { get; set; }
        public string SkillItemTitle { get; set; }
        public int SkillID { get; set; }
        public Skill Skill{ get; set; }
    }
}
