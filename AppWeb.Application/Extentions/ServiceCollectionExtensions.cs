using AppWeb.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace AppWeb.Application.Extentions
{
    public static class ServiceCollectionExtension
    {

        //metoda rozszerzająca w Application 
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICottageServices, CottageServices>();
        }
    }



}
