using Bogus;
using FSA.Cache.Models;
using FSA.Core.DataType;
using FSA.Core.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Radzen;
using RazorClassLibrary1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FSA.Management.Application.Domain.Constants.Permissions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace RazorClassLibrary1.Pages
{
    public partial class ServiceOrderComponent
    {
        #region Properties

        private IQueryable<ServiceOrderDto>? ListItems { get; set; }
        private int TotalItems { get; set; }
        private GridConfiguration GridConfiguration { get; set; } = new();
        private List<DataColumn> DataColumns { get; set; } = [];
        private List<GridItemAction> ItemActions { get; set; } = new();
        private List<GridGeneralAction> GeneralActions { get; set; } = new();
        private Pagination Pagination { get; set; } = new Pagination(10);
        record ItemCached(int TotalItems, IQueryable<ServiceOrderDto> Items);

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            DataColumns = new List<DataColumn>
                {
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.Number),
                        Title = Localizer["Number"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "160px"
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.EstimatedEndingDate),
                        Title = Localizer["EstimatedEndingDate"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "100px"
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.Observations),
                        Title = Localizer["Observations"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px"
                    },
                new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.Address),
                        Title = "Address",
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px"
                    },
                new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.ServiceOrderTypeId),//reemplazar por su correspondiente valor de entidad
                        Title = "Type",
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px"
                    },
                new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.ParentServiceOrderId),//reemplazar por su correspondiente valor de entidad
                        Title = "Parent SO",
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px"
                    },
                new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.ExecutorId),//reemplazar por su correspondiente valor de entidad
                        Title = "Executor",
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px"
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderDto.IsActive),
                        Title = "Active",
                        Filterable = true,
                        Sortable = true,
                        Width = "120px",
                        MinWidth = "120px"
                    },
                };
            CreateActions();
            await LoadItems(false);
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
                        new GridItemAction
                        {
                            Action = GridItemActions.VIEW_DETAILS,
                            Icon = "preview",
                            Title = "ViewData",
                            Style = ButtonStyle.Info.GetHashCode(),
                            //Show = o => { return (admin || update); }
                        },
                        new GridItemAction
                        {
                            Action = GridItemActions.EDIT_ITEM,
                            Icon = "edit",
                            Title = "Edit",
                            Style = ButtonStyle.Warning.GetHashCode(),
                            //Show = show => { return (admin || update); }
                        },

                    ];
                #endregion

                #region GeneralActions
                GeneralActions =
                    [
                        new GridGeneralAction
                        {
                            Action = GridGeneralActions.ADD_ITEM,
                            Icon = "add",
                            Title = Localizer["AddServiceOrder"],
                            Style = ButtonStyle.Primary.GetHashCode(),
                            //Show = show => { return create; }
                        }
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
                //ListItems = new Faker<ServiceOrderDocumentDto>()
                //                .RuleFor(x => x.Name, f => f.Finance.Account(15))
                //                .RuleFor(x => x.Url, f => f.Image.PicsumUrl())
                //                .RuleFor(x => x.ServiceOrderId, f => f.Random.Long())
                //                .RuleFor(x => x.DocumentTypeId, f => f.Random.Long())
                //                .Generate(50).ToList().AsQueryable();
                var key = $"GetAllServiceOrdersService-{Pagination.GetCacheId()}";
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
                    var response = await GetAllServiceOrdersService.Handle(Pagination);
                    if (response.Succeeded)
                    {
                        TotalItems = (int)response.Pagination.TotalItems;
                        ListItems = response.Data!.AsQueryable();
                        Pagination = response.Pagination;
                    }
                }
                //ListItems = ListItems ?? new List<ServiceOrderDto>().AsQueryable();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                ListItems = new List<ServiceOrderDto>().AsQueryable();
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
                var item = _item as ServiceOrderDto;
                item = item is null ? new ServiceOrderDto() : new ServiceOrderDto(item);

                switch (action)
                {
                    case GridGeneralActions.ADD_ITEM:
                        var result = await CustomSODialogService.Open_AddEditSO_ServiceOrder(item);
                        if (result)
                        {
                            await LoadItems(true);
                        }
                        break;
                    case GridItemActions.VIEW_DETAILS:
                        await CustomSODialogService.Open_ServiceOrderData(item!);
                        break;
                    case GridItemActions.EDIT_ITEM:
                        result = await CustomSODialogService.Open_AddEditSO_ServiceOrder(item);
                        if (result)
                        {
                            await LoadItems(true);
                        }
                        break;
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                NotificationService.ShowNotification(NotificationSeverity.Error, ex.Message, Localizer["ErrorCompanyGroup"]);
            }
        }
    }
}
