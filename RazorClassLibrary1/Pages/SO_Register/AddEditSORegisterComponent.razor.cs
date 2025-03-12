using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages.SO_Register
{
    public partial class AddEditSORegisterComponent
    {
        #region Parameters
        [Parameter, EditorRequired]
        public ServiceOrderRegisterDto ServiceOrderRegister { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        #endregion
        public AddEditSORegisterComponent()
        {

        }
    }
}
