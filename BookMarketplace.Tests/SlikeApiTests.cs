using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Slika;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class SlikeApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public SlikeApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreateSlikaAsync();

        var response = await _client.GetAsync("/api/slike");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnSlika_WhenSlikaExists()
    {
        var slika = await CreateSlikaAsync();

        var response = await _client.GetAsync($"/api/slike/{slika.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<SlikaDTO>();

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(slika.Id);
        dto.OglasId.Should().Be(slika.OglasId);
        dto.Putanja.Should().Be("/images/test.jpg");
        dto.RedoslijedPrikaza.Should().Be(1);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenSlikaDoesNotExist()
    {
        var response = await _client.GetAsync("/api/slike/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreateSlika_WhenModelIsValid()
    {
        var oglas = await CreateOglasAsync();

        var newSlika = new
        {
            oglasId = oglas.Id,
            putanja = "/images/nova-slika.jpg",
            redoslijedPrikaza = 1
        };

        var response = await _client.PostAsJsonAsync("/api/slike", newSlika);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await response.Content.ReadFromJsonAsync<SlikaDTO>();

        dto.Should().NotBeNull();
        dto!.OglasId.Should().Be(oglas.Id);
        dto.Putanja.Should().Be("/images/nova-slika.jpg");
        dto.RedoslijedPrikaza.Should().Be(1);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var invalidSlika = new
        {
            oglasId = 0,
            putanja = "",
            redoslijedPrikaza = 0
        };

        var response = await _client.PostAsJsonAsync("/api/slike", invalidSlika);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdateSlika_WhenSlikaExists()
    {
        var slika = await CreateSlikaAsync();

        var updatedSlika = new
        {
            id = slika.Id,
            putanja = "/images/izmijenjena-slika.jpg",
            redoslijedPrikaza = 2
        };

        var response = await _client.PutAsJsonAsync($"/api/slike/{slika.Id}", updatedSlika);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<SlikaDTO>();

        dto.Should().NotBeNull();
        dto!.Putanja.Should().Be("/images/izmijenjena-slika.jpg");
        dto.RedoslijedPrikaza.Should().Be(2);
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenSlikaDoesNotExist()
    {
        var updatedSlika = new
        {
            id = 99999,
            putanja = "/images/nepostojeca.jpg",
            redoslijedPrikaza = 1
        };

        var response = await _client.PutAsJsonAsync("/api/slike/99999", updatedSlika);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenSlikaExists()
    {
        var slika = await CreateSlikaAsync();

        var response = await _client.DeleteAsync($"/api/slike/{slika.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenSlikaDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/slike/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<Slika> CreateSlikaAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var oglas = await SeedOglasAsync(dbContext);

        var slika = new Slika
        {
            OglasId = oglas.Id,
            Putanja = "/images/test.jpg",
            RedoslijedPrikaza = 1
        };
        dbContext.Slike.Add(slika);
        await dbContext.SaveChangesAsync();

        return slika;
    }

    private async Task<Oglas> CreateOglasAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        return await SeedOglasAsync(dbContext);
    }

    private static async Task<Oglas> SeedOglasAsync(BookMarketplaceDbContext dbContext)
    {
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
}
