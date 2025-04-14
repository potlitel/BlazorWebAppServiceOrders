using FSA.Core.ServiceOrders.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;
using System;


namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class ServiceOrderDetailsComponent
    {
        [Parameter]
        public ServiceOrderDto ServiceOrder { get; set; } = null!;
        
        [Parameter]
        public bool IsSideDialog { get; set; } = false;

        int selectedIndex = 0;

        IEnumerable<ServiceOrderTaskStateDto> SOStates = [];

        //RenderFragment DisplayValue(string value)
        //{
        //    return @<p>@value</p>;
        //}

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var statesTask = GetAllServiceOrderTasksStatesService.Handle(null);
                var states = await statesTask;

                if (states.Succeeded)
                    SOStates = states.Data.ToList();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                NotificationService.ShowNotification(NotificationSeverity.Error, ex.Message, Localizer["ErrorPolicy"]);
            }
            finally
            {
                //IsLoading = false;
            }
        }

        void OnChange(int index)
        {
            //Console.WriteLine($"Step with index {index} was selected.");
            NotificationService.ShowNotification(true,
                                                 $"Succesfully register state {index}",
                                                 ServiceOrder.Number);
        }
    }
}
