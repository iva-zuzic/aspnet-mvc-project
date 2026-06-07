using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookMarketplace.Controllers;

public class OglasController : Controller
{
    private readonly BookMarketplaceDbContext _context;

    public OglasController(BookMarketplaceDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        var model = new OglasCreateModel
        {
            DatumIsteka = DateTime.Now.AddDays(30)
        };

        PopuniDropdowne();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OglasCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            PopuniDropdowne();
            return View(model);
        }

        var oglas = new Oglas
        {
            Naslov = model.Naslov.Trim(),
            Opis = model.Opis.Trim(),
            Cijena = model.Cijena,
            DatumObjave = DateTime.Now,
            DatumIsteka = model.DatumIsteka,
            Status = StatusOglasa.Aktivan,
            TipOglasa = model.TipOglasa,
            StanjeArtikla = model.StanjeArtikla,
            KorisnikId = model.KorisnikId,
            GradId = model.GradId
        };

        if (model.TipOglasa == TipOglasa.Knjiga)
        {
            oglas.Knjiga = new Knjiga
            {
                Naziv = model.KnjigaNaziv?.Trim() ?? string.Empty,
                Autor = model.KnjigaAutor?.Trim() ?? string.Empty,
                ISBN = model.KnjigaISBN?.Trim() ?? string.Empty,
                Izdavac = model.KnjigaIzdavac?.Trim() ?? string.Empty,
                GodinaIzdanja = model.KnjigaGodinaIzdanja ?? 0,
                BrojStrana = model.KnjigaBrojStrana ?? 0,
                Jezik = model.KnjigaJezik?.Trim() ?? string.Empty,
                Zanr = model.KnjigaZanr ?? ZanrKnjige.Drama
            };
        }
        else if (model.TipOglasa == TipOglasa.DrustvenaIgra)
        {
            oglas.DrustvenaIgra = new DrustvenaIgra
            {
                Naziv = model.IgraNaziv?.Trim() ?? string.Empty,
                MinBrojIgraca = model.IgraMinBrojIgraca ?? 1,
                MaxBrojIgraca = model.IgraMaxBrojIgraca ?? 1,
                MinimalnaDob = model.IgraMinimalnaDob ?? 0,
                TrajanjeMins = model.IgraTrajanjeMins ?? 0,
                Zanr = model.IgraZanr ?? ZanrIgre.Zabavna
            };
        }

        _context.Oglasi.Add(oglas);
        await _context.SaveChangesAsync();

        await SpremiSlikeAsync(oglas.Id, model.Slike);

