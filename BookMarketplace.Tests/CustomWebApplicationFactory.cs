using BookMarketplace.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BookMarketplace.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<BookMarketplaceDbContext>>();

            services.AddDbContext<BookMarketplaceDbContext>(options =>
            {
                options.UseInMemoryDatabase("BookMarketplaceTestDb");
            });
        });
    }
}