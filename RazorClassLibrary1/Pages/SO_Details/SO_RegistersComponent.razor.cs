﻿using FSA.Cache.Models;
using FSA.Core.DataType;
using FSA.Core.Utils;
using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;


namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class SO_RegistersComponent
    {

        [CascadingParameter]
        public ServiceOrderDto ServiceOrder { get; set; } = new();


        #region Properties

        private IQueryable<ServiceOrderRegisterDto>? ListItems { get; set; }
        private int TotalItems { get; set; }
        private GridConfiguration GridConfiguration { get; set; } = new();
        private List<DataColumn> DataColumns { get; set; } = [];
        private List<GridItemAction> ItemActions { get; set; } = new();
        private List<GridGeneralAction> GeneralActions { get; set; } = new();
        private Pagination Pagination { get; set; } = new Pagination(10);
        record ItemCached(int TotalItems, IQueryable<ServiceOrderRegisterDto> Items);

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            DataColumns = new List<DataColumn>
                {
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderRegisterDto.Trigger),
                        Title = Localizer["Trigger"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "120px",
                        Width = "180px",
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderRegisterDto.StateFrom),
                        Title = Localizer["StateFrom"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "120px",
                        Width = "170px",
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderRegisterDto.StateTo),
                        Title = Localizer["StateTo"],
                        Filterable = true,
                        Sortable = true,
                        Width = "170px",
                        MinWidth = "120px"
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderRegisterDto.Observations),
                        Title = Localizer["Observations"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "120px"
                    }
                };
            CreateActions();
            await LoadItems(false);
            NotifierService.Notifier += OnNotify;
        }
        private void CreateActions()
        {
            try
            {
                var user = AppState!.GetUser();

                #region Actions
                //var admin = user.IsAdmin();
                //var update = user.PermissionExist(Permissions.CompanyGroup.Management)
                //            || user.PermissionExist(Permissions.CompanyGroup.Update);
                //var create = user.PermissionExist(Permissions.CompanyGroup.Management)
                //            || user.PermissionExist(Permissions.CompanyGroup.Create);
                #endregion

                #region ItemActions
                ItemActions =
                    [
                        //new GridItemAction
                        //{
                        //    Action = GridItemActions.ADD_SUB_ITEM,
                        //    Icon = "add_circle",
                        //    Title = "AddManager",
                        //    Style = ButtonStyle.Primary.GetHashCode(),
                        //    //Show = o => { return admin; }
                        //},
                        //new GridItemAction
                        //{
                        //    Action = GridItemActions.EDIT_ITEM,
                        //    Icon = "edit",
                        //    Title = "Edit",
                        //    Style = ButtonStyle.Warning.GetHashCode(),
                        //    //Show = show => { return (admin || update); }
                        //},

                    ];
                #endregion

                #region GeneralActions
                GeneralActions =
                    [
                        //new GridGeneralAction
                        //{
                        //    Action = GridGeneralActions.ADD_ITEM,
                        //    Icon = "add",
                        //    Title = @Localizer["AddServiceOrderRegister"],
                        //    Style = ButtonStyle.Primary.GetHashCode(),
                        //    //Show = show => { return create; }
                        //}
                    ];
                #endregion

            }
            catch (Exception)
            {
            }
        }

        private async Task LoadItems(bool deleteCache)
        {
            try
            {
                //await Task.CompletedTask;
                var key = $"GetServiceOrderRegistersBySOIdService-{Pagination.GetCacheId()}";
                if (deleteCache)
                    AppCache.RemoveItem(key, CacheType.IndexedDB);

                var ResponseItemCached = await AppCache.GetItem<ItemCached?>(key, CacheType.IndexedDB);
                if (ResponseItemCached != null)
                {
                    ListItems = ResponseItemCached.Items.AsQueryable();
                    TotalItems = ResponseItemCached.TotalItems;
                }
                else
                {
                    var response = await GetServiceOrderRegistersBySOIdService.Handle((int)ServiceOrder.Id, Pagination);
                    if (response.Succeeded)
                    {
                        TotalItems = (int)response.Pagination.TotalItems;
                        ListItems = response.Data!.AsQueryable();
                        Pagination = response.Pagination;
                    }
                }
                ListItems = ListItems ?? new List<ServiceOrderRegisterDto>().AsQueryable();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                ListItems = new List<ServiceOrderRegisterDto>().AsQueryable();
                NotificationService.ShowNotification(NotificationSeverity.Error, $"{ex.Message}");
            }
        }

        protected async Task Search()
        {
            try
            {
                await LoadItems(false);
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                NotificationService.ShowNotification(NotificationSeverity.Error, $"{ex.Message}");
            }
            finally
            {
                await InvokeAsync(StateHasChanged);
            }
        }

        protected async Task LoadData()
        {
            await LoadItems(false);
        }

        public async Task ExecuteToAction(object action, object _item)
        {
            try
            {
                var item = _item as ServiceOrderRegisterDto;
                item = item is null ? new ServiceOrderRegisterDto() : new ServiceOrderRegisterDto(item);

                switch (action)
                {
                    case GridGeneralActions.ADD_ITEM:
                        var result = await CustomSODialogService.Open_AddEditSO_Register(item);
                        await LoadItems(true);
                        break;
                    case GridItemActions.ADD_SUB_ITEM:
                        //result = await CustomDialogService.Open_AddManager(item, null);
                        //if (result)
                        //    await LoadItems(true);
                        break;
                    case GridItemActions.EDIT_ITEM:
                        //result = await CustomDialogService.Open_AddEditMaster(item, "EditCompanyGroup");
                        //if (result)
                        //{
                        //    var response = await UpdateCompanyGroupService.Handle(item!);
                        //    NotificationService.ShowNotification(response.Succeeded,
                        //                                        response.StatusCode.ToString(),
                        //                                        item!.Code);
                        //    await LoadItems(true);
                        //}
                        break;
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                NotificationService.ShowNotification(NotificationSeverity.Error, ex.Message, Localizer["ErrorCompanyGroup"]);
            }
        }

        private async Task OnNotify(string key, object? value)
        {
            // Actualizar estado según la notificación
            if (key == NotificationsKeys.UpdateRegistersList)
                await LoadItems(false);

            StateHasChanged();
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        public void Dispose()
        {
            NotifierService.Notifier -= OnNotify;
        }
    }
}
