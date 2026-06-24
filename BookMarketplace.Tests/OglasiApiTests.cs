using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Oglas;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class OglasiApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public OglasiApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreateOglasAsync();

        var response = await _client.GetAsync("/api/oglasi");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnOglas_WhenOglasExists()
    {
        var oglas = await CreateOglasAsync();

        var response = await _client.GetAsync($"/api/oglasi/{oglas.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<OglasDTO>();

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(oglas.Id);
        dto.Naslov.Should().Be("Test Oglas");
        dto.Status.Should().Be(StatusOglasa.Aktivan.ToString());
        dto.TipOglasa.Should().Be(TipOglasa.Knjiga.ToString());
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenOglasDoesNotExist()
    {
        var response = await _client.GetAsync("/api/oglasi/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreateOglas_WhenModelIsValid()
    {
        var (grad, korisnik) = await CreateGradAndKorisnikAsync();

        var newOglas = new
        {
            naslov = "Novi Oglas",
            opis = "Opis oglasa koji je dovoljno dugacak",
            cijena = 50.00m,
            datumIsteka = DateTime.Now.AddDays(30),
            korisnikId = korisnik.Id,
            gradId = grad.Id,
            tipOglasa = (int)TipOglasa.Knjiga,
            stanjeArtikla = (int)StanjeArtikla.Novo,
            knjigaNaziv = "Nova Knjiga",
            knjigaAutor = "Novi Autor",
            knjigaISBN = "978-3-16-148410-0",
            knjigaIzdavac = "Novi Izdavac",
            knjigaGodinaIzdanja = 2022,
            knjigaBrojStrana = 250,
            knjigaJezik = "Hrvatski",
            knjigaZanr = (int)ZanrKnjige.Fantastika
        };

        var response = await _client.PostAsJsonAsync("/api/oglasi", newOglas);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await response.Content.ReadFromJsonAsync<OglasDTO>();

        dto.Should().NotBeNull();
        dto!.Naslov.Should().Be("Novi Oglas");
        dto.Status.Should().Be(StatusOglasa.Aktivan.ToString());
        dto.TipOglasa.Should().Be(TipOglasa.Knjiga.ToString());
        dto.Knjiga.Should().NotBeNull();
        dto.Knjiga!.Naziv.Should().Be("Nova Knjiga");
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var invalidOglas = new
        {
            naslov = "",
            opis = "",
            cijena = 0m,
            korisnikId = 0,
            gradId = 0,
            tipOglasa = 0,
            stanjeArtikla = 0
        };

        var response = await _client.PostAsJsonAsync("/api/oglasi", invalidOglas);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdateOglas_WhenOglasExists()
    {
        var oglas = await CreateOglasAsync();

        var updatedOglas = new
        {
            id = oglas.Id,
            naslov = "Izmijenjeni Oglas",
            opis = "Izmijenjeni opis koji je dovoljno dugacak",
            cijena = 75.00m,
            datumIsteka = DateTime.Now.AddDays(60),
            gradId = oglas.GradId,
            stanjeArtikla = (int)StanjeArtikla.KaoNovo,
            knjigaNaziv = "Izmijenjena Knjiga",
            knjigaAutor = "Izmijenjeni Autor",
            knjigaISBN = "978-3-16-148410-0",
            knjigaIzdavac = "Izmijenjeni Izdavac",
            knjigaGodinaIzdanja = 2021,
            knjigaBrojStrana = 350,
            knjigaJezik = "Engleski",
            knjigaZanr = (int)ZanrKnjige.Krimi
        };

        var response = await _client.PutAsJsonAsync($"/api/oglasi/{oglas.Id}", updatedOglas);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<OglasDTO>();

        dto.Should().NotBeNull();
        dto!.Naslov.Should().Be("Izmijenjeni Oglas");
        dto.StanjeArtikla.Should().Be(StanjeArtikla.KaoNovo.ToString());
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenOglasDoesNotExist()
    {
        var (grad, _) = await CreateGradAndKorisnikAsync();

        var updatedOglas = new
        {
            id = 99999,
            naslov = "Nepostojeci Oglas",
            opis = "Opis nepostojeceg oglasa koji je dugacak",
            cijena = 10.00m,
            gradId = grad.Id,
            stanjeArtikla = (int)StanjeArtikla.Dobro
        };

        var response = await _client.PutAsJsonAsync("/api/oglasi/99999", updatedOglas);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenOglasExists()
    {
        var oglas = await CreateOglasAsync();

        var response = await _client.DeleteAsync($"/api/oglasi/{oglas.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenOglasDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/oglasi/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<Oglas> CreateOglasAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var grad = new Grad { Naziv = "Test Grad", PostanskiBroj = "10000" };
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

        return oglas;
    }

    private async Task<(Grad grad, Korisnik korisnik)> CreateGradAndKorisnikAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var grad = new Grad { Naziv = "Test Grad", PostanskiBroj = "10000" };
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

        return (grad, korisnik);
    }
}
