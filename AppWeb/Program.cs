
using AppWeb.Application.Extentions; // Odwo³anie do metody AddApplication w pliku AppWeb.Application/Extensions/ServiceCollectionExtensions.cs
using AppWeb.Infrastructure.Extentions;// Odwo³anie do metody AddInfrastructure w pliku AppWeb.Infrastructure/Extensions/ServiceCollectionExtension.cs
using AppWeb.Infrastructure.Seeders;// Odwo³anie do klasy CottageSeeder w pliku AppWeb.Infrastructure/Seeders/CottageSeeder.cs

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Wywo³uje metodê zdefiniowan¹ w AppWeb.Infrastructure/Extensions/ServiceCollectionExtension.cs, która konfiguruje bazê w AppWeb.Infrastructure/Persistence/AppWebDbv2Context.cs
builder.Services.AddInfrastructure(builder.Configuration);

// Wywo³uje metodê zdefiniowan¹ w AppWeb.Application/Extentions/ServiceCollectionExtensions.cs, która rejestruje serwisy w AppWeb.Application/Services/
builder.Services.AddApplication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Pobiera zarejestrowan¹ w³asn¹ klasê z folderu AppWeb.Infrastructure/Seeders/
    var seeder = scope.ServiceProvider.GetRequiredService<CottageSeeder>();
    // Wykonuje logikê zapisu do tabeli Cottages zdefiniowanej w AppWeb.Domain/Entities/Cottage.cs
    await seeder.Seed();
}

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