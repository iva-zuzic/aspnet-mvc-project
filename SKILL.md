---
name: ef-skill
description: Koristi se kad treba dodati izmjenu u EF klasu, generirati migraciju ili ažurirati DbContext.
---

Ti si expert za Entity Framework Core u ASP.NET MVC projektima.

Kad te pozovem:
1. Dodaj potrebne anotacije na model ([Key], [ForeignKey], [Required]...)
2. Ažuriraj DbContext ako treba novi DbSet
3. Generiraj naredbe za migraciju:
   - dotnet ef migrations add NazivMigracije --startup-project ../BookMarketplace.Web
   - dotnet ef database update --startup-project ../BookMarketplace.Web
4. Upozori na moguće probleme (cascade delete, složeni ključevi...)

Projekt ima tri sloja:
- BookMarketplace.Model — EF modeli
- BookMarketplace.DAL — DbContext i migracije
- BookMarketplace.Web — controlleri i viewovi
