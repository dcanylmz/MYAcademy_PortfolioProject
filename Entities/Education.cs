using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Education
    {
        public int EducationID { get; set; }

        [Required(ErrorMessage = "Okul adı zorunludur.")]
        [StringLength(150, ErrorMessage = "Okul adı en fazla 150 karakter olabilir.")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Bölüm zorunludur.")]
        [StringLength(150, ErrorMessage = "Bölüm en fazla 150 karakter olabilir.")]
        public string Department { get; set; }

        [Range(1950, 2100, ErrorMessage = "Başlangıç yılı 1950 ile 2100 arasında olmalıdır.")]
        public int StartYear { get; set; }

        // Boş bırakılırsa eğitim devam ediyor kabul edilir.
        [Range(1950, 2100, ErrorMessage = "Bitiş yılı 1950 ile 2100 arasında olmalıdır.")]
        public int? EndYear { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string? Description { get; set; }
    }
}
