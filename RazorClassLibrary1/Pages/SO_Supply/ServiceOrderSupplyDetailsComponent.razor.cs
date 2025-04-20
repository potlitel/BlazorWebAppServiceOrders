using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages.SO_Supply
{
    public partial class ServiceOrderSupplyDetailsComponent
    {
        [Parameter]
        public SupplyDto Supply { get; set; } = new();

        [Parameter]
        public bool IsSideDialog { get; set; } = false;
    }
}
