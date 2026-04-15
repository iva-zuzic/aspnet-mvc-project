using BookMarketplace.MockRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<KnjigaMockRepository>();
builder.Services.AddSingleton<DrustvenaIgraMockRepository>();
builder.Services.AddSingleton<GradMockRepository>();
builder.Services.AddSingleton<OglasMockRepository>();
builder.Services.AddSingleton<KorisnikMockRepository>();
builder.Services.AddSingleton<PorukaMockRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
