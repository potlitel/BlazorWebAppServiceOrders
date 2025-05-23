﻿using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;


namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class ServiceOrderDetailsComponent
    {
        [Parameter]
        public ServiceOrderDto ServiceOrder { get; set; } = new();
        
        [Parameter]
        public bool IsSideDialog { get; set; } = false;

        int selectedIndex = 0;

        string SORegisterObservations = string.Empty;

        IEnumerable<ServiceOrderTaskStateDto> SOStates = [];

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var statesTask = GetAllServiceOrderTasksStatesService.Handle(null);
                int soId = (int)ServiceOrder.Id;
                //var currentRegisterTask = GetCurrentSORegisterByIdService.Handle(soId);

                var states = await statesTask;
                //var currentRegister = await currentRegisterTask;

                if (states.Succeeded)
                {
                    SOStates = states.Data.ToList();
                    var random = new Random();
                }

                //if (currentRegister.Succeeded)
                //{
                //    //Get current register of this Service Order
                //    var register = currentRegister.Data;

                //    if (register is not null)
                //    {
                //        var index = SOStates.ToList().FindIndex(item => item.Description == register.StateTo);
                //        selectedIndex = index;
                //    }
                //}
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
    }
}
