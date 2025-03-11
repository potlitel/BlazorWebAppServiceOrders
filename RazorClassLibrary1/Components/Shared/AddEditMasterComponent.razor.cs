using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Components.Shared
{
    public partial class AddEditMasterComponent<TItem> where TItem : MasterDto
    {
        #region Parameters
        [Parameter, EditorRequired]
        public TItem Item { get; set; }
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        [Parameter]
        public bool IsCreatedItem { get; set; } = false;
        #endregion Parameters

        #region Properties
        private bool IsLoading { get; set; } = true;
        private bool Busy { get; set; } = false;
        private bool IsEditMode { get; set; }
        [Parameter]
        public EventCallback MasterSubmit { get; set; }
        #endregion Properties

        protected override void OnInitialized()
        {
            IsLoading = false;
        }

        private void CloseDialog(bool result)
        {
            if (IsSideDialog)
                DialogService!.CloseSide(result);
            else
                DialogService!.Close(result);
        }
        protected void Cancel() => CloseDialog(false);
        protected void Submit()
        {
            if (MasterSubmit.HasDelegate)
                MasterSubmit.InvokeAsync();
            else
                CloseDialog(true);
        }
    }
}
