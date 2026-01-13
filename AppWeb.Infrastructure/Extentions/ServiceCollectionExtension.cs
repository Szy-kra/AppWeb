using AppWeb.Domain.Interfaces;
using AppWeb.Infrastructure.Persistence;
using AppWeb.Infrastructure.Repositories;
using AppWeb.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AppWeb.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            // Tutaj musimy użyć AddDbContext, a nie nazwy samej metody rozszerzającej
            services.AddDbContext<AppWebDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<AppWebDbContext>();


            /* AddScoped rejestruje usługę w kontenerze Dependency Injection z cyklem życia ograniczonym
            do pojedynczego żądania HTTP (HttpRequest), co gwarantuje współdzielenie 
            tej samej instancji obiektu przez wszystkie komponenty w obrębie jednego wątku obsługi */
            services.AddScoped<ICottageRepository, CottageRepository>();

            services.AddScoped<CottageSeeder>();

        }
    }
}