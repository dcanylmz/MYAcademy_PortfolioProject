using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Proje adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Proje adı en fazla 100 karakter olabilir.")]
        public string ProjectTitle { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
        public string Description { get; set; }

        // Veritabanında yalnızca dosya yolu tutulur (IFormFile ile yüklenen görselin yolu).
        [StringLength(500, ErrorMessage = "Görsel yolu en fazla 500 karakter olabilir.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Kullanılan teknolojiler zorunludur.")]
        [StringLength(200, ErrorMessage = "Teknoloji alanı en fazla 200 karakter olabilir.")]
        public string Technology { get; set; }

        // Pasif projeler admin panelinde görünür, public sayfada listelenmez.
        public bool IsActive { get; set; } = true;

        // "En yeni proje" sıralaması ve dashboard'daki son projeler için.
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int CategoryId { get; set; }

        // Nullable: formdan gelmez, ModelState'i bozmaz. Listelerde Include ile doldurulur.
        public Category? Category { get; set; }
    }
}
