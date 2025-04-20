using FSA.Management.Application.Features.Municipalities;
using FSA.Management.Application.Infrastructure.Services.AppState;
//using FSA.Razor.Components.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using RazorClassLibrary1.Components.Shared;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Pages;
using RazorClassLibrary1.Pages.SO_Details;
using RazorClassLibrary1.Pages.SO_Document;
using RazorClassLibrary1.Pages.SO_Feature;
using RazorClassLibrary1.Pages.SO_Register;
using RazorClassLibrary1.Pages.SO_Supply;
using RazorClassLibrary1.Pages.SO_Task;
using RazorClassLibrary1.Pages.SO_TaskDocument;
using RazorClassLibrary1.Resources;

namespace RazorClassLibrary1.Services
{
    public interface IFSASOCustomDialogService
    {
        Task<bool> ConfirmDialog(string message, string title);
        Task<bool> OpenDialog(string title, RenderFragment rf);
        Task<bool> Open_AddEditMaster(Dtos.MasterDto item, string title);
        Task<bool> Open_AddEditSO_Document(ServiceOrderDocumentDto item);
        Task<bool> Open_AddEditSO_Feature(ServiceOrderFeatureDto item);
        Task<bool> Open_AddEditSO_Register(ServiceOrderRegisterDto item);
        Task<bool> Open_AddEditSO_Supply(SupplyDto item);
        Task<bool> Open_AddEditSO_TaskDocument(ServiceOrderTaskDocumentDto item);
        Task<bool> Open_AddEditSO_ServiceOrderTask(ServiceOrderTaskDto item);
        Task<bool> Open_AddEditSO_ServiceOrder(ServiceOrderDto item);
        Task<bool> Open_ServiceOrderData(ServiceOrderDto item);
        Task<bool> Open_ServiceOrderTaskData(ServiceOrderTaskDto item);
        Task<bool> Open_ServiceOrderRegisterData(ServiceOrderRegisterDto item);
        Task<bool> Open_ServiceOrderSupplyData(SupplyDto item);
    }

    public class FSASOCustomDialogService : IFSASOCustomDialogService
    {
        private readonly DialogService dialogService;
        private readonly IStringLocalizer<Resource> localizer;
        private readonly IManagementAppState appState;

        public FSASOCustomDialogService(DialogService dialogService, 
                                        IStringLocalizer<Resource> localizer, 
                                        IManagementAppState appState)
        {
            this.dialogService = dialogService;
            this.localizer = localizer;
            this.appState = appState;
        }

        public async Task<bool> ConfirmDialog(string message, string title)
        {
            var result = await dialogService.Confirm(
                message,
                title,
                new ConfirmOptions()
                {
                    OkButtonText = localizer["Yes"],
                    CancelButtonText = localizer["No"]
                }
            );

            return result is null ? false : (bool)result;
        }

        public async Task<bool> OpenDialog(string title, RenderFragment rf)
        {
            var result = await dialogService.OpenAsync(
                title,
                ds => rf
            );
            return result is null ? false : (bool)result;
        }
        private SideDialogOptions GetOptions()
        {
            return new SideDialogOptions
            {
                CloseDialogOnOverlayClick = true,
                Position = DialogPosition.Right,
                Width = "500px",
                ShowClose = false,
                ShowMask = true,
            };
        }

        public async Task<bool> Open_AddEditMaster(Dtos.MasterDto item, string title)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditMasterComponent<Dtos.MasterDto>>(
                    (item.Id == 0 ? localizer[title] : localizer[title]),
                    new Dictionary<string, object>()
                    {
                        { "Item", item },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );
                return result is not null && (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_AddEditSO_Document(ServiceOrderDocumentDto dto)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditSODocument>(
                    (dto.Id == 0 ? localizer["AddServiceOrderDocument"] : localizer["EditServiceOrderDocument"]),
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrderDocument", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> Open_AddEditSO_Feature(ServiceOrderFeatureDto dto)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditSOFeatureComponent>(
                    (dto.Id == 0 ? localizer["AddServiceOrderFeature"] : localizer["EditServiceOrderFeature"]),
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrderFeature", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_AddEditSO_Register(ServiceOrderRegisterDto dto)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditSORegisterComponent>(
                    (dto.Id == 0 ? localizer["AddServiceOrderRegister"] : localizer["EditServiceOrderRegister"]),
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrderRegister", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_AddEditSO_Supply(SupplyDto dto)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditSOSupplyComponent>(
                    (dto.Id == 0 ? localizer["AddSupply"] : localizer["EditSupply"]),
                    new Dictionary<string, object>()
                    {
                        { "Supply", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_AddEditSO_TaskDocument(ServiceOrderTaskDocumentDto dto)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditSOTaskDocumentComponent>(
                    (dto.Id == 0 ? localizer["AddServiceOrderTaskDocument"] : localizer["EditServiceOrderTaskDocument"]),
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrderTaskDocument", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_AddEditSO_ServiceOrderTask(ServiceOrderTaskDto dto)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditServiceOrderTaskComponent>(
                    (dto.Id == 0 ? localizer["AddServiceOrderTask"] : localizer["EditServiceOrderTask"]),
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrderTask", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_AddEditSO_ServiceOrder(ServiceOrderDto dto)
        {
            try
            {
                var result = await dialogService.OpenSideAsync<AddEditServiceOrderComponent>(
                    dto.Id == 0 ? localizer["AddServiceOrder"] : localizer["EditServiceOrder"],
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrder", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_ServiceOrderData(ServiceOrderDto dto)
        {
            try
            {
                var result = await dialogService.OpenAsync<ServiceOrderDetailsComponent>(
                    localizer["ServiceOrderData"] + ": " + dto.Number,
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrder", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetDialogOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private DialogOptions GetDialogOptions()
        {
            return new DialogOptions
            {
                CloseDialogOnOverlayClick = true,
                Width = "1250px",
                ShowClose = false,
            };
        }

        public async Task<bool> Open_ServiceOrderTaskData(ServiceOrderTaskDto dto)
        {
            try
            {
                var result = await dialogService.OpenAsync<ServiceOrderTaskDetailsComponent>(
                    localizer["ServiceOrderTaskData"],
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrderTask", dto },
                        { "IsSideDialog", true }
                    },
                    options: GetDialogOptions()
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_ServiceOrderRegisterData(ServiceOrderRegisterDto dto)
        {
            try
            {
                var result = await dialogService.OpenAsync<ServiceOrderRegisterDetailsComponent>(
                    localizer["ServiceOrderRegisterData"],
                    new Dictionary<string, object>()
                    {
                        { "ServiceOrderRegister", dto },
                        { "IsSideDialog", true }
                    },
                    options: new DialogOptions
                    {
                        CloseDialogOnOverlayClick = true,
                        Width = "570px",
                        ShowClose = false,
                    }
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Open_ServiceOrderSupplyData(SupplyDto dto)
        {
            try
            {
                var result = await dialogService.OpenAsync<ServiceOrderSupplyDetailsComponent>(
                    localizer["SupplyData"],
                    new Dictionary<string, object>()
                    {
                        { "Supply", dto },
                        { "IsSideDialog", true }
                    },
                    options: new DialogOptions
                    {
                        CloseDialogOnOverlayClick = true,
                        Width = "550px",
                        ShowClose = false,
                    }
                );

                return result is null ? false : (bool)result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
