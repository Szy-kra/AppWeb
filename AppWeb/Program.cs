using AppWeb.Infrastructure.Extentions;

namespace AppWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Dodanie obs³ugi kontrolerów i widoków (MVC)
            builder.Services.AddControllersWithViews();


            // Rejestracja AppWebDbContext z u¿yciem Connection Stringa z appsettings.json
            builder.Services.AddInfrastructure(builder.Configuration);


            var app = builder.Build();

            // Konfiguracja potoku ¿¹dañ HTTP
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
        }
    }
}