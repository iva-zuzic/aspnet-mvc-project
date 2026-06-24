using System.Net;
using System.Net.Http.Json;
using BookMarketplace.DAL;
using BookMarketplace.Model;
using BookMarketplace.Web.DTOs.Grad;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BookMarketplace.Tests;

public class GradoviApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public GradoviApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetById_ShouldReturnGrad_WhenGradExists()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        dbContext.Gradovi.RemoveRange(dbContext.Gradovi);
        await dbContext.SaveChangesAsync();

        var grad = new Grad
        {
            Naziv = "Test Grad",
            PostanskiBroj = "12345"
        };

        dbContext.Gradovi.Add(grad);
        await dbContext.SaveChangesAsync();

        // Act
        var response = await _client.GetAsync($"/api/gradovi/{grad.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<GradDTO>();

        dto.Should().NotBeNull();
        dto!.Id.Should().Be(grad.Id);
        dto.Naziv.Should().Be("Test Grad");
        dto.PostanskiBroj.Should().Be("12345");
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        await CreateGradAsync();

        var response = await _client.GetAsync("/api/gradovi");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenGradDoesNotExist()
    {
        var response = await _client.GetAsync("/api/gradovi/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldCreateGrad_WhenModelIsValid()
    {
        var newGrad = new
        {
            naziv = "Novi Grad",
            postanskiBroj = "10000"
        };

        var response = await _client.PostAsJsonAsync("/api/gradovi", newGrad);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var invalidGrad = new
        {
            naziv = "",
            postanskiBroj = ""
        };

        var response = await _client.PostAsJsonAsync("/api/gradovi", invalidGrad);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldUpdateGrad_WhenGradExists()
    {
        var grad = await CreateGradAsync();

        var updatedGrad = new
        {
            id = grad.Id,
            naziv = "Izmijenjeni Grad",
            postanskiBroj = "54321"
        };

        var response = await _client.PutAsJsonAsync($"/api/gradovi/{grad.Id}", updatedGrad);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Put_ShouldReturnNotFound_WhenGradDoesNotExist()
    {
        var updatedGrad = new
        {
            id = 99999,
            naziv = "Nepostojeci Grad",
            postanskiBroj = "54321"
        };

        var response = await _client.PutAsJsonAsync("/api/gradovi/99999", updatedGrad);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldDeleteGrad_WhenGradExists()
    {
        var grad = await CreateGradAsync();

        var response = await _client.DeleteAsync($"/api/gradovi/{grad.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenGradDoesNotExist()
    {
        var response = await _client.DeleteAsync("/api/gradovi/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<Grad> CreateGradAsync(string naziv = "Test Grad", string postanskiBroj = "12345")
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();

        var grad = new Grad
        {
            Naziv = naziv,
            PostanskiBroj = postanskiBroj
        };

        dbContext.Gradovi.Add(grad);
        await dbContext.SaveChangesAsync();

        return grad;
    }
}