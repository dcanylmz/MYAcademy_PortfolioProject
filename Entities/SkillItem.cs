using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class SkillItem
    {
        public int SkillItemID { get; set; }

        [Required(ErrorMessage = "Madde adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Madde adı en fazla 100 karakter olabilir.")]
        public string SkillItemTitle { get; set; }

        public int SkillID { get; set; }

        public Skill? Skill { get; set; }
    }
}
