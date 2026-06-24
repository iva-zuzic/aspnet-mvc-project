using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.DrustvenaIgra;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class DrustveneIgreApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DrustveneIgreApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreateIgraAsync();

        var response = await _client.GetAsync("/api/drustvene-igre");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnIgra_WhenIgraExists()
    {
        var igra = await CreateIgraAsync();

        var response = await _client.GetAsync($"/api/drustvene-igre/{igra.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<DrustvenaIgraDTO>();

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(igra.Id);
        dto.Naziv.Should().Be("Test Igra");
        dto.MinBrojIgraca.Should().Be(2);
        dto.MaxBrojIgraca.Should().Be(6);
        dto.MinimalnaDob.Should().Be(8);
        dto.TrajanjeMins.Should().Be(60);
        dto.Zanr.Should().Be(ZanrIgre.Strategija.ToString());
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenIgraDoesNotExist()
    {
        var response = await _client.GetAsync("/api/drustvene-igre/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreateIgra_WhenModelIsValid()
    {
        var oglas = await CreateOglasWithoutIgraAsync();

        var newIgra = new
        {
            oglasId = oglas.Id,
            naziv = "Nova Igra",
            minBrojIgraca = 2,
            maxBrojIgraca = 4,
            minimalnaDob = 10,
            trajanjeMins = 45,
            zanr = (int)ZanrIgre.Kooperativna
        };

        var response = await _client.PostAsJsonAsync("/api/drustvene-igre", newIgra);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dto = await response.Content.ReadFromJsonAsync<DrustvenaIgraDTO>();

        dto.Should().NotBeNull();
        dto!.Naziv.Should().Be("Nova Igra");
        dto.MinBrojIgraca.Should().Be(2);
        dto.MaxBrojIgraca.Should().Be(4);
        dto.OglasId.Should().Be(oglas.Id);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var invalidIgra = new
        {
            oglasId = 0,
            naziv = "",
            minBrojIgraca = 0,
            maxBrojIgraca = 0,
            minimalnaDob = 0,
            trajanjeMins = 0,
            zanr = 0
        };

        var response = await _client.PostAsJsonAsync("/api/drustvene-igre", invalidIgra);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdateIgra_WhenIgraExists()
    {
        var igra = await CreateIgraAsync();

        var updatedIgra = new
        {
            id = igra.Id,
            naziv = "Izmijenjena Igra",
            minBrojIgraca = 3,
            maxBrojIgraca = 5,
            minimalnaDob = 12,
            trajanjeMins = 90,
            zanr = (int)ZanrIgre.Pustolovinska
        };

        var response = await _client.PutAsJsonAsync($"/api/drustvene-igre/{igra.Id}", updatedIgra);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<DrustvenaIgraDTO>();

        dto.Should().NotBeNull();
        dto!.Naziv.Should().Be("Izmijenjena Igra");
        dto.MinBrojIgraca.Should().Be(3);
        dto.MaxBrojIgraca.Should().Be(5);
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenIgraDoesNotExist()
    {
        var updatedIgra = new
        {
            id = 99999,
            naziv = "Nepostojeca Igra",
            minBrojIgraca = 2,
            maxBrojIgraca = 4,
            minimalnaDob = 8,
            trajanjeMins = 30,
            zanr = (int)ZanrIgre.Apstraktna
        };

        var response = await _client.PutAsJsonAsync("/api/drustvene-igre/99999", updatedIgra);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenIgraExists()
    {
        var igra = await CreateIgraAsync();

        var response = await _client.DeleteAsync($"/api/drustvene-igre/{igra.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenIgraDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/drustvene-igre/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<DrustvenaIgra> CreateIgraAsync()
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
            Cijena = 80.00m,
            DatumObjave = DateTime.Now,
            Status = StatusOglasa.Aktivan,
            TipOglasa = TipOglasa.DrustvenaIgra,
            StanjeArtikla = StanjeArtikla.KaoNovo,
            KorisnikId = korisnik.Id,
            GradId = grad.Id
        };
        dbContext.Oglasi.Add(oglas);
        await dbContext.SaveChangesAsync();

        var igra = new DrustvenaIgra
        {
            OglasId = oglas.Id,
            Naziv = "Test Igra",
            MinBrojIgraca = 2,
            MaxBrojIgraca = 6,
            MinimalnaDob = 8,
            TrajanjeMins = 60,
            Zanr = ZanrIgre.Strategija
        };
        dbContext.DrustveneIgre.Add(igra);
        await dbContext.SaveChangesAsync();

        return igra;
    }

    private async Task<Oglas> CreateOglasWithoutIgraAsync()
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
            Naslov = "Test Oglas Bez Igre",
            Opis = "Opis oglasa",
            Cijena = 40.00m,
            DatumObjave = DateTime.Now,
            Status = StatusOglasa.Aktivan,
            TipOglasa = TipOglasa.DrustvenaIgra,
            StanjeArtikla = StanjeArtikla.Dobro,
            KorisnikId = korisnik.Id,
            GradId = grad.Id
        };
        dbContext.Oglasi.Add(oglas);
        await dbContext.SaveChangesAsync();

        return oglas;
    }
}
