using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages.SO_Supply
{
    public partial class AddEditSOSupplyComponent
    {
        #region Parameters
        [Parameter, EditorRequired]
        public SupplyDto ServiceOrderDocument { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        #endregion
        public AddEditSOSupplyComponent()
        {

        }
    }
}
