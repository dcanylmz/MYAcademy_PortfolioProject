using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Social
    {
        public int SocialID { get; set; }

        [Required(ErrorMessage = "Platform adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Platform adı en fazla 50 karakter olabilir.")]
        public string SocialPlatform { get; set; }

        [Required(ErrorMessage = "Bağlantı zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir URL giriniz (https://...).")]
        [StringLength(300, ErrorMessage = "Bağlantı en fazla 300 karakter olabilir.")]
        public string SocialURL { get; set; }

        [Required(ErrorMessage = "İkon zorunludur.")]
        [StringLength(50, ErrorMessage = "İkon en fazla 50 karakter olabilir.")]
        public string SocialIcon { get; set; }
    }
}
