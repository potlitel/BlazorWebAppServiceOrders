using Bogus;
using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Pages.SO_Task
{
    public partial class AddEditServiceOrderTaskComponent
    {
        #region Parameters
        [Parameter, EditorRequired]
        public ServiceOrderTaskDto ServiceOrderTask { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        #endregion

        #region Properties
        private bool IsLoading { get; set; } = true;
        private bool Busy { get; set; } = false;
        public bool IsCreateItem { get; set; } = false;

        IEnumerable<ServiceOrderDto> ServiceOrders = [];

        IEnumerable<ServiceOrderTaskStateDto> States = [];

        Faker<ServiceOrderDto> faker = new();

        Faker<ServiceOrderTaskStateDto> fakerDoc = new();
        List<DateTime> dates = [];
        #endregion

        void DateRender(DateRenderEventArgs args)
        {
            var special = dates.Select(d => d.Date).Contains(args.Date.Date);
            if (special)
            {
                args.Attributes.Add("style", "background-color: #fafbfd; border-color: white;");
            }

            args.Disabled = special || args.Disabled || args.Date.DayOfWeek == DayOfWeek.Sunday || args.Date.DayOfWeek == DayOfWeek.Saturday;
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Task.CompletedTask;
                IsLoading = true;

                DateTime startDate = new DateTime(1900, 1, 25);
                int days = (DateTime.Now - startDate).Days + 1;
                for (int i = 1; i < days; i++)
                    dates.Add(DateTime.Today.AddDays(-i));

                var serviceOrdersTask = GetAllServiceOrderService.Handle(null);
                var statesTask = GetAllServiceOrderTasksStatesService.Handle(null);
                
                var serviceOrders = await serviceOrdersTask;
                var states = await statesTask;

                if (serviceOrders.Succeeded) 
                {
                    ServiceOrders = serviceOrders.Data!.ToList();
                    if (ServiceOrderTask.ServiceOrderId == 0)
                        ServiceOrderTask.ServiceOrder = ServiceOrders.First();
                }

                if (states.Succeeded) 
                {
                    States = states.Data.ToList();
                    if (ServiceOrderTask.ServiceOrderTaskStateId == 0)
                        ServiceOrderTask.ServiceOrderTaskState = States.First();
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                NotificationService.ShowNotification(NotificationSeverity.Error, ex.Message, Localizer["ErrorPolicy"]);
            }
            finally
            {
                IsLoading = false;
            }
        }

        async Task ChangeServiceOrder(object _item)
        {
            try
            {
                await Task.CompletedTask;
                var item = _item as ServiceOrderDto;
                ServiceOrderTask.ServiceOrder = item!;
            }
            catch (Exception)
            {
            }
        }

        async Task ChangeState(object _item)
        {
            try
            {
                await Task.CompletedTask;
                var item = _item as ServiceOrderTaskStateDto;
                ServiceOrderTask.ServiceOrderTaskState = item!;
            }
            catch (Exception)
            {
            }
        }

        protected async Task Submit()
        {
            try
            {
                await Task.CompletedTask;
                Busy = true;

                ServiceOrderTask.ServiceOrderId = ServiceOrderTask.ServiceOrder!.Id;
                ServiceOrderTask.ServiceOrderTaskStateId = ServiceOrderTask.ServiceOrderTaskState!.Id;
                //ServiceOrderTask.CustomFieldSOTask = "custom field";

                if (ServiceOrderTask.Id == 0)
                {
                    var response = await CreateServiceOrderTasksService.Handle(ServiceOrderTask);
                    NotificationService.ShowNotification(response.Succeeded,
                                                         response.StatusCode.ToString(),
                                                         ServiceOrderTask.Observations!);
                    CloseDialog(response.Succeeded);
                }
                else
                {
                    var response = await UpdateServiceOrderTasksService.Handle(ServiceOrderTask);
                    NotificationService.ShowNotification(response.Succeeded,
                                                        response.StatusCode.ToString(),
                                                        ServiceOrderTask.Observations!);
                    CloseDialog(response.Succeeded);
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex)
            {
                NotificationService.ShowNotification(NotificationSeverity.Error, ex.Message, Localizer["ErrorPolicy"]);
            }
            finally
            {
                Busy = false;
            }
        }

        void CloseDialog(bool result)
        {
            if (IsSideDialog)
                DialogService.CloseSide(result);
            else
                DialogService.Close(result);
        }

        protected void Cancel()
        {
            CloseDialog(false);
        }
    }
}
