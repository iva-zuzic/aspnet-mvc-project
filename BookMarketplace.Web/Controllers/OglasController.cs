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
            DatumObjave = DateTime.Today
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
            DatumObjave = model.DatumObjave,
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
                MinimalnasDob = model.IgraMinimalnaDob ?? 0,
                TrajanjeMins = model.IgraTrajanjeMins ?? 0,
                Zanr = model.IgraZanr ?? ZanrIgre.Zabavna
            };
        }

        _context.Oglasi.Add(oglas);
        await _context.SaveChangesAsync();

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
}