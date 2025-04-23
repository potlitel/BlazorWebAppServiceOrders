using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class SO_TimelineViewComponent
    {
        [CascadingParameter]
        public ServiceOrderDto ServiceOrder { get; set; } = new();

        string maxRegister = string.Empty;

        protected override async Task OnInitializedAsync() =>
            //gets the max value of CreatedAt field
            maxRegister = await Task.FromResult(ServiceOrder.Registers!.Max(entry => entry.CreatedAt.ToString()));
    }
}
