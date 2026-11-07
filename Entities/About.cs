using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class About
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(150, ErrorMessage = "Başlık en fazla 150 karakter olabilir.")]
        public string AboutTitle { get; set; }

        [Required(ErrorMessage = "Hakkımda metni zorunludur.")]
        [StringLength(2000, ErrorMessage = "Hakkımda metni en fazla 2000 karakter olabilir.")]
        public string AboutContent { get; set; }

        [Required(ErrorMessage = "Deneyim başlığı zorunludur.")]
        [StringLength(100, ErrorMessage = "Deneyim başlığı en fazla 100 karakter olabilir.")]
        public string AboutExperienceTitle { get; set; }

        [Required(ErrorMessage = "Deneyim değeri zorunludur.")]
        [StringLength(50, ErrorMessage = "Deneyim değeri en fazla 50 karakter olabilir.")]
        public string AboutExperienceValue { get; set; }

        [StringLength(500, ErrorMessage = "Görsel yolu en fazla 500 karakter olabilir.")]
        public string? AboutMainImageUrl { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string AboutNameSurname { get; set; }

        [Required(ErrorMessage = "Konum zorunludur.")]
        [StringLength(100, ErrorMessage = "Konum en fazla 100 karakter olabilir.")]
        public string AboutLocation { get; set; }

        [Required(ErrorMessage = "Meslek/unvan zorunludur.")]
        [StringLength(100, ErrorMessage = "Meslek/unvan en fazla 100 karakter olabilir.")]
        public string AboutProfession { get; set; }

        [StringLength(500, ErrorMessage = "CV bağlantısı en fazla 500 karakter olabilir.")]
        public string? AboutCVUrl { get; set; }
    }
}
