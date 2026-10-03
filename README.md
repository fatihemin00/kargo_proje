# 📦 Gora Kargo Takip ve Envanter Yönetim Sistemi

ASP.NET MVC ve Entity Framework kullanılarak geliştirilmiş, detaylı yetkilendirme (RBAC) altyapısına sahip kapsamlı bir kargo ve envanter takip otomasyonudur.

## 🚀 Projenin Amacı ve Kapsamı
Bu proje, bir işletmenin gelen/giden kargo süreçlerini, envanter (ürün) giriş çıkışlarını, şirket (cari) ve kargo firması tanımlamalarını tek bir merkezden, güvenli bir şekilde yönetmesini sağlar.

## 🛡️ Rol Bazlı Yetkilendirme (RBAC)
Sistemde maksimum güvenlik ve veri bütünlüğü sağlamak amacıyla 4 farklı kullanıcı rolü tanımlanmıştır:
* **(A) Patron (Admin):** Sistemin tam hakimi. Yeni kullanıcı ekleme, silme, tüm şirket ve kargo firması tanımlamalarını yapma yetkisine sahiptir.
* **(B) Usta:** Operasyonel yönetici. Gelen ve giden kargoları ekleyebilir, ürünleri yönetebilir ve şirket bilgilerini güncelleyebilir ancak kullanıcı yönetimi yapamaz.
* **(C) Eleman:** Sadece temel operasyon işlemlerini gerçekleştirebilir (Gelen/Giden ürün ekleme vb.). Kritik silme işlemlerine erişimi yoktur.
* **(D) Misafir:** Yeni kayıt olan her kullanıcının varsayılan olarak atandığı, sadece okuma/listeleme yetkisi olan en düşük güvenlik rolüdür.

## 💻 Kullanılan Teknolojiler
* **Backend:** C#, ASP.NET MVC
* **Veritabanı:** MSSQL, Entity Framework (Code First / Database First)
* **Frontend:** HTML5, CSS3, Bootstrap, AdminLTE 3 Tema
* **İnteraktif İşlemler:** JavaScript, jQuery, AJAX (Sayfa yenilenmeden dinamik form gönderimi)
* **Versiyon Kontrol:** Git & GitHub

## 📌 Temel Özellikler
- **Dinamik Sidebar:** Giriş yapan kullanıcının rolüne göre sol menüdeki yetkisiz linkler otomatik olarak gizlenir.
- **Kullanıcı Profil Kartı:** Sisteme giren kişinin Adı-Soyadı ve Yetki Rolü anlık olarak ekranda gösterilir.
- **AJAX Tabanlı Formlar:** Kargo, Şirket ve Ürün ekleme/güncelleme işlemleri asenkron olarak çalışır.
- **Gelişmiş Filtreleme:** Listeleme sayfalarında anlık arama (search) yapabilen dinamik tablo yapıları.

## ⚙️ Kurulum
1. Projeyi bilgisayarınıza klonlayın: `git clone https://github.com/fatihemin00/kargo_proje.git`
2. SQL Server üzerinde projenin veritabanını oluşturun.
3. `Web.config` dosyası içerisindeki `connectionStrings` alanını kendi SQL sunucu bilgilerinize göre güncelleyin.
4. Projeyi Visual Studio üzerinden derleyip (Build) çalıştırın.

---
*Geliştirici:* **Fatih Emin Küçük**