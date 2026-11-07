using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class HeroTitle
    {
        public int HeroTitleID { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Range(1, 100, ErrorMessage = "Sıra 1 ile 100 arasında olmalıdır.")]
        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
