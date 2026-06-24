using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Favorit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class FavoritiApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public FavoritiApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreateFavoritAsync();

        var response = await _client.GetAsync("/api/favoriti");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnFavorit_WhenFavoritExists()
    {
        var favorit = await CreateFavoritAsync();

        var response = await _client.GetAsync($"/api/favoriti/{favorit.KorisnikId}/{favorit.OglasId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<FavoritDTO>();

        dto.Should().NotBeNull();
        dto!.KorisnikId.Should().Be(favorit.KorisnikId);
        dto.OglasId.Should().Be(favorit.OglasId);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenFavoritDoesNotExist()
    {
        var response = await _client.GetAsync("/api/favoriti/99999/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreateFavorit_WhenModelIsValid()
    {
        var (korisnik, oglas) = await CreateKorisnikAndOglasAsync();

        var newFavorit = new
        {
            korisnikId = korisnik.Id,
            oglasId = oglas.Id
        };

        var response = await _client.PostAsJsonAsync("/api/favoriti", newFavorit);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await response.Content.ReadFromJsonAsync<FavoritDTO>();

        dto.Should().NotBeNull();
        dto!.KorisnikId.Should().Be(korisnik.Id);
        dto.OglasId.Should().Be(oglas.Id);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenKorisnikDoesNotExist()
    {
        var invalidFavorit = new
        {
            korisnikId = 0,
            oglasId = 0
        };

        var response = await _client.PostAsJsonAsync("/api/favoriti", invalidFavorit);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdateFavorit_WhenFavoritExists()
    {
        var favorit = await CreateFavoritAsync();
        var newDate = new DateTime(2025, 1, 1);

        var updatedFavorit = new
        {
            korisnikId = favorit.KorisnikId,
            oglasId = favorit.OglasId,
            datumDodavanja = newDate
        };

        var response = await _client.PutAsJsonAsync(
            $"/api/favoriti/{favorit.KorisnikId}/{favorit.OglasId}",
            updatedFavorit);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<FavoritDTO>();

        dto.Should().NotBeNull();
        dto!.KorisnikId.Should().Be(favorit.KorisnikId);
        dto.OglasId.Should().Be(favorit.OglasId);
        dto.DatumDodavanja.Should().Be(newDate);
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenFavoritDoesNotExist()
    {
        var updatedFavorit = new
        {
            korisnikId = 99999,
            oglasId = 99999,
            datumDodavanja = DateTime.Now
        };

        var response = await _client.PutAsJsonAsync("/api/favoriti/99999/99999", updatedFavorit);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenFavoritExists()
    {
        var favorit = await CreateFavoritAsync();

        var response = await _client.DeleteAsync($"/api/favoriti/{favorit.KorisnikId}/{favorit.OglasId}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenFavoritDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/favoriti/99999/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<Favorit> CreateFavoritAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var (korisnik, oglas) = await SeedKorisnikAndOglasAsync(dbContext);

        var favorit = new Favorit
        {
            KorisnikId = korisnik.Id,
            OglasId = oglas.Id,
            DatumDodavanja = DateTime.Now
        };
        dbContext.Favoriti.Add(favorit);
        await dbContext.SaveChangesAsync();

        return favorit;
    }

    private async Task<(Korisnik korisnik, Oglas oglas)> CreateKorisnikAndOglasAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        return await SeedKorisnikAndOglasAsync(dbContext);
    }

    private static async Task<(Korisnik korisnik, Oglas oglas)> SeedKorisnikAndOglasAsync(
        BookMarketplaceDbContext dbContext)
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

        return (korisnik, oglas);
    }
}
