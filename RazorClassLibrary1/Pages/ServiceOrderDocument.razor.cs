using FSA.Cache.Models;
using FSA.Core.DataType;
using FSA.Core.Utils;
using Radzen;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages
{
    public partial class ServiceOrderDocument
    {
        #region Properties

        private IQueryable<ServiceOrderDocumentDto>? ListItems { get; set; }
        private int TotalItems { get; set; }
        private GridConfiguration GridConfiguration { get; set; } = new();
        private List<DataColumn> DataColumns { get; set; } = [];
        private List<GridItemAction> ItemActions { get; set; } = new();
        private List<GridGeneralAction> GeneralActions { get; set; } = new();
        private Pagination Pagination { get; set; } = new Pagination(10);
        record ItemCached(int TotalItems, IQueryable<ServiceOrderDocumentDto> Items);

        #endregion Properties

        protected override async Task OnInitializedAsync()
        {
            DataColumns = new List<DataColumn>
                {
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderDocumentDto.Name),
                        Title = Localizer["Name"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "50px",
                        Width = "290px",
                    },
                    //new DataColumn
                    //{
                    //    Property = nameof(ServiceOrderDocumentDto.Url),
                    //    Title = "Url",
                    //    Filterable = true,
                    //    Sortable = true,
                    //    MinWidth = "120px"
                    //},
                    new DataColumn
                    {
                        Property = $"{nameof(ServiceOrderDocumentDto.ServiceOrder)}.{nameof(ServiceOrderDocumentDto.ServiceOrder.Number)}",
                        Title = Localizer["ServiceOrder"],
                        Filterable = true,
                        Sortable = true,
                        MinWidth = "160px",
                        Width = "170px",
                    },
                new DataColumn
                    {
                        Property = $"{nameof(ServiceOrderDocumentDto.DocumentType)}.{nameof(ServiceOrderDocumentDto.DocumentType.Description)}",
                        Title = Localizer["DocumentType"],
                        Filterable = true,
                        Sortable = true,
                        Width = "140px",
                        MinWidth = "140px"
                    },
                    new DataColumn
                    {
                        Property = nameof(ServiceOrderDocumentDto.IsActive),
                        Title = "Active",
                        Filterable = true,
                        Sortable = true,
                        Width = "70px",
                        MinWidth = "70px"
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
                        //new GridItemAction
                        //{
                        //    Action = GridItemActions.VIEW_DETAILS,
                        //    Icon = "preview",
                        //    Title = "ViewData",
                        //    Style = ButtonStyle.Info.GetHashCode(),
                        //    //Show = o => { return (admin || update); }
                        //},
                        new GridItemAction
                        {
                            Action = GridItemActions.TOGGLE_ITEM,
                            Icon = "download",
                            Title = "Download",
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
                            Title = @Localizer["AddServiceOrderDocument"],
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
                var key = $"GetAllServicesOrdersDocumentsService-{Pagination.GetCacheId()}";
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
                    var response = await GetAllServicesOrdersDocumentsService.Handle(Pagination);
                    if (response.Succeeded)
                    {
                        TotalItems = (int)response.Pagination.TotalItems;
                        ListItems = response.Data!.AsQueryable();
                        Pagination = response.Pagination;
                    }
                }
                ListItems = ListItems ?? new List<ServiceOrderDocumentDto>().AsQueryable();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                ListItems = new List<ServiceOrderDocumentDto>().AsQueryable();
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
                var item = _item as ServiceOrderDocumentDto;
                item = item is null ? new ServiceOrderDocumentDto() : new ServiceOrderDocumentDto(item);

                switch (action)
                {
                    case GridGeneralActions.ADD_ITEM:
                        var result = await CustomSODialogService.Open_AddEditSO_Document(item);
                        if (result)
                            await LoadItems(true);
                        break;
                    case GridItemActions.TOGGLE_ITEM:
                        //var file = await DownloadServiceOrderDocumentService.Handle(item.Name);

                        var stream = await DownloadServiceOrderDocumentAsStreamService.Handle(item.Name);
                        // Call SaveAsFileAsync method in order to download the file
                        // and to save it in the Download location.
                        //await BlobService.SaveAsFileAsync(file);

                        // Create a IBlob and copy data into it.
                        var blob = await BlobService.CreateBlobAsync(stream);
                        // Now we can just call SaveAsFileAsync to download the file
                        await BlobService.SaveAsFileAsync(blob, item.Name);

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
