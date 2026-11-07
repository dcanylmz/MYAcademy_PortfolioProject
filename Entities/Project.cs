namespace MYAcademy_PortfolioProject.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }       // Görsel yolu için (IFormFile ile doldurulacak)
        public string Technology { get; set; }     // Kullanılan teknolojiler
        public bool IsActive { get; set; }         // Aktif/Pasif durumu (3. Adım gereksinimi)

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
