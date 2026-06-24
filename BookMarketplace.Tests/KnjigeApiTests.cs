using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Knjiga;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class KnjigeApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public KnjigeApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreateKnjigaAsync();

        var response = await _client.GetAsync("/api/knjige");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnKnjiga_WhenKnjigaExists()
    {
        var knjiga = await CreateKnjigaAsync();

        var response = await _client.GetAsync($"/api/knjige/{knjiga.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<KnjigaDTO>();

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(knjiga.Id);
        dto.Naziv.Should().Be("Test Knjiga");
        dto.Autor.Should().Be("Test Autor");
        dto.ISBN.Should().Be("978-3-16-148410-0");
        dto.Izdavac.Should().Be("Test Izdavac");
        dto.GodinaIzdanja.Should().Be(2020);
        dto.BrojStrana.Should().Be(300);
        dto.Jezik.Should().Be("Hrvatski");
        dto.Zanr.Should().Be(ZanrKnjige.Fantastika.ToString());
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenKnjigaDoesNotExist()
    {
        var response = await _client.GetAsync("/api/knjige/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreateKnjiga_WhenModelIsValid()
    {
        var oglas = await CreateOglasWithoutKnjigaAsync();

        var newKnjiga = new
        {
            oglasId = oglas.Id,
            naziv = "Nova Knjiga",
            autor = "Novi Autor",
            isbn = "978-0-13-468599-1",
            izdavac = "Novi Izdavac",
            godinaIzdanja = 2023,
            brojStrana = 450,
            jezik = "Engleski",
            zanr = (int)ZanrKnjige.Triler
        };

        var response = await _client.PostAsJsonAsync("/api/knjige", newKnjiga);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await response.Content.ReadFromJsonAsync<KnjigaDTO>();

        dto.Should().NotBeNull();
        dto!.Naziv.Should().Be("Nova Knjiga");
        dto.Autor.Should().Be("Novi Autor");
        dto.OglasId.Should().Be(oglas.Id);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var invalidKnjiga = new
        {
            oglasId = 0,
            naziv = "",
            autor = "",
            isbn = "",
            izdavac = "",
            godinaIzdanja = 0,
            brojStrana = 0,
            jezik = "",
            zanr = 0
        };

        var response = await _client.PostAsJsonAsync("/api/knjige", invalidKnjiga);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdateKnjiga_WhenKnjigaExists()
    {
        var knjiga = await CreateKnjigaAsync();

        var updatedKnjiga = new
        {
            id = knjiga.Id,
            naziv = "Izmijenjena Knjiga",
            autor = "Izmijenjeni Autor",
            isbn = "978-3-16-148410-0",
            izdavac = "Izmijenjeni Izdavac",
            godinaIzdanja = 2021,
            brojStrana = 350,
            jezik = "Engleski",
            zanr = (int)ZanrKnjige.Krimi
        };

        var response = await _client.PutAsJsonAsync($"/api/knjige/{knjiga.Id}", updatedKnjiga);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<KnjigaDTO>();

        dto.Should().NotBeNull();
        dto!.Naziv.Should().Be("Izmijenjena Knjiga");
        dto.Autor.Should().Be("Izmijenjeni Autor");
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenKnjigaDoesNotExist()
    {
        var updatedKnjiga = new
        {
            id = 99999,
            naziv = "Nepostojeca Knjiga",
            autor = "Nepostojeci Autor",
            isbn = "978-3-16-148410-0",
            izdavac = "Nepostojeci Izdavac",
            godinaIzdanja = 2021,
            brojStrana = 350,
            jezik = "Hrvatski",
            zanr = (int)ZanrKnjige.Drama
        };

        var response = await _client.PutAsJsonAsync("/api/knjige/99999", updatedKnjiga);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenKnjigaExists()
    {
        var knjiga = await CreateKnjigaAsync();

        var response = await _client.DeleteAsync($"/api/knjige/{knjiga.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenKnjigaDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/knjige/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<Knjiga> CreateKnjigaAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var grad = new Grad
        {
            Naziv = "Test Grad",
            PostanskiBroj = "10000"
        };
        dbContext.Gradovi.Add(grad);
        await dbContext.SaveChangesAsync();

        var korisnik = new Korisnik
        {
            ImeIPrezime = "Test Korisnik",
            Email = $"test-{Guid.NewGuid()}@example.com",
            Lozinka = "lozinka123",
            Telefon = "0911234567",
            DatumRegistracije = DateTime.Now,
            Uloga = UlogaKorisnika.Korisnik,
            GradId = grad.Id
        };
        dbContext.Korisnici.Add(korisnik);
        await dbContext.SaveChangesAsync();

        var oglas = new Oglas
        {
            Naslov = "Test Oglas",
            Opis = "Opis oglasa",
            Cijena = 100.00m,
            DatumObjave = DateTime.Now,
            Status = StatusOglasa.Aktivan,
            TipOglasa = TipOglasa.Knjiga,
            StanjeArtikla = StanjeArtikla.Novo,
            KorisnikId = korisnik.Id,
            GradId = grad.Id
        };
        dbContext.Oglasi.Add(oglas);
        await dbContext.SaveChangesAsync();

        var knjiga = new Knjiga
        {
            OglasId = oglas.Id,
            Naziv = "Test Knjiga",
            Autor = "Test Autor",
            ISBN = "978-3-16-148410-0",
            Izdavac = "Test Izdavac",
            GodinaIzdanja = 2020,
            BrojStrana = 300,
            Jezik = "Hrvatski",
            Zanr = ZanrKnjige.Fantastika
        };
        dbContext.Knjige.Add(knjiga);
        await dbContext.SaveChangesAsync();

        return knjiga;
    }

    private async Task<Oglas> CreateOglasWithoutKnjigaAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var grad = new Grad
        {
            Naziv = "Test Grad",
            PostanskiBroj = "10000"
        };
        dbContext.Gradovi.Add(grad);
        await dbContext.SaveChangesAsync();

        var korisnik = new Korisnik
        {
            ImeIPrezime = "Test Korisnik",
            Email = $"test-{Guid.NewGuid()}@example.com",
            Lozinka = "lozinka123",
            Telefon = "0911234567",
            DatumRegistracije = DateTime.Now,
            Uloga = UlogaKorisnika.Korisnik,
            GradId = grad.Id
        };
        dbContext.Korisnici.Add(korisnik);
        await dbContext.SaveChangesAsync();

        var oglas = new Oglas
        {
            Naslov = "Test Oglas Bez Knjige",
            Opis = "Opis oglasa",
            Cijena = 50.00m,
            DatumObjave = DateTime.Now,
            Status = StatusOglasa.Aktivan,
            TipOglasa = TipOglasa.Knjiga,
            StanjeArtikla = StanjeArtikla.Dobro,
            KorisnikId = korisnik.Id,
            GradId = grad.Id
        };
        dbContext.Oglasi.Add(oglas);
        await dbContext.SaveChangesAsync();

        return oglas;
    }
}
