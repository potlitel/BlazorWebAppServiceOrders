using FSA.Cache;
using FSA.Core.Dtos;
using FSA.Management.Application.DependencyContainers;
using FSA.Razor.Components.Extensions;
using FSA.Razor.Components.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.JSInterop;
using Radzen;
using RazorClassLibrary1.Services;
using System.Globalization;
using System.Text.RegularExpressions;

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
            services.AddManagementHttpServices();
            services.AddManagementHttpAuthServices();

            ////Cache
            services.AddFSACacheServices();
            //Components
            services.AddFSARadzenComponentsServices();
            //services.AddFSALocalizationServices(configuration);
            services.AddFSA_ServiceOrders_LocalizationServices(configuration);
            return services;
        }

        /// <summary>
        /// Method <see cref="AddFSASOCustomComponentsService"/>:
        /// </summary>
        /// <param name="services">The IServiceCollection instance</param>
        /// <param name="configuration">The IConfiguration isntance</param>
        /// <returns>A service instance of type <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddFSASOCustomComponentsService(this IServiceCollection services)
        {
            //services.AddScoped<IFSACustomNotificationService, FSACustomNotificationService>();
            ////services.AddScoped<IFSACustomDialogService, FSACustomDialogService>();
            //services.AddScoped<IFSAThemeService, FSAAThemeService>();

            services.AddScoped<IFSASOCustomDialogService, FSASOCustomDialogService>();
            services.AddScoped<IFSASOThemeService, FSAAThemeService>();
            services.AddScoped<IGitHubService, GitHubService>();
            services.AddScoped<IMyService, MyService>();

            //services.AddRadzenComponents();

            return services;
        }

        public static IServiceCollection AddFSA_ServiceOrders_LocalizationServices(this IServiceCollection services, IConfiguration configuration)
        {
			#region ToDelete
			//services.AddLocalization();

			//var culture = configuration["FSASOManagement:App:Culture"];

			//var cultureConf = new CultureInfo(culture!)
			//{
			//    NumberFormat = new NumberFormatInfo()
			//    {
			//        NumberDecimalDigits = 0,
			//        NumberDecimalSeparator = "."
			//    }
			//};
			//CultureInfo.DefaultThreadCurrentCulture = cultureConf;
			//CultureInfo.DefaultThreadCurrentUICulture = cultureConf;

			//return services;
			#endregion

			services.AddLocalization();
            //CultureInfo obj = new CultureInfo(configuration["FSAManagement:App:Culture"])
            //{
            //    NumberFormat = new NumberFormatInfo
            //    {
            //        NumberDecimalDigits = 0,
            //        NumberDecimalSeparator = "."
            //    }
            //};

            //if (obj != null)
            //{
            //    CultureInfo.DefaultThreadCurrentCulture = obj;
            //    CultureInfo.DefaultThreadCurrentUICulture = obj;
            //}
            //else
            //{
            //    //var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            //    //var result = await jsInterop.InvokeAsync<string>("blazorCulture.get");
            //    obj = new CultureInfo("pt");
            //}


            return services;
        }
    }
}
