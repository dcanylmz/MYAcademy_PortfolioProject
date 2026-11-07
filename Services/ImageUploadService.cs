namespace MYAcademy_PortfolioProject.Services
{
    /// <summary>
    /// Görsel yükleme: tür/boyut doğrulama, güvenli dosya adı üretme ve yalnızca dosya yolunu döndürme.
    /// Proje dışındaki modüller (Hero, About vb.) de aynı servisi kullanabilir.
    /// </summary>
    public class ImageUploadService
    {
        private const long MaxFileSizeInBytes = 2 * 1024 * 1024; // 2 MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        private readonly IWebHostEnvironment _env;

        public ImageUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>Geçerliyse null, değilse kullanıcıya gösterilecek hata mesajını döndürür.</summary>
        public string? Validate(IFormFile file)
        {
            if (file.Length == 0)
                return "Seçilen dosya boş.";

            if (file.Length > MaxFileSizeInBytes)
                return "Görsel en fazla 2 MB olabilir.";

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                return "Yalnızca JPG, PNG veya WEBP dosyası yükleyebilirsiniz.";

            if (!HasValidSignature(file, extension))
                return "Dosya içeriği, seçilen görsel türüyle uyuşmuyor.";

            return null;
        }

        /// <summary>Dosyayı wwwroot/uploads/{folder} altına rastgele bir adla kaydeder, "/uploads/..." yolunu döndürür.</summary>
        public async Task<string> SaveAsync(IFormFile file, string folder)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid():N}{extension}"; // kullanıcının dosya adı hiç kullanılmaz

            var directory = Path.Combine(_env.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(directory);

            await using var stream = new FileStream(Path.Combine(directory, fileName), FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{folder}/{fileName}";
        }

        /// <summary>Daha önce kaydedilmiş bir görseli siler (yalnızca uploads klasörü içindeki dosyalar).</summary>
        public void Delete(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath) || !relativePath.StartsWith("/uploads/"))
                return;

            var uploadsRoot = Path.GetFullPath(Path.Combine(_env.WebRootPath, "uploads")) + Path.DirectorySeparatorChar;
            var fullPath = Path.GetFullPath(Path.Combine(_env.WebRootPath, relativePath.TrimStart('/')));

            if (fullPath.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase) && File.Exists(fullPath))
                File.Delete(fullPath);
        }

        // Uzantı kolayca değiştirilebildiği için dosyanın ilk baytlarına (imza) da bakılır.
        private static bool HasValidSignature(IFormFile file, string extension)
        {
            using var stream = file.OpenReadStream();
            var header = new byte[12];
            var read = stream.Read(header, 0, header.Length);
            if (read < header.Length)
                return false;

            return extension switch
            {
                ".jpg" or ".jpeg" => header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF,
                ".png" => header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47,
                ".webp" => header[0] == 'R' && header[1] == 'I' && header[2] == 'F' && header[3] == 'F'
                           && header[8] == 'W' && header[9] == 'E' && header[10] == 'B' && header[11] == 'P',
                _ => false
            };
        }
    }
}
