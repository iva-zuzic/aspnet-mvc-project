using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Poruka;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class PorukeApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public PorukeApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreatePorukaAsync();

        var response = await _client.GetAsync("/api/poruke");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnPoruka_WhenPorukaExists()
    {
        var poruka = await CreatePorukaAsync();

        var response = await _client.GetAsync($"/api/poruke/{poruka.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<PorukaDTO>();

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(poruka.Id);
        dto.Sadrzaj.Should().Be("Test poruka sadrzaj");
        dto.Procitano.Should().BeFalse();
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenPorukaDoesNotExist()
    {
        var response = await _client.GetAsync("/api/poruke/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreatePoruka_WhenModelIsValid()
    {
        var (posiljatelj, primatelj, oglas) = await CreateTwoKorisniciAndOglasAsync();

        var newPoruka = new
        {
            posiljateljId = posiljatelj.Id,
            primateljId = primatelj.Id,
            oglasId = oglas.Id,
            sadrzaj = "Nova poruka sadrzaj"
        };

        var response = await _client.PostAsJsonAsync("/api/poruke", newPoruka);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await response.Content.ReadFromJsonAsync<PorukaDTO>();

        dto.Should().NotBeNull();
        dto!.PosiljateljId.Should().Be(posiljatelj.Id);
        dto.PrimateljId.Should().Be(primatelj.Id);
        dto.OglasId.Should().Be(oglas.Id);
        dto.Sadrzaj.Should().Be("Nova poruka sadrzaj");
        dto.Procitano.Should().BeFalse();
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var invalidPoruka = new
        {
            posiljateljId = 0,
            primateljId = 0,
            oglasId = 0,
            sadrzaj = ""
        };

        var response = await _client.PostAsJsonAsync("/api/poruke", invalidPoruka);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdatePoruka_WhenPorukaExists()
    {
        var poruka = await CreatePorukaAsync();

        var updatedPoruka = new
        {
            id = poruka.Id,
            sadrzaj = "Izmijenjen sadrzaj poruke",
            procitano = true
        };

        var response = await _client.PutAsJsonAsync($"/api/poruke/{poruka.Id}", updatedPoruka);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<PorukaDTO>();

        dto.Should().NotBeNull();
        dto!.Sadrzaj.Should().Be("Izmijenjen sadrzaj poruke");
        dto.Procitano.Should().BeTrue();
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenPorukaDoesNotExist()
    {
        var updatedPoruka = new
        {
            id = 99999,
            sadrzaj = "Nepostojeca poruka",
            procitano = false
        };

        var response = await _client.PutAsJsonAsync("/api/poruke/99999", updatedPoruka);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenPorukaExists()
    {
        var poruka = await CreatePorukaAsync();

        var response = await _client.DeleteAsync($"/api/poruke/{poruka.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenPorukaDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/poruke/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<Poruka> CreatePorukaAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var (posiljatelj, primatelj, oglas) = await SeedTwoKorisniciAndOglasAsync(dbContext);

        var poruka = new Poruka
        {
            PosiljateljId = posiljatelj.Id,
            PrimateljId = primatelj.Id,
            OglasId = oglas.Id,
            Sadrzaj = "Test poruka sadrzaj",
            DatumSlanja = DateTime.Now,
            Procitano = false
        };
        dbContext.Poruke.Add(poruka);
        await dbContext.SaveChangesAsync();

        return poruka;
    }

    private async Task<(Korisnik posiljatelj, Korisnik primatelj, Oglas oglas)> CreateTwoKorisniciAndOglasAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        return await SeedTwoKorisniciAndOglasAsync(dbContext);
    }

    private static async Task<(Korisnik posiljatelj, Korisnik primatelj, Oglas oglas)> SeedTwoKorisniciAndOglasAsync(
        BookMarketplaceDbContext dbContext)
    {
        var grad = new Grad { Naziv = "Test Grad", PostanskiBroj = "10000" };
        dbContext.Gradovi.Add(grad);
        await dbContext.SaveChangesAsync();

        var posiljatelj = new Korisnik
        {
            ImeIPrezime = "Posiljatelj Korisnik",
            Email = $"posiljatelj-{Guid.NewGuid()}@example.com",
            Lozinka = "lozinka123",
            Telefon = "0911234567",
            DatumRegistracije = DateTime.Now,
            Uloga = UlogaKorisnika.Korisnik,
            GradId = grad.Id
        };
        dbContext.Korisnici.Add(posiljatelj);
        await dbContext.SaveChangesAsync();

        var primatelj = new Korisnik
        {
            ImeIPrezime = "Primatelj Korisnik",
            Email = $"primatelj-{Guid.NewGuid()}@example.com",
            Lozinka = "lozinka123",
            Telefon = "0921234567",
            DatumRegistracije = DateTime.Now,
            Uloga = UlogaKorisnika.Korisnik,
            GradId = grad.Id
        };
        dbContext.Korisnici.Add(primatelj);
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
            KorisnikId = primatelj.Id,
            GradId = grad.Id
        };
        dbContext.Oglasi.Add(oglas);
        await dbContext.SaveChangesAsync();

        return (posiljatelj, primatelj, oglas);
    }
}
