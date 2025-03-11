using FSA.Management.Application.Infrastructure.Services.AppState;
using FSA.Razor.Components.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using RazorClassLibrary1.Components.Shared;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Pages.SO_Document;

namespace RazorClassLibrary1.Services
{
    public interface IFSASOCustomDialogService
    {
        Task<bool> ConfirmDialog(string message, string title);
        Task<bool> OpenDialog(string title, RenderFragment rf);
        Task<bool> Open_AddEditMaster(Dtos.MasterDto item, string title);
        Task<bool> Open_AddAdminEntity(ServiceOrderDocumentDto Company);
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

        public async Task<bool> Open_AddAdminEntity(ServiceOrderDocumentDto Company)
        {
            try
            {
                //var title = localizer["AddAdminEntity"];

                var result = await dialogService.OpenSideAsync<AddEditSODocument>(
                    "title",
                    new Dictionary<string, object>()
                    {
                { "Company", Company! },
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
    }
}
