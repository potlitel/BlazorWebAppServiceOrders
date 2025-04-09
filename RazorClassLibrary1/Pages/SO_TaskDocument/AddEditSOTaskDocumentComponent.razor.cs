using Bogus;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Radzen;
using RazorClassLibrary1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RazorClassLibrary1.Pages.SO_TaskDocument
{
    public partial class AddEditSOTaskDocumentComponent
    {
        #region Parameters
        [Parameter, EditorRequired]
        public ServiceOrderTaskDocumentDto ServiceOrderTaskDocument { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        public bool IsCreateItem { get; set; } = false;
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
                await Task.CompletedTask;
                IsLoading = true;

                #region ToDelete-Bogus
                ////https://github.com/bchavez/Bogus
                ////https://medium.com/@fahrican.kcn/generating-mock-data-in-net-applications-a-comprehensive-guide-to-using-bogus-3fd60cc09f18#id_token=eyJhbGciOiJSUzI1NiIsImtpZCI6IjkxNGZiOWIwODcxODBiYzAzMDMyODQ1MDBjNWY1NDBjNmQ0ZjVlMmYiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTU2MzUxODk4NjQzOTMyNzIzMDIiLCJlbWFpbCI6InBvdGxpdGVsQGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYmYiOjE3NDE3MDc1MTMsIm5hbWUiOiJBbGFpbiBKb3JnZSIsInBpY3R1cmUiOiJodHRwczovL2xoMy5nb29nbGV1c2VyY29udGVudC5jb20vYS9BQ2c4b2NMajJCbE9zZmZsM21IQWUxREd3dGlkUFkxdDF5cEpMM3U5aWxuWGwzOHAzN0tzUkp2bz1zOTYtYyIsImdpdmVuX25hbWUiOiJBbGFpbiIsImZhbWlseV9uYW1lIjoiSm9yZ2UiLCJpYXQiOjE3NDE3MDc4MTMsImV4cCI6MTc0MTcxMTQxMywianRpIjoiM2UyMDYyZTY3MzJmNzBmODgzY2JjMjUzYjE0ZTcxM2RhNzI3YmY2NCJ9.GDPLzC40vJtA0hCz9whdNZSJ8DzPPJQcGeWQY1fBqPWoS3VVH0soqoYlvg_bAexkZkH1X9RiDd7CmJ02wvxQhQSJIdCMMUH2xUMsu5bqJOD3koiWQ7J44dBHMlyMwlZ18cIgZHMvRPgQUH-Z6yEp8viggoCG83mllEnTOH8DobsFQxqstYhMfmJix_THkIfuLRqxcxSlZxxJwvYEq4CxDwHS0Wx3cQbSL3sPWVpUx4OBfEOdafeLb_4Q2ISUSEZjOhbPJrliIyUBWhoNn0UF1I8UgWd3bzPpTutuK6NSpukoKAKC6I42U8U6pStb7V_3Dosp2YEHhNwz_KEkExzp7w
                //ServiceOrders = faker
                //                     .RuleFor(x => x.Number, f => f.Finance.Account(15))
                //                     .RuleFor(x => x.EstimatedEndingDate, f => f.Date.Future(1))
                //                     .RuleFor(x => x.Observations, f => f.Address.FullAddress())
                //                     .RuleFor(x => x.Address, f => f.Address.FullAddress())
                //                     .Generate(50).ToList();

                //DocumentTypes = fakerDoc
                //                     .RuleFor(x => x.Code, f => f.Finance.Bic())
                //                     .RuleFor(x => x.Description, f => f.Lorem.Sentence(8))
                //                     .Generate(15).ToList();

                //var policyGroupsTask = GetAllPolicyGroupsService.Handle(null);
                //var policyGroups = await policyGroupsTask;

                //if (policyGroups?.Succeeded == true)
                //{
                //    PolicyGroups = policyGroups.Data!.PolicyGroups.ToList();
                //    if (Policy.PolicyGroupId == 0)
                //        Policy.PolicyGroup = PolicyGroups.First();
                //}
                #endregion

                var documentTypesTask = GetAllDocumentTypesService.Handle(null);
                var documentTypes = await documentTypesTask;

                if (documentTypes)
                {
                    DocumentTypes = documentTypes.Data.ToList();
                    if (ServiceOrderTaskDocument.DocumentTypeId == 0)
                        ServiceOrderTaskDocument.DocumentType = DocumentTypes.First();
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
                //var item = _item as ServiceOrderDto;
                //ServiceOrderTaskDocument.ServiceOrder = item!;
            }
            catch (Exception)
            {
            }
        }

        async Task ChangeDocumentType(object _item)
        {
            try
            {
                await Task.CompletedTask;
                var item = _item as DocumentTypeDto;
                ServiceOrderTaskDocument.DocumentType = item!;
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
