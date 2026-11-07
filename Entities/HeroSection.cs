using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class HeroSection
    {
        public int HeroSectionID { get; set; }

        [Required(ErrorMessage = "Karşılama metni zorunludur.")]
        [StringLength(100, ErrorMessage = "Karşılama metni en fazla 100 karakter olabilir.")]
        public string Greeting { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(150, ErrorMessage = "Başlık en fazla 150 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Description { get; set; }

        [StringLength(500, ErrorMessage = "Görsel yolu en fazla 500 karakter olabilir.")]
        public string? MainImageURL { get; set; }
    }
}
