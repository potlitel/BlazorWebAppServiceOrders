using FSA.Core.DataType;
using FSA.Core.Utils;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Components.Shared
{
    public partial class GenericDataGrid<TItem> where TItem : BaseDto
    {
        #region Properties
        private bool isLoading { get; set; } = false;
        public RadzenDataGrid<TItem>? DataGrid { get; set; }
        public IList<TItem> SelectedItems = default!;
        protected string search = string.Empty;
        string WidtActions = "120px";
        #endregion

        #region Parameters

        [Parameter, EditorRequired]
        public IQueryable<TItem> Items { get; set; } = default!;

        [Parameter, EditorRequired]
        public GridConfiguration GridConfiguration { get; set; } = new();

        [Parameter, EditorRequired]
        public List<DataColumn> DataColumns { get; set; } = new List<DataColumn>();

        [Parameter, EditorRequired]
        public List<GridItemAction> Actions { get; set; } = new List<GridItemAction>();
        [Parameter]
        public List<GridGeneralAction> GeneralActions { get; set; } = new List<GridGeneralAction>();
        [Parameter, EditorRequired]
        public string AddSufixTitle { get; set; } = "Item";
        [Parameter]
        public Pagination Pagination { get; set; } = new Pagination(10);

        #endregion

        #region Callbacks
        [Parameter]
        public EventCallback<(object, object)> ReturnToAction { get; set; }
        [Parameter]
        public EventCallback SearchActionOnEnter { get; set; }
        [Parameter]
        public EventCallback OnLoadData { get; set; }
        #endregion

        protected override void OnInitialized()
        {
            Pagination.TakeItems = GridConfiguration.PageSizeOptions.ElementAt(0);
        }

        private void CreateActions()
        {
            try
            {
                if (Actions.Count > 0)
                {
                    float width = (float)(Actions.Count * 48);

                    WidtActions = $"{width}px";
                    StateHasChanged();
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnParametersSet()
        {
            SelectedItems = default!;
            CreateActions();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
            {
                Localize();
            }
        }

        private void Localize()
        {
            if (DataGrid is not null)
            {
                #region Filters

                DataGrid.EqualsText = Localizer["EqualsText"];
                DataGrid.NotEqualsText = Localizer["NotEqualsText"];
                DataGrid.LessThanText = Localizer["LessThanText"];
                DataGrid.LessThanOrEqualsText = Localizer["LessThanOrEqualsText"];
                DataGrid.GreaterThanText = Localizer["GreaterThanText"];
                DataGrid.GreaterThanOrEqualsText = Localizer["GreaterThanOrEqualsText"];
                DataGrid.IsNullText = Localizer["IsNullText"];
                DataGrid.IsNotNullText = Localizer["IsNotNullText"];
                DataGrid.AndOperatorText = Localizer["AndOperatorText"];
                DataGrid.OrOperatorText = Localizer["OrOperatorText"];
                DataGrid.ContainsText = Localizer["ContainsText"];
                DataGrid.DoesNotContainText = Localizer["DoesNotContainText"];
                DataGrid.StartsWithText = Localizer["StartsWithText"];
                DataGrid.EndsWithText = Localizer["EndsWithText"];
                DataGrid.ClearFilterText = Localizer["ClearFilterText"];
                DataGrid.ApplyFilterText = Localizer["ApplyFilterText"];
                DataGrid.FilterText = Localizer["FilterText"];
                DataGrid.IsEmptyText = Localizer["IsEmptyText"];
                DataGrid.IsNotEmptyText = Localizer["IsNotEmptyText"];
                #endregion
            }
        }

        void Loading()
        {
            isLoading = !isLoading;
            StateHasChanged();
        }

        async Task SearchOnEnter(string value)
        {

            if (search != value)
            {
                search = value;
                Pagination.Page = 1;
                Pagination.FilterTerm = search;
                await DataGrid!.GoToPage(0);

                Loading();
                await SearchActionOnEnter.InvokeAsync();
                Loading();
            }
        }

        async Task ExecuteAction(string action, TItem item)
        {
            Loading();
            await ReturnToAction.InvokeAsync((action, item));
            Loading();
        }

        async Task ExecuteAction(RadzenSplitButtonItem action, TItem item)
        {
            Loading();
            await ReturnToAction.InvokeAsync((action.Value, item));
            Loading();
        }

        async Task ExecuteAction(GridAction action, TItem item)
        {
            Loading();
            await ReturnToAction.InvokeAsync((action.Action, item));
            Loading();
        }

        public void Refresh()
        {
            try
            {
                DataGrid!.Reload();
            }
            catch (Exception)
            {
            }
        }

        private bool IsActive(TItem data)
        {
            return data.IsActive;
        }

        #region ContextMenu
        void OnCellContextMenu(DataGridCellMouseEventArgs<TItem> args)
        {
            SelectedItems = new List<TItem>() { args.Data };

            List<ContextMenuItem> list = new();
            if (Actions.Count > 0)
                foreach (var action in Actions.Where(a => a.Show(null!)))
                    list.Add(new ContextMenuItem() { Text = Localizer[action.Title], Value = action.Action, Icon = action.Icon });
            ContextMenuService.Open(args, list, OnMenuItemClick);
        }

        void OnMenuItemClick(MenuItemEventArgs args)
        {
            ContextMenuService.Close();
            ExecuteAction((string)args.Value, SelectedItems[0]).ConfigureAwait(false);
        }
        #endregion

        void ShowTooltip(ElementReference elementReference, GridAction action)
        {
            //var color = GetColorToToolTip(action.Action);
            var title = action is null ? AddSufixTitle : action.Title;
            TooltipOptions options = new TooltipOptions()
            {
                //Style = $"background-color: var(--rz-base-background-color); color: var(--rz-text-color)",
                Style = $"background-color: var(--rz-text-color); color: var(--rz-base-background-color)",
            };
            TooltipService.OpenOnTheBottom(elementReference, $"{Localizer[title]}", options);
        }

        void CloseTooltip()
        {
            TooltipService.Close();
        }

        string GetColorToToolTip(string action)
        {
            switch (action)
            {
                case GridGeneralActions.ADD_ITEM:
                    return "primary";
                case GridItemActions.ADD_SUB_ITEM:
                    return "info";
                case GridItemActions.EDIT_ITEM:
                case GridItemActions.TOGGLE_ITEM:
                    return "warning";
                case GridItemActions.DELETE_ITEM:
                case GridItemActions.RESET_PASSWORD:
                    return "danger";
                default:
                    return "primary";
            }
        }

        void OnPage(PagerEventArgs args)
        {
            Pagination.Page = args.PageIndex + 1;
        }

        async Task LoadData(LoadDataArgs args)
        {
            if (Pagination.TakeItems != args.Top && args.Skip != 0)
            {
                Pagination.TakeItems = args.Top ?? 10;
                return;
            }

            Pagination.Page = args.Skip == 0 ? 1 : Pagination.Page;
            Pagination.TakeItems = args.Top ?? 10;
            Pagination.Filter = args.Filter;
            Pagination.OrderBy = args.OrderBy;

            Loading();
            await OnLoadData.InvokeAsync();
            Loading();
        }
    }
}
