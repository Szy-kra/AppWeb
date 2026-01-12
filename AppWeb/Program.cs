using AppWeb.Application.Cottage.Commands.CreateCottage;
using AppWeb.Application.Extensions;
using AppWeb.Infrastructure.Extensions;
using AppWeb.Infrastructure.Seeders;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Podstawowa konfiguracja MVC
builder.Services.AddControllersWithViews();

// 2. Konfiguracja FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCottageCommandValidator>();

// 3. REJESTRACJA MEDIATR (Wersja 14.0.0 dla .NET 8)
// Ta linia sprawia, ¿e MediatR znajdzie wszystkie Twoje Commandy i Handlery
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    // Jeœli Handlery masz w innym projekcie (Application), dodaj te¿ to:
    cfg.RegisterServicesFromAssembly(typeof(AppWeb.Application.Extensions.ServiceCollectionExtension).Assembly);
});

// 4. Konfiguracja Warstw (Infrastructure i Application)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// 5. Seeding bazy danych (Uruchamianie asynchroniczne)
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<CottageSeeder>();
await seeder.Seed();

// 6. Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();