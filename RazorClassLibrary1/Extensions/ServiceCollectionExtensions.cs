using FSA.Cache;
using FSA.Management.Application.DependencyContainers;
using FSA.Razor.Components.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrary1.Services;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.Create;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetById;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.Update;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.Create;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetAllBySOId;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetCurrent;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.Create;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetByExecutor;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetById;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetByOwner;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.Update;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.GetAllBySOId;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.Create;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.GetById;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.Update;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.Create;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.GetById;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.Update;
using RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.Create;
using RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.GetById;
using RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.Update;

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
            services.AddScoped<IFSASOCustomDialogService, FSASOCustomDialogService>();
            services.AddScoped<IFSASOThemeService, FSAAThemeService>();
            services.AddScoped<IGitHubService, GitHubService>();
            services.AddScoped<IMyService, MyService>();

            services.AddScoped<IGetAllDocumentTypesService, GetAllDocumentTypesService>();
            services.AddScoped<IGetAllServiceOrderTasksStatesService, GetAllServiceOrderTasksStatesService>();
            services.AddScoped<IGetAllServiceOrderTypesService, GetAllServiceOrderTypesService>();
            services.AddScoped<IGetAllSupplyOperationsService,GetAllSupplyOperationsService>();
            services.AddScoped<IGetAllServiceOrdersService,GetAllServiceOrdersService>();
            services.AddScoped<IGetAllServiceOrderRegistersService, GetAllServiceOrderRegistersService>();
            services.AddScoped<IGetAllServiceOrdersTasksService, GetAllServiceOrdersTasksService>();

            services.AddScoped<ICreateDocumentTypeService, CreateDocumentTypeService>();
            services.AddScoped<ICreateServiceOrderService, CreateServiceOrderService>();
            services.AddScoped<ICreateServiceOrderTaskStateService, CreateServiceOrderTaskStateService>();
            services.AddScoped<ICreateServiceOrderTypeService, CreateServiceOrderTypeService>();
            services.AddScoped<ICreateSupplyOperationService, CreateSupplyOperationService>();
            services.AddScoped<ICreateServiceOrderRegisterService, CreateServiceOrderRegisterService>();

            services.AddScoped<IUpdateDocumentTypeService, UpdateDocumentTypeService>();
            services.AddScoped<IUpdateServiceOrderTaskStateService, UpdateServiceOrderTaskStateService>();
            services.AddScoped<IUpdateServiceOrderTypeService, UpdateServiceOrderTypeService>();
            services.AddScoped<IUpdateSupplyOperationService, UpdateSupplyOperationService>();
            services.AddScoped<IUpdateServiceOrderService, UpdateServiceOrderService>();

            services.AddScoped<IGetDocumentTypeByIdService, GetDocumentTypeByIdService>();
            services.AddScoped<IGetServiceOrderTaskStateByIdService, GetServiceOrderTaskStateByIdService>();
            services.AddScoped<IGetServiceOrderTypeByIdService, GetServiceOrderTypeByIdService>();
            services.AddScoped<IGetSupplyOperationByIdService, GetSupplyOperationByIdService>();
            services.AddScoped<IGetServiceOrderByIdService, GetServiceOrderByIdService>();
            services.AddScoped<IGetServiceOrderByExecutorService, GetServiceOrderByExecutorService>();
            services.AddScoped<IGetServiceOrderByOwnerService, GetServiceOrderByOwnerService>();
            services.AddScoped<IGetCurrentSORegisterByIdService, GetCurrentSORegisterByIdService>();
            services.AddScoped<IGetServiceOrderTasksBySOIdService, GetServiceOrderTasksBySOIdService>();
            services.AddScoped<IGetServiceOrderRegistersBySOIdService, GetServiceOrderRegistersBySOIdService>();

            services.AddScoped<NotifierService>();

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
