using Bogus;
using FSA.Cache.Models;
using FSA.Core.DataType;
using FSA.Core.Utils;
using FSA.Management.Application.Features.Companies;
using Radzen;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages.SO_Task
{
    public partial class ServiceOrderTaskComponent
    {
        #region Properties

        private IQueryable<ServiceOrderTaskDto>? ListItems { get; set; }
        private int TotalItems { get; set; }
        private GridConfiguration GridConfiguration { get; set; } = new();
        private List<DataColumn> DataColumns { get; set; } = [];
        private List<GridItemAction> ItemActions { get; set; } = new();
        private List<GridGeneralAction> GeneralActions { get; set; } = new();
        private Pagination Pagination { get; set; } = new Pagination(10);
        record ItemCached(int TotalItems, IQueryable<ServiceOrderTaskDto> Items);

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            DataColumns = new List<DataColumn>
                {
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderTaskDto.Observations),
                        Title = "Observations",
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "250px",
                        Width = "300px",
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderTaskDto.ExecutionDate),
                        Title = Localizer["ExecutionDate"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px",
                        Width = "180px",
                    },
                    new DataColumn
                    {
                        Property = $"{nameof(ServiceOrderTaskDto.ServiceOrderTaskState)}.{nameof(ServiceOrderTaskDto.ServiceOrderTaskState.Description)}",
                        Title = Localizer["State"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "30px",
                        Width = "140px",
                    },
                new DataColumn
                    {
                        Property = $"{nameof(ServiceOrderTaskDto.ServiceOrder)}.{nameof(ServiceOrderTaskDto.ServiceOrder.Number)}",
                        Title = Localizer["ServiceOrder"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px",
                        Width = "180px",
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderTaskDto.IsActive),
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
                            Style = ButtonStyle.Primary.GetHashCode(),
                            //Show = o => { return admin; }
                        },
                        new GridItemAction
                        {
                            Action = GridItemActions.ADD_SUB_ITEM,
                            Icon = "quick_reference",
                            Title = "ViewServiceOrder",
                            Style = ButtonStyle.Primary.GetHashCode(),
                            //Show = o => { return admin; }
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
                            Title = @Localizer["AddServiceOrderTask"],
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
                var key = $"GetAllServiceOrdersTasksService-{Pagination.GetCacheId()}";
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
                    var response = await GetAllServiceOrdersTasksService.Handle(Pagination);
                    if (response.Succeeded)
                    {
                        TotalItems = (int)response.Pagination.TotalItems;
                        ListItems = response.Data!.AsQueryable();
                        Pagination = response.Pagination;
                    }
                }
                ListItems = ListItems ?? new List<ServiceOrderTaskDto>().AsQueryable();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                ListItems = new List<ServiceOrderTaskDto>().AsQueryable();
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
                var item = _item as ServiceOrderTaskDto;
                item = item is null ? new ServiceOrderTaskDto() : new ServiceOrderTaskDto(item);

                switch (action)
                {
                    case GridGeneralActions.ADD_ITEM:
                        var result = await CustomSODialogService.Open_AddEditSO_ServiceOrderTask(item);
                        if (result)
                        {
                        }
                        break;
                    case GridItemActions.VIEW_DETAILS:
                        await CustomSODialogService.Open_ServiceOrderTaskData(item!);
                        break;
                    case GridItemActions.EDIT_ITEM:
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
