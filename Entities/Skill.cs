using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Skill
    {
        public int SkillID { get; set; }

        [Required(ErrorMessage = "Yetenek adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Yetenek adı en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [StringLength(300, ErrorMessage = "Açıklama en fazla 300 karakter olabilir.")]
        public string? Description { get; set; }

        [StringLength(300, ErrorMessage = "İkon alanı en fazla 300 karakter olabilir.")]
        public string? IconURL { get; set; }

        // Eski SkillValue (string) yerine 0-100 arası sayısal seviye.
        [Range(0, 100, ErrorMessage = "Seviye 0 ile 100 arasında olmalıdır.")]
        public int SkillLevel { get; set; }

        public List<SkillItem> SkillItems { get; set; } = new();
    }
}
