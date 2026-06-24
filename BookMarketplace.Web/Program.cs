using BookMarketplace.DAL;
using BookMarketplace.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BookMarketplaceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookMarketplaceDbContext")));

builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<BookMarketplaceDbContext>();

var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

if (!string.IsNullOrWhiteSpace(googleClientId) &&
    !string.IsNullOrWhiteSpace(googleClientSecret))
{
    builder.Services.AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = googleClientId;
            options.ClientSecret = googleClientSecret;
        });
}

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookMarketplaceDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    string[] roles = { "Admin", "Korisnik" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var adminEmail = "admin@bookmarketplace.local";
    var adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            KorisnickoIme = "admin",
            OIB = "00000000001",
            JMBG = "0000000000001"
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (!result.Succeeded)
        {
            throw new Exception("Admin korisnik nije uspješno kreiran.");
        }
    }
    else
    {
        adminUser.UserName = adminEmail;
        adminUser.Email = adminEmail;
        adminUser.EmailConfirmed = true;

        if (string.IsNullOrWhiteSpace(adminUser.KorisnickoIme))
        {
            adminUser.KorisnickoIme = "admin";
        }

        await userManager.UpdateAsync(adminUser);
    }

    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    var adminProfil = await context.Korisnici
        .FirstOrDefaultAsync(k => k.AppUserId == adminUser.Id || k.Email == adminEmail);

    if (adminProfil == null)
    {
        adminProfil = new Korisnik
        {
            ImeIPrezime = "Administrator",
            KorisnickoIme = "admin",
            Email = adminEmail,
            Lozinka = string.Empty,
            Telefon = string.Empty,
            DatumRegistracije = DateTime.Now,
            Uloga = UlogaKorisnika.Admin,
            AppUserId = adminUser.Id
        };

        context.Korisnici.Add(adminProfil);
    }
    else
    {
        adminProfil.ImeIPrezime = "Administrator";
        adminProfil.KorisnickoIme = "admin";
        adminProfil.Email = adminEmail;
        adminProfil.Uloga = UlogaKorisnika.Admin;
        adminProfil.AppUserId = adminUser.Id;
    }

    await context.SaveChangesAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

public partial class Program { }