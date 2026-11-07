using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Message
    {
        public int MessageID { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(150, ErrorMessage = "E-posta en fazla 150 karakter olabilir.")]
        public string SenderEmail { get; set; }

        [Required(ErrorMessage = "Konu zorunludur.")]
        [StringLength(150, ErrorMessage = "Konu en fazla 150 karakter olabilir.")]
        public string SenderSubject { get; set; }

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj 10 ile 2000 karakter arasında olmalıdır.")]
        public string MessageDetail { get; set; }

        // SendDate ve IsRead sunucuda atanır, formdan alınmaz.
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
