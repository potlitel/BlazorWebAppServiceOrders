using FSA.Core.DataType;
using FSA.Core.Utils;
using Radzen;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages.Masters.DocumentType
{
    public partial class DocumentTypeComponent
    {
        #region Properties

        private IQueryable<DocumentTypeDto>? ListItems { get; set; }
        private int TotalItems { get; set; }
        private GridConfiguration GridConfiguration { get; set; } = new();
        private List<DataColumn> DataColumns { get; set; } = [];
        private List<GridItemAction> ItemActions { get; set; } = new();
        private List<GridGeneralAction> GeneralActions { get; set; } = new();
        private Pagination Pagination { get; set; } = new Pagination(10);
        record ItemCached(int TotalItems, IQueryable<DocumentTypeDto> Items);

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            DataColumns = new List<DataColumn>
                {
                    new DataColumn
                    {
                        Property = nameof(DocumentTypeDto.Code),
                        Title = (Localizer["Code"]),
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "120px"
                    },
                    new DataColumn
                    {
                        Property = nameof(DocumentTypeDto.Description),
                        Title = (Localizer["Description"]),
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "120px"
                    },
                    new DataColumn
                    {
                        Property = nameof(DocumentTypeDto.IsActive),
                        Title = (Localizer["Active"]),
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
                var user = appState!.GetUser();

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
                            Action = GridItemActions.ADD_SUB_ITEM,
                            Icon = "add_circle",
                            Title = "AddManager",
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
                            Title = Localizer["AddDocumentType"],
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
                await Task.CompletedTask;
                //var key = $"GetAllCompanyGroupsService-{Pagination.GetCacheId()}";
                //if (deleteCache)
                //    AppCache.RemoveItem(key, CacheType.IndexedDB);

                //var ResponseItemCached = await AppCache.GetItem<ItemCached?>(key, CacheType.IndexedDB);
                //if (ResponseItemCached != null)
                //{
                //    ListItems = ResponseItemCached.Items.AsQueryable();
                //    TotalItems = ResponseItemCached.TotalItems;
                //}
                //else
                //{
                //    var response = await GetAllCompanyGroupsService.Handle(Pagination);
                //    if (response.Succeeded)
                //    {
                //        TotalItems = (int)response.Pagination.TotalItems;
                //        ListItems = response.Data!.CompanyGroups.AsQueryable();
                //        Pagination = response.Pagination;
                //    }
                //}
                ListItems = ListItems ?? new List<DocumentTypeDto>().AsQueryable();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                ListItems = new List<DocumentTypeDto>().AsQueryable();
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
                var item = _item as DocumentTypeDto;
                item = item is null ? new DocumentTypeDto() : new DocumentTypeDto(item);

                switch (action)
                {
                    case GridGeneralActions.ADD_ITEM:
                        var result = await CustomSODialogService.Open_AddEditMaster(item, Localizer["AddDocumentType"]);
                        if (result)
                        {
                            //var response = await CreateSystemModuleService.Handle(item!);
                            //NotificationService.ShowNotification(response.Succeeded,
                            //                                     response.StatusCode.ToString(),
                            //                                     item!.Code);
                            await LoadItems(true);
                        }
                        break;
                    case GridItemActions.EDIT_ITEM:
                        result = await CustomSODialogService.Open_AddEditMaster(item, Localizer["EditDocumentType"]);
                        if (result)
                        {
                            //var response = await UpdateSystemModuleService.Handle(item!);
                            //NotificationService.ShowNotification(response.Succeeded,
                            //                                    response.StatusCode.ToString(),
                            //                                    item!.Code);
                            await LoadItems(true);
                        }
                        break;
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                //NotificationService.ShowNotification(NotificationSeverity.Error, ex.Message, Localizer["ErrorConsortium"]);
                NotificationService.ShowNotification(NotificationSeverity.Error, ex.Message, "Error");
            }
        }
    }
}
