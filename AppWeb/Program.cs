using AppWeb.Application.Cottage.Commands.CreateCottage;
using AppWeb.Application.Extensions;
using AppWeb.Infrastructure.Extensions;
using AppWeb.Infrastructure.Seeders;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// --- SEKCJA SERVICES (Przed builder.Build) ---
builder.Services.AddControllersWithViews();

// DODAJ TO TUTAJ:
builder.Services.AddRazorPages();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCottageCommandValidator>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AppWeb.Application.Extensions.ServiceCollectionExtension).Assembly);
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// --- BUDOWANIE APLIKACJI ---
var app = builder.Build();

// --- SEKCJA MIDDLEWARE ---
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<CottageSeeder>();
await seeder.Seed();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// KOLEJNOå∆ JEST KLUCZOWA:
app.UseAuthentication(); // 1. Sprawdü kim jest uøytkownik
app.UseAuthorization();  // 2. Sprawdü co moøe robiÊ

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();