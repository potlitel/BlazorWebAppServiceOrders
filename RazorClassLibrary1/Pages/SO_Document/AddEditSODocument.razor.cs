using Bogus;
using FSA.Core.ServiceOrders.Models;
using FSA.Core.ServiceOrders.Models.Masters;
using FSA.Management.Application.Domain.Constants;
using FSA.Management.Application.Features.Policies;
using FSA.Management.Application.Features.Policies.Create;
using FSA.Management.Application.Features.Policies.Update;
using FSA.Management.Application.Features.PolicyGroups;
using FSA.Management.Application.Features.PolicyGroups.GetAll;
using FSA.Razor.Components.Components.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Pages.SO_Document
{
    public partial class AddEditSODocument
    {
        #region Parameters
        [Parameter, EditorRequired]
        public ServiceOrderDocumentDto ServiceOrderDocument { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        #endregion

        #region Properties
        private bool IsLoading { get; set; } = true;
        private bool Busy { get; set; } = false;

        IEnumerable<ServiceOrderDto> ServiceOrders = [];

        IEnumerable<DocumentTypeDto> DocumentTypes = [];

        Faker<ServiceOrderDto> faker = new();

        Faker<DocumentTypeDto> fakerDoc = new();
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;

                //https://github.com/bchavez/Bogus
                ServiceOrders = faker
                                     .RuleFor(x => x.Number, f => f.Finance.Account(15))
                                     .RuleFor(x => x.EstimatedEndingDate, f => f.Date.Future(1))
                                     .RuleFor(x => x.Observations, f => f.Address.FullAddress())
                                     .RuleFor(x => x.Address, f => f.Address.FullAddress())
                                     .Generate(50).ToList();

                DocumentTypes =       fakerDoc
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
                var item = _item as ServiceOrderDto;
                ServiceOrderDocument.ServiceOrder = item!;
            }
            catch (Exception)
            {
            }
        }

        async Task ChangeDocumentType(object _item)
        {
            try
            {
                var item = _item as DocumentTypeDto;
                ServiceOrderDocument.DocumentType = item!;
            }
            catch (Exception)
            {
            }
        }

        protected async Task Submit()
        {
            try
            {
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
