using FSA.Core.ServiceOrders.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.Create;
using System;


namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class ServiceOrderDetailsComponent
    {
        [Parameter]
        public ServiceOrderDto ServiceOrder { get; set; } = new();
        
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
                {
                    SOStates = states.Data.ToList();
                    var random = new Random();
                    selectedIndex = random.Next(0, SOStates.Count());
                }
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

        async Task OnChange(int index)
        {
            var stateFrom = SOStates.ElementAt(selectedIndex);
            var stateTo = SOStates.ElementAt(index);

            //Console.WriteLine($"Step with index {index} was selected.");
            ServiceOrderRegisterDto serviceOrderRegisterDto = new ServiceOrderRegisterDto {
                Trigger = "custom trigger",
                StateFrom = stateFrom.Description,
                StateTo = stateTo.Description,
                Observations = "lorem ipsum",
                ServiceOrderId = ServiceOrder.Id,
            };


            var response = await CreateServiceOrderRegisterService.Handle(serviceOrderRegisterDto);
            NotificationService.ShowNotification(response.Succeeded,
                                                  $"Succesfully register state {index}",
                                                 ServiceOrder.Number);

            //NotificationService.ShowNotification(true,
            //                                     $"Succesfully register state {index}",
            //                                     ServiceOrder.Number);

            selectedIndex = index;
        }
    }
}
