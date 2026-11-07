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
<img width="1904" height="946" alt="01-ana-sayfa png" src="https://github.com/user-attachments/assets/ddeba8bb-ded3-433b-bfae-7258591703a6" />


#### 2. Yetenekler ve deneyimler

<img width="1903" height="946" alt="02-yetenekler-deneyimler png" src="https://github.com/user-attachments/assets/6669db66-5af1-47b2-99f3-c3753971a1fe" />


#### 3. Projeler (kategori filtresi ve sıralama)

<img width="1904" height="946" alt="03-projeler png" src="https://github.com/user-attachments/assets/268c691d-35db-4458-86dc-182accc0e17e" />


#### 4. İletişim formu
<img width="1904" height="943" alt="04-iletisim-formu png" src="https://github.com/user-attachments/assets/83ca9d9d-d806-4f88-ade5-e0fffa9b8765" />



### 🛠️ Admin Paneli

#### 5. Dashboard

<img width="1901" height="943" alt="05-dashboard1 png" src="https://github.com/user-attachments/assets/6915a1c5-e471-48a0-a332-b1603c0f6f69" />
<img width="1902" height="942" alt="06-dashboard2 png" src="https://github.com/user-attachments/assets/81df259f-0f43-4bc0-b5c0-1659f05c00f5" />

#### 6. Proje listesi

<img width="1919" height="941" alt="07-proje-listesi png" src="https://github.com/user-attachments/assets/6bfc4a00-d05a-4c3c-a15c-3fb329311760" />

#### 7. Proje ekleme formu

<img width="1917" height="941" alt="08-proje-formu png" src="https://github.com/user-attachments/assets/7ccada32-6f87-4228-8a4d-af951d3adf46" />

#### 8. Gelen kutusu

<img width="1919" height="944" alt="09-gelen-kutusu png" src="https://github.com/user-attachments/assets/b9fc329d-3097-4b63-b3f3-0c0c71c556c8" />

#### 9. Mesaj detay modalı

<img width="1918" height="942" alt="10-mesaj-modal png" src="https://github.com/user-attachments/assets/fe798ba0-da69-4658-8312-164072ca3c1c" />

#### 10. Silme onayı

<img width="1917" height="946" alt="11-silme-onayi png" src="https://github.com/user-attachments/assets/f19b24e9-7fe9-40df-98b8-fe25c9a14a64" />

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
