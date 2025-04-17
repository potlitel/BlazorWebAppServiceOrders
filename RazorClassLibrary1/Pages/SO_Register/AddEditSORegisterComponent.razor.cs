using Bogus;
using Microsoft.AspNetCore.Components;
using Radzen;
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

        #region Properties
        private bool IsLoading { get; set; } = true;
        private bool Busy { get; set; } = false;

        public bool IsCreateItem { get; set; } = false;

        IEnumerable<ServiceOrderDto> ServiceOrders = [];

        IEnumerable<DocumentTypeDto> DocumentTypes = [];

        Faker<ServiceOrderDto> faker = new();

        Faker<DocumentTypeDto> fakerDoc = new();
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Task.CompletedTask;
                IsLoading = true;

                var serviceOrdersTask = GetAllServiceOrderService.Handle(null);
                var serviceOrders = await serviceOrdersTask;

                if (serviceOrders.Succeeded)
                {
                    ServiceOrders = serviceOrders.Data!.ToList();
                    if (ServiceOrderRegister.ServiceOrderId == 0)
                        ServiceOrderRegister.ServiceOrder = ServiceOrders.First();
                }

                DocumentTypes = fakerDoc
                                     .RuleFor(x => x.Code, f => f.Finance.Bic())
                                     .RuleFor(x => x.Description, f => f.Lorem.Sentence(8))
                                     .Generate(15).ToList();

                //var policyGroupsTask = GetAllPolicyGroupsService.Handle(null);
                //var policyGroups = await policyGroupsTask;

                //if (policyGroups?.Succeeded == true)
                //{
                //    PolicyGroups = policyGroups.Data!.PolicyGroups.ToList();
                //    if (Policy.PolicyGroupId == 0)
                //        Policy.PolicyGroup = PolicyGroups.First();
                //}
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
                ServiceOrderRegister.ServiceOrder = item!;
            }
            catch (Exception)
            {
            }
        }

        //async Task ChangeDocumentType(object _item)
        //{
        //    try
        //    {
        //        var item = _item as DocumentTypeDto;
        //        ServiceOrderDocument.DocumentType = item!;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        protected async Task Submit()
        {
            try
            {
                await Task.CompletedTask;
                //Busy = true;
                //Policy.PolicyGroupId = Policy.PolicyGroup.Id;

                //if (Policy.Id == 0)
                //{
                //    var response = await CreatePolicyService.Handle(Policy);
                //    NotificationService.ShowNotification(response.Succeeded,
                //                                         response.StatusCode.ToString(),
                //                                         Policy.Code);
                //    CloseDialog(response.Succeeded);
                //}
                //else
                //{
                //    var response = await UpdatePolicyService.Handle(Policy);
                //    NotificationService.ShowNotification(response.Succeeded,
                //                                        response.StatusCode.ToString(),
                //                                        Policy.Code);
                //    CloseDialog(response.Succeeded);
                //}
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
