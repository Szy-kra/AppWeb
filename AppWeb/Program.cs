using AppWeb.Application.Extensions;
using AppWeb.Application.Validators;
using AppWeb.Infrastructure.Extensions;
using AppWeb.Infrastructure.Seeders;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Podstawowa konfiguracja MVC
builder.Services.AddControllersWithViews();

// 2. Konfiguracja FluentValidation (POPRAWIONA)
builder.Services.AddFluentValidationAutoValidation(); 
// PONI¯SZA LINIA JEST KLUCZOWA: bez niej JS nie widzi regu³ z walidatora
builder.Services.AddFluentValidationClientsideAdapters(); 
builder.Services.AddValidatorsFromAssemblyContaining<CottageDtoValidator>();

// 3. Konfiguracja Warstw (Infrastructure i Application)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// 4. Seeding bazy danych
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CottageSeeder>();
    await seeder.Seed();
}

// 5. Middleware (Potok przetwarzania)
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