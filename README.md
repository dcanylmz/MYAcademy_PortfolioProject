<div align="center">

# 🗂️ Portfolyo

**Admin panelinden yönetilen, tamamen dinamik bir kişisel portfolyo sitesi.**

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/mvc/overview)
[![EF Core](https://img.shields.io/badge/EF_Core-Code_First-68217A)](https://learn.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)

<sub>M&Y Yazılım Eğitim Akademi · 12. Dönem Gündüz Grubu · 1. Proje (Portfolyo Case)</sub>

</div>

---

## 📑 İçindekiler

- [Öne Çıkanlar](#-öne-çıkanlar)
- [Ekran Görüntüleri](#-ekran-görüntüleri)
- [Hızlı Başlangıç](#-hızlı-başlangıç)
- [Modüller](#-modüller)
- [Yapılan Geliştirmeler](#-yapılan-geliştirmeler)
- [Özgün Katkı](#-özgün-katkı)
- [Bilinen Sınırlamalar](#-bilinen-sınırlamalar)

---

## ✨ Öne Çıkanlar

| | |
|---|---|
| 🔄 **Tam dinamik içerik** | Adminde yapılan her değişiklik, veritabanı üzerinden siteye anında yansır. |
| 📥 **Gelişmiş gelen kutusu** | Arama, modal detay, okundu/okunmadı durumu ve canlı okunmamış sayacı. |
| 📊 **Anlamlı dashboard** | 7 istatistik kartı, son mesajlar, kategori özeti ve son projeler. |
| 🖼️ **Güvenli görsel yükleme** | Tür, boyut ve dosya imzası kontrolü; rastgele dosya adı. |
| ✅ **Sağlam doğrulama** | Data Annotations ve sunucu tarafı `ModelState`, alan bazlı hata mesajları. |
| 🧩 **Temiz mimari** | View Component'ler, paylaşılan form partial'ları, tek yükleme servisi. |

---

### 🌐 Herkese Açık Site

#### 1. Ana sayfa (hero ve yazı animasyonu)


#### 2. Yetenekler ve deneyimler



#### 3. Projeler (kategori filtresi ve sıralama)



#### 4. İletişim formu



### 🛠️ Admin Paneli

#### 5. Dashboard



#### 6. Proje listesi



#### 7. Proje ekleme formu



#### 8. Gelen kutusu



#### 9. Mesaj detay modalı


#### 10. Silme onayı



---

## 🚀 Hızlı Başlangıç

**Gereksinimler:** .NET 10 SDK, SQL Server (LocalDB de olur), Visual Studio 2022 veya sonrası.

```bash
# 1. Depoyu klonla
git clone https://github.com/dcanylmz/MYAcademy_PortfolioProject.git
cd MYAcademy_PortfolioProject

# 2. Context/PortfolioContext.cs içindeki bağlantı dizesini kendi SQL Server'ına göre düzenle

# 3. Veritabanını oluştur
dotnet ef database update      # veya Package Manager Console: Update-Database

# 4. Çalıştır
dotnet run
```

Uygulama `https://localhost:7092` adresinde açılır.

| Alan | Adres |
|---|---|
| 🌐 Herkese açık site | `/` (otomatik olarak `/Default/Index` sayfasına yönlenir) |
| 🛠️ Admin paneli | `/Admin/Dashboard/Index` |

**İçerik giriş sırası (önerilen):** Ana Sayfa → Hakkımda → İletişim → Sosyal Medya → Kategoriler → Projeler → Yetenekler → Deneyimler → Eğitim.

> [!NOTE]
> Yüklenen görseller `wwwroot/uploads/` altına kaydedilir; klasörün çalışma anında yazılabilir olması gerekir. `Program.cs` içinde `AddScoped<ImageUploadService>()` ve `UseStaticFiles()` satırları bulunmalıdır.

---

## 🧱 Modüller

| Modül | Admin ekranı | Sitedeki karşılığı |
|---|---|---|
| 🏠 **Ana Sayfa (Hero)** | Güncelleme, görsel yükleme, yazı animasyonu metinleri (CRUD) | Karşılama alanı ve daktilo animasyonu |
| 👤 **Hakkımda** | Güncelleme, görsel yükleme | "Beni Tanıyın" bölümü |
| ⭐ **Yeteneklerim** | CRUD, 0-100 seviye kaydırıcısı | İlerleme çubuklu kartlar |
| 💼 **Deneyimlerim** | CRUD, aktif/pasif | Zaman çizelgesi (yalnızca aktifler) |
| 🎓 **Eğitim** | CRUD, devam eden eğitimler için boş bitiş yılı | Eğitim listesi |
| 📁 **Projelerim** | CRUD, kategori dropdown, görsel yükleme, aktif/pasif | Kategori filtresi ve sıralama, pasifler gizli |
| 🏷️ **Kategoriler** | CRUD | Proje filtre butonları |
| ✉️ **İletişim ve Sosyal Medya** | İletişim bilgisi güncelleme, sosyal bağlantı CRUD | İletişim bölümü, hero ve footer |
| 📥 **Gelen Mesajlar** | Arama, durum filtresi, modal detay, okundu/okunmadı, silme | Çalışan iletişim formu |
| 📊 **Dashboard** | İstatistik kartları, son mesajlar, kategori özeti, son projeler, hızlı erişim | - |

---

## 🔧 Yapılan Geliştirmeler

<details open>
<summary><b>Veri modeli ve doğrulama</b></summary>

- Tüm entity'lere `[Required]`, `[StringLength]`, `[Range]`, `[EmailAddress]`, `[Url]` ve `[Phone]` eklendi. Sunucu tarafında `ModelState.IsValid` kontrolü ve alan bazlı Türkçe hata mesajları var.
- `Category` - `Project` bire-çok ilişkisi. Proje formunda kategoriler veritabanından `SelectList` + `asp-items` ile dolar, listede kategori adı `Include` ile gösterilir.
- `Skill.SkillLevel` sayısal (0-100) hale getirildi. `Education` entity'si ve `Project.CreatedDate` eklendi.

</details>

<details open>
<summary><b>Görsel yükleme</b></summary>

`Services/ImageUploadService.cs`: JPG/PNG/WEBP kabul edilir, en fazla 2 MB. Veritabanında yalnızca dosya yolu tutulur, dosya adı `Guid` ile üretilir. Güncelleme ve silmede eski dosya temizlenir, silme işlemi yalnızca `uploads` klasörüyle sınırlıdır.

</details>

<details open>
<summary><b>Gelen kutusu</b></summary>

- Ziyaretçi formu sayfa yenilenmeden gönderilir ve veritabanına kaydedilir.
- Liste en yeniden eskiye sıralıdır, gönderen veya konuya göre aranabilir.
- Mesaj Bootstrap Modal içinde açılır. Okunmamış satırlar görsel olarak ayrışır, okunmamış sayısı sidebar'da ve dashboard'da görünür.
- Mesaj içeriği düzenlenmez, yalnızca durumu yönetilir.

</details>

<details open>
<summary><b>Dashboard</b></summary>

`DashboardViewModel` ile 7 istatistik kartı, son 5 mesaj, `GroupBy` ile kategori özeti, son 3 proje ve hızlı erişim kısayolları tek ekranda sunulur. Sorgularda `Count`, `Where`, `OrderByDescending`, `Take` ve `GroupBy` kullanıldı.

</details>

<details open>
<summary><b>Kullanıcı deneyimi</b></summary>

Silme işlemlerinde SweetAlert2 onayı, kayıt işlemlerinde başarı/hata bildirimi (toast). Ekleme ve güncelleme ekranları ortak form partial'ları kullanır.

</details>

---

## 💡 Özgün Katkı

**1. Gelen kutusu deneyimi**
Dashboard'daki son mesajlara tıklanınca gelen kutusu açılır ve o mesajın modalı otomatik açılır (`?openId=`). Mesaj açıldığı anda okundu olarak işaretlenir. Sidebar'daki okunmamış sayısı sayfa yenilenmeden güncellenir.

**2. Güvenlik iyileştirmeleri**
- Yüklenen dosyanın yalnızca uzantısına değil, ilk baytlarına (dosya imzası) da bakılır.
- İletişim formunda görünmez bir *honeypot* alanı ile basit spam koruması vardır.
- Formdan gelen `SendDate` ve `IsRead` gibi alanlar `Bind` ile engellenir (overposting koruması).
- Tüm POST işlemlerinde antiforgery token kullanılır, silme işlemleri GET ile yapılmaz.

**3. Veri bütünlüğü**
Projesi olan bir kategori silinemez (aksi halde EF Core'un cascade delete davranışı projeleri de silerdi). Pasif deneyim ve projeler sitede gizlenir ama admin panelinde yönetilebilir.

---

## ⚠️ Bilinen Sınırlamalar

- Admin paneline giriş/yetkilendirme yoktur (case kapsamında istenmedi).
- `SkillItem` (yetenek altındaki teknoloji rozetleri) için yönetim ekranı yoktur.
- Bağlantı dizesi şu an kod içindedir, yayına alırken `appsettings.json` veya ortam değişkenine taşınmalıdır.

---

<div align="center">

Geliştirici: [**Doğancan Yılmaz**](https://github.com/dcanylmz)

</div>