        if (oglas.TipOglasa == TipOglasa.Knjiga)
        {
            return RedirectToAction("Details", "Knjiga", new { id = oglas.Id });
        }
        else
        {
            return RedirectToAction("Details", "DrustvenaIgra", new { id = oglas.Id });
        }
    }

    private void PopuniDropdowne()
    {
        ViewBag.Korisnici = new SelectList(
            _context.Korisnici.OrderBy(k => k.ImeIPrezime),
            "Id", "ImeIPrezime");

        ViewBag.Gradovi = new SelectList(
            _context.Gradovi.OrderBy(g => g.Naziv),
            "Id", "Naziv");
    }

    private async Task SpremiSlikeAsync(int oglasId, List<IFormFile>? slike)
    {
        if (slike == null || slike.Count == 0)
        {
            return;
        }

        var uploadsPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads",
            "oglasi",
            oglasId.ToString());

        Directory.CreateDirectory(uploadsPath);

        var redoslijed = await _context.Slike.CountAsync(s => s.OglasId == oglasId);

        foreach (var slika in slike)
        {
            if (slika == null || slika.Length == 0)
            {
                continue;
            }

            if (!slika.ContentType.StartsWith("image/"))
            {
                continue;
            }

            var ekstenzija = Path.GetExtension(slika.FileName).ToLowerInvariant();

            var dozvoljeneEkstenzije = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

            if (!dozvoljeneEkstenzije.Contains(ekstenzija))
            {
                continue;
            }

            var fileName = Guid.NewGuid().ToString() + ekstenzija;
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await slika.CopyToAsync(stream);
            }

            var novaSlika = new Slika
            {
                OglasId = oglasId,
                Putanja = "/uploads/oglasi/" + oglasId + "/" + fileName,
                FileName = slika.FileName,
                ContentType = slika.ContentType,
                FileSize = slika.Length,
                CreatedAt = DateTime.Now,
                RedoslijedPrikaza = redoslijed
            };

            _context.Slike.Add(novaSlika);
            redoslijed++;
        }

        await _context.SaveChangesAsync();
    }

    [ActionName("Edit")]
    public async Task<IActionResult> EditGet(int id)
    {
        var oglas = await _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
            return NotFound();

        var model = new OglasEditModel
        {
            Id = oglas.Id,
            Naslov = oglas.Naslov,
            Opis = oglas.Opis,
            Cijena = oglas.Cijena,
            DatumIsteka = oglas.DatumIsteka ?? DateTime.Now.AddDays(30),
            TipOglasa = oglas.TipOglasa,
            StanjeArtikla = oglas.StanjeArtikla,
            KorisnikId = oglas.KorisnikId,
            KorisnikIme = oglas.Korisnik.ImeIPrezime,
            GradId = oglas.GradId
        };

        if (oglas.TipOglasa == TipOglasa.Knjiga && oglas.Knjiga != null)
        {
            model.KnjigaNaziv = oglas.Knjiga.Naziv;
            model.KnjigaAutor = oglas.Knjiga.Autor;
            model.KnjigaISBN = oglas.Knjiga.ISBN;
            model.KnjigaIzdavac = oglas.Knjiga.Izdavac;
            model.KnjigaGodinaIzdanja = oglas.Knjiga.GodinaIzdanja;
            model.KnjigaBrojStrana = oglas.Knjiga.BrojStrana;
            model.KnjigaJezik = oglas.Knjiga.Jezik;
            model.KnjigaZanr = oglas.Knjiga.Zanr;
        }
        else if (oglas.TipOglasa == TipOglasa.DrustvenaIgra && oglas.DrustvenaIgra != null)
        {
            model.IgraNaziv = oglas.DrustvenaIgra.Naziv;
            model.IgraMinBrojIgraca = oglas.DrustvenaIgra.MinBrojIgraca;
            model.IgraMaxBrojIgraca = oglas.DrustvenaIgra.MaxBrojIgraca;
            model.IgraMinimalnaDob = oglas.DrustvenaIgra.MinimalnaDob;
            model.IgraTrajanjeMins = oglas.DrustvenaIgra.TrajanjeMins;
            model.IgraZanr = oglas.DrustvenaIgra.Zanr;
        }

        var grad = await _context.Gradovi.FirstOrDefaultAsync(g => g.Id == oglas.GradId);
        ViewBag.GradNaziv = grad?.Naziv ?? "";
        PopuniDropdowne();
        return View(model);
    }

    [HttpPost]
    [ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost(OglasEditModel model)
    {
        if (!ModelState.IsValid)
        {
            model.KorisnikIme = (await _context.Korisnici
                .FirstOrDefaultAsync(k => k.Id == model.KorisnikId))?.ImeIPrezime;
            var grad = await _context.Gradovi.FirstOrDefaultAsync(g => g.Id == model.GradId);
            ViewBag.GradNaziv = grad?.Naziv ?? "";
            PopuniDropdowne();
            return View(model);
        }

        var oglas = await _context.Oglasi
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .FirstOrDefaultAsync(o => o.Id == model.Id);

        if (oglas == null)
            return NotFound();

        oglas.Naslov = model.Naslov.Trim();
        oglas.Opis = model.Opis.Trim();
        oglas.Cijena = model.Cijena;
        oglas.DatumIsteka = model.DatumIsteka;
        oglas.StanjeArtikla = model.StanjeArtikla;
        oglas.GradId = model.GradId;
        oglas.DatumIzmjene = DateTime.Now;

        if (oglas.TipOglasa == TipOglasa.Knjiga && oglas.Knjiga != null)
        {
            oglas.Knjiga.Naziv = model.KnjigaNaziv?.Trim() ?? string.Empty;
            oglas.Knjiga.Autor = model.KnjigaAutor?.Trim() ?? string.Empty;
            oglas.Knjiga.ISBN = model.KnjigaISBN?.Trim() ?? string.Empty;
            oglas.Knjiga.Izdavac = model.KnjigaIzdavac?.Trim() ?? string.Empty;
            oglas.Knjiga.GodinaIzdanja = model.KnjigaGodinaIzdanja ?? 0;
            oglas.Knjiga.BrojStrana = model.KnjigaBrojStrana ?? 0;
            oglas.Knjiga.Jezik = model.KnjigaJezik?.Trim() ?? string.Empty;
            oglas.Knjiga.Zanr = model.KnjigaZanr ?? ZanrKnjige.Drama;
        }
        else if (oglas.TipOglasa == TipOglasa.DrustvenaIgra && oglas.DrustvenaIgra != null)
        {
            oglas.DrustvenaIgra.Naziv = model.IgraNaziv?.Trim() ?? string.Empty;
            oglas.DrustvenaIgra.MinBrojIgraca = model.IgraMinBrojIgraca ?? 1;
            oglas.DrustvenaIgra.MaxBrojIgraca = model.IgraMaxBrojIgraca ?? 1;
            oglas.DrustvenaIgra.MinimalnaDob = model.IgraMinimalnaDob ?? 0;
            oglas.DrustvenaIgra.TrajanjeMins = model.IgraTrajanjeMins ?? 0;
            oglas.DrustvenaIgra.Zanr = model.IgraZanr ?? ZanrIgre.Zabavna;
        }

        await _context.SaveChangesAsync();

        if (oglas.TipOglasa == TipOglasa.Knjiga)
            return RedirectToAction("Details", "Knjiga", new { id = oglas.Id });
        else
            return RedirectToAction("Details", "DrustvenaIgra", new { id = oglas.Id });
    }

    [ActionName("Delete")]
    public async Task<IActionResult> DeleteGet(int id)
    {
        var oglas = await _context.Oglasi
            .Include(o => o.Korisnik)
            .Include(o => o.Grad)
            .Include(o => o.Knjiga)
            .Include(o => o.DrustvenaIgra)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
            return NotFound();

        return View(oglas);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var oglas = await _context.Oglasi
            .FirstOrDefaultAsync(o => o.Id == id);

        if (oglas == null)
            return NotFound();

        oglas.Status = StatusOglasa.Izbrisan;
        await _context.SaveChangesAsync();

        if (oglas.TipOglasa == TipOglasa.Knjiga)
            return RedirectToAction("Index", "Knjiga");
        else
            return RedirectToAction("Index", "DrustvenaIgra");
    }
}