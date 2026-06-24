using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Korisnik;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class KorisniciApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public KorisniciApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreateKorisnikAsync();

        var response = await _client.GetAsync("/api/korisnici");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnKorisnik_WhenKorisnikExists()
    {
        var korisnik = await CreateKorisnikAsync();

        var response = await _client.GetAsync($"/api/korisnici/{korisnik.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<KorisnikDTO>();

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(korisnik.Id);
        dto.ImeIPrezime.Should().Be("Test Korisnik");
        dto.Telefon.Should().Be("0911234567");
        dto.Uloga.Should().Be(UlogaKorisnika.Korisnik.ToString());
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenKorisnikDoesNotExist()
    {
        var response = await _client.GetAsync("/api/korisnici/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreateKorisnik_WhenModelIsValid()
    {
        var newKorisnik = new
        {
            imeIPrezime = "Novi Korisnik",
            email = $"novi-{Guid.NewGuid()}@example.com",
            telefon = "0921234567",
            lozinka = "sigurnaLozinka123",
            uloga = (int)UlogaKorisnika.Korisnik
        };

        var response = await _client.PostAsJsonAsync("/api/korisnici", newKorisnik);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await response.Content.ReadFromJsonAsync<KorisnikDTO>();

        dto.Should().NotBeNull();
        dto!.ImeIPrezime.Should().Be("Novi Korisnik");
        dto.Uloga.Should().Be(UlogaKorisnika.Korisnik.ToString());
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var invalidKorisnik = new
        {
            imeIPrezime = "",
            email = "nije-email",
            telefon = "",
            lozinka = "k",
            uloga = 0
        };

        var response = await _client.PostAsJsonAsync("/api/korisnici", invalidKorisnik);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdateKorisnik_WhenKorisnikExists()
    {
        var korisnik = await CreateKorisnikAsync();

        var updatedKorisnik = new
        {
            id = korisnik.Id,
            imeIPrezime = "Izmijenjeni Korisnik",
            email = $"izmijenjeni-{Guid.NewGuid()}@example.com",
            telefon = "0931234567",
            uloga = (int)UlogaKorisnika.Admin
        };

        var response = await _client.PutAsJsonAsync($"/api/korisnici/{korisnik.Id}", updatedKorisnik);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<KorisnikDTO>();

        dto.Should().NotBeNull();
        dto!.ImeIPrezime.Should().Be("Izmijenjeni Korisnik");
        dto.Uloga.Should().Be(UlogaKorisnika.Admin.ToString());
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenKorisnikDoesNotExist()
    {
        var updatedKorisnik = new
        {
            id = 99999,
            imeIPrezime = "Nepostojeci Korisnik",
            email = "nepostojeci@example.com",
            telefon = "0991234567",
            uloga = (int)UlogaKorisnika.Korisnik
        };

        var response = await _client.PutAsJsonAsync("/api/korisnici/99999", updatedKorisnik);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenKorisnikExists()
    {
        var korisnik = await CreateKorisnikAsync();

        var response = await _client.DeleteAsync($"/api/korisnici/{korisnik.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenKorisnikDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/korisnici/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<Korisnik> CreateKorisnikAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var korisnik = new Korisnik
        {
            ImeIPrezime = "Test Korisnik",
            Email = $"test-{Guid.NewGuid()}@example.com",
            Lozinka = "lozinka123",
            Telefon = "0911234567",
            DatumRegistracije = DateTime.Now,
            Uloga = UlogaKorisnika.Korisnik
        };

        dbContext.Korisnici.Add(korisnik);
        await dbContext.SaveChangesAsync();

        return korisnik;
    }
}
