using Microsoft.AspNetCore.Components;
using Radzen;
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

        void ShowTooltip(ElementReference elementReference, string msg, TooltipOptions options = null!) => 
            tooltipService.Open(elementReference, msg, options);
    }
}
