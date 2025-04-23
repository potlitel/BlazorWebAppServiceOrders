using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;


namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class SO_TimelineViewComponent
    {
        [CascadingParameter]
        public ServiceOrderDto ServiceOrder { get; set; } = new();

        string maxRegister = string.Empty;

        protected override async Task OnInitializedAsync() {

            NotifierService.Notifier += OnNotify;
            //gets the max value of CreatedAt field
            maxRegister = await Task.FromResult(ServiceOrder.Registers!.Max(entry => entry.CreatedAt.ToString()));
        }
        void ShowTooltip(ElementReference elementReference, string msg, TooltipOptions options = null!) => 
            tooltipService.Open(elementReference, msg, options);

        private async Task OnNotify(string key, object? value)
        {
            // Actualizar estado según la notificación
            if (key == NotificationsKeys.UpdateRegistersList)
            {
                var registersBySOIdTask = GetServiceOrderRegistersBySOIdService.Handle((int)ServiceOrder.Id, null);
                var registersBySOId = await registersBySOIdTask;
                if (registersBySOId.Succeeded)
                {
                    ServiceOrder.Registers = registersBySOId.Data!.ToList();
                }
            }
            StateHasChanged();
        }

        public void Dispose()
        {
            NotifierService.Notifier -= OnNotify;
        }
    }
}
