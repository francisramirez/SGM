
using Microsoft.Extensions.DependencyInjection;
using SGM.Application.Contracts.Repositories.Insurance;
using SGM.Application.Contracts.Services.insuranse;
using SGM.Application.Services.Insurance;
using SGM.Persistence.Repositories;

namespace SGM.IOC.Dependencies.Insurance
{
    public static class InsuranceDependency
    {
        public static void AddInsuranceDependency(this IServiceCollection services) 
        {
            services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();
            services.AddTransient<INetworkTypeService, NetworkTypeService>();


            services.AddScoped<IInsuranceProviderRepository, InsuranceProviderRepository>();
            services.AddTransient<IInsuranceProviderService, InsuranceProviderService>();


        }
    }
}
