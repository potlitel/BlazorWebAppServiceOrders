using FSA.Cache;
using FSA.Management.Application.DependencyContainers;
using FSA.Razor.Components.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RazorClassLibrary1.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Method <see cref="ConfigureSOComponents"/>: Extension method to expose the configuration of the visual components (UI) of service orders.
        /// </summary>
        /// <param name="services">The IServiceCollection instance</param>
        /// <param name="configuration">The IConfiguration isntance</param>
        /// <returns>A service instance of type <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureSOComponents(this IServiceCollection services, IConfiguration configuration)
        {
            //Management.Application
            services.AddManagementApplication(configuration);
            //services.AddManagementHttpServices();
            services.AddManagementHttpAuthServices();

            ////Cache
            services.AddFSACacheServices();
            //Components
            services.AddFSARadzenComponentsServices();
            services.AddFSALocalizationServices(configuration);
            services.AddManagementApplication(configuration);
            return services;
        }
    }
}
