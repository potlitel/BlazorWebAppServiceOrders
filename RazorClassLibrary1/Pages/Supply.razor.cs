﻿using FSA.Cache.Models;
using FSA.Core.DataType;
using FSA.Core.Utils;
using FSA.Management.Application.Features.CompanyGroups;
using Radzen;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages
{
    public partial class Supply
    {
        #region Properties

        private IQueryable<SupplyDto>? ListItems { get; set; }
        private int TotalItems { get; set; }
        private GridConfiguration GridConfiguration { get; set; } = new();
        private List<DataColumn> DataColumns { get; set; } = [];
        private List<GridItemAction> ItemActions { get; set; } = new();
        private List<GridGeneralAction> GeneralActions { get; set; } = new();
        private Pagination Pagination { get; set; } = new Pagination(10);
        record ItemCached(int TotalItems, IQueryable<SupplyDto> Items);

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            DataColumns = new List<DataColumn>
                {
                    new DataColumn
                    {
                        Property = nameof(SupplyDto.Amount),
                        Title = Localizer["Amount"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "30px",
                        Width = "110px",
                    },
                    new DataColumn
                    {
                        Property = nameof(SupplyDto.Price),
                        Title = Localizer["Price"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "90px",
                        Width = "140px",
                    },
                    new DataColumn
                    {
                        Property = nameof(SupplyDto.Description),
                        Title = "Description",
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "150px",
                        Width = "190px",
                    },
                    new DataColumn
                    {
                        //Property = nameof(SupplyDto.SupplyOperationId),
                        Property = $"{nameof(SupplyDto.SupplyOperation)}.{nameof(SupplyDto.SupplyOperation.Description)}",
                        Title = Localizer["SupplyOperation"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px",
                        Width = "210px",
                    },
                    new DataColumn
                    {
                        //Property = nameof(SupplyDto.ServiceOrderTaskId),
                        Property = $"{nameof(SupplyDto.ServiceOrderTask)}.{nameof(SupplyDto.ServiceOrderTask.Observations)}",
                        Title = Localizer["ServiceOrderTask"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px",
                        Width = "210px",
                    },
                    new DataColumn
                    {
                        Property = nameof(SupplyDto.IsActive),
                        Title = "Active",
                        Filterable = true,
                        Sortable = true,
                        Width = "80px",
                        MinWidth = "140px"
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
                            Title = @Localizer["AddSupply"],
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
                var key = $"GetAllSuppliesService-{Pagination.GetCacheId()}";
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
                    var response = await GetAllSuppliesService.Handle(Pagination);
                    if (response.Succeeded)
                    {
                        TotalItems = (int)response.Pagination.TotalItems;
                        ListItems = response.Data!.AsQueryable();
                        Pagination = response.Pagination;
                    }
                }
                ListItems = ListItems ?? new List<SupplyDto>().AsQueryable();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                ListItems = new List<SupplyDto>().AsQueryable();
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
                var item = _item as SupplyDto;
                item = item is null ? new SupplyDto() : new SupplyDto(item);

                switch (action)
                {
                    case GridGeneralActions.ADD_ITEM:
                        var result = await CustomSODialogService.Open_AddEditSO_Supply(item);
                        await LoadItems(true);
                        break;
                    case GridItemActions.VIEW_DETAILS:
                        await CustomSODialogService.Open_ServiceOrderSupplyData(item!);
                        break;
                    case GridItemActions.EDIT_ITEM:
                        result = await CustomSODialogService.Open_AddEditSO_Supply(item);
                        await LoadItems(true);
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
