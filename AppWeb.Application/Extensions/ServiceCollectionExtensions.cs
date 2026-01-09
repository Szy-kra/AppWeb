using AppWeb.Application.Mappings;
using AppWeb.Application.Services;
using AppWeb.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace AppWeb.Application.Extensions
{
    public static class ServiceCollectionExtension
    {

        //metoda rozszerzająca w Application 
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICottageServices, CottageServices>();

            services.AddAutoMapper(typeof(CottageMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CottageDtoValidator>();

            services.AddFluentValidationAutoValidation();
        }
    }



}
