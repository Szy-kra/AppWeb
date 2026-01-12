using AppWeb.Application.Cottage.Commands.CreateCottage;
using AppWeb.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppWeb.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // 1. REJESTRACJA MEDIATR (To zastępuje Twoje serwisy)
            // Ta linia automatycznie znajdzie wszystkie Twoje Handlery (GetAll, GetForOne itd.)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCottageCommand).Assembly));

            // 2. AUTOMAPPER
            services.AddAutoMapper(typeof(CottageMappingProfile));

            // 3. VALIDATORS
            services.AddValidatorsFromAssemblyContaining<CreateCottageCommandValidator>();

            // 4. FLUENT VALIDATION
            services.AddFluentValidationAutoValidation();


        }
    }
}