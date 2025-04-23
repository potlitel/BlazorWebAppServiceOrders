using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;
using RazorClassLibrary1.Services;



namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class SO_StepsComponent
    {
        [CascadingParameter]
        public IEnumerable<ServiceOrderTaskStateDto> SOStates { get; set; }

        [CascadingParameter]
        public ServiceOrderDto ServiceOrder { get; set; } = new();

        int selectedIndex = 0;

        string SORegisterObservations = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var currentRegisterTask = GetCurrentSORegisterByIdService.Handle((int)ServiceOrder.Id);
                var currentRegister = await currentRegisterTask;

                if (currentRegister.Succeeded)
                {
                    //Get current register of this Service Order
                    var register = currentRegister.Data;

                    if (register is not null)
                    {
                        var index = SOStates.ToList().FindIndex(item => item.Description == register.StateTo);
                        selectedIndex = index;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">THe index</param>
        /// <returns></returns>
        async Task OnChange(int index)
        {
            await SaveServiceOrderRegister(index);
        }

        void Change(string text)
        {
            Console.WriteLine($"{text}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns></returns>
        private async Task SaveServiceOrderRegister(int index)
        {
            var stateFrom = SOStates.ElementAt(selectedIndex);
            var stateTo = SOStates.ElementAt(index);

            ServiceOrderRegisterDto serviceOrderRegisterDto = new ServiceOrderRegisterDto
            {
                Trigger = "custom trigger",
                StateFrom = stateFrom.Description,
                StateTo = stateTo.Description,
                Observations = SORegisterObservations,
                ServiceOrderId = ServiceOrder.Id,
            };


            var response = await CreateServiceOrderRegisterService.Handle(serviceOrderRegisterDto);

            if (response.Succeeded)
            {
                NotificationService.ShowNotification(response.Succeeded,
                                                     Localizer["UpdSORegister"] + " " + stateTo.Description,
                                                     ServiceOrder.Number);

                selectedIndex = index;
                SORegisterObservations = string.Empty;

                await NotifierService.SendNotification(NotificationsKeys.UpdateRegistersList, response!);
            }
        }
    }
}
