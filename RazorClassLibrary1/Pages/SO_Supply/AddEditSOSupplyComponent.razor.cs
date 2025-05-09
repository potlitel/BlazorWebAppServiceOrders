﻿using Bogus;
using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.GetAll;
using RazorClassLibrary1.Services.HttpClientSrv.Supplies.Create;
using RazorClassLibrary1.Services.HttpClientSrv.Supplies.Update;


namespace RazorClassLibrary1.Pages.SO_Supply
{
    public partial class AddEditSOSupplyComponent
    {
        #region Parameters
        [Parameter, EditorRequired]
        public SupplyDto Supply { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        #endregion

        #region Properties
        private bool IsLoading { get; set; } = true;
        public bool IsCreateItem { get; set; } = false;
        private bool Busy { get; set; } = false;

        IEnumerable<SupplyOperationDto> SupplyOperations = [];

        IEnumerable<ServiceOrderTaskDto> ServiceOrderTasks = [];

        Faker<ServiceOrderDto> faker = new();

        Faker<DocumentTypeDto> fakerDoc = new();
        #endregion

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Task.CompletedTask;
                IsLoading = true;

                //https://github.com/bchavez/Bogus
                //https://medium.com/@fahrican.kcn/generating-mock-data-in-net-applications-a-comprehensive-guide-to-using-bogus-3fd60cc09f18#id_token=eyJhbGciOiJSUzI1NiIsImtpZCI6IjkxNGZiOWIwODcxODBiYzAzMDMyODQ1MDBjNWY1NDBjNmQ0ZjVlMmYiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTU2MzUxODk4NjQzOTMyNzIzMDIiLCJlbWFpbCI6InBvdGxpdGVsQGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYmYiOjE3NDE3MDc1MTMsIm5hbWUiOiJBbGFpbiBKb3JnZSIsInBpY3R1cmUiOiJodHRwczovL2xoMy5nb29nbGV1c2VyY29udGVudC5jb20vYS9BQ2c4b2NMajJCbE9zZmZsM21IQWUxREd3dGlkUFkxdDF5cEpMM3U5aWxuWGwzOHAzN0tzUkp2bz1zOTYtYyIsImdpdmVuX25hbWUiOiJBbGFpbiIsImZhbWlseV9uYW1lIjoiSm9yZ2UiLCJpYXQiOjE3NDE3MDc4MTMsImV4cCI6MTc0MTcxMTQxMywianRpIjoiM2UyMDYyZTY3MzJmNzBmODgzY2JjMjUzYjE0ZTcxM2RhNzI3YmY2NCJ9.GDPLzC40vJtA0hCz9whdNZSJ8DzPPJQcGeWQY1fBqPWoS3VVH0soqoYlvg_bAexkZkH1X9RiDd7CmJ02wvxQhQSJIdCMMUH2xUMsu5bqJOD3koiWQ7J44dBHMlyMwlZ18cIgZHMvRPgQUH-Z6yEp8viggoCG83mllEnTOH8DobsFQxqstYhMfmJix_THkIfuLRqxcxSlZxxJwvYEq4CxDwHS0Wx3cQbSL3sPWVpUx4OBfEOdafeLb_4Q2ISUSEZjOhbPJrliIyUBWhoNn0UF1I8UgWd3bzPpTutuK6NSpukoKAKC6I42U8U6pStb7V_3Dosp2YEHhNwz_KEkExzp7w

                var supplyOperationsTask = GetAllSupplyOperationsService.Handle(null);
                var serviceOrdersTasksTask = GetAllServiceOrdersTasksService.Handle(null);

                var supplyOperations = await supplyOperationsTask;
                var serviceOrdersTasks = await serviceOrdersTasksTask;

                if (supplyOperations?.Succeeded == true)
                {
                    SupplyOperations = supplyOperations.Data!.ToList();
                    if (Supply.SupplyOperationId == 0)
                        Supply.SupplyOperation = SupplyOperations.First();
                }

                if (serviceOrdersTasks?.Succeeded == true)
                {
                    ServiceOrderTasks = serviceOrdersTasks.Data!.ToList();
                    if (Supply.ServiceOrderTaskId == 0)
                        Supply.ServiceOrderTask = ServiceOrderTasks.First();
                }

                #region ToDelete

                //SupplyOperations = new Faker<SupplyOperationDto>()
                //                        .RuleFor(x => x.Code, f => f.Finance.Account(15))
                //                        .RuleFor(x => x.Description, f => f.Address.FullAddress())
                //                        .Generate(50).ToList();

                //ServiceOrderTasks = new Faker<ServiceOrderTaskDto>()
                //                        .RuleFor(x => x.Observations, f => f.Address.FullAddress())
                //                        .Generate(50).ToList();

                //var policyGroupsTask = GetAllPolicyGroupsService.Handle(null);
                //var policyGroups = await policyGroupsTask;

                //if (policyGroups?.Succeeded == true)
                //{
                //    PolicyGroups = policyGroups.Data!.PolicyGroups.ToList();
                //    if (Policy.PolicyGroupId == 0)
                //        Policy.PolicyGroup = PolicyGroups.First();
                //}

                #endregion
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

        async Task ChangeSupplyOperation(object _item)
        {
            try
            {
                await Task.CompletedTask;
                var item = _item as SupplyOperationDto;
                Supply.SupplyOperation = item!;
            }
            catch (Exception)
            {
            }
        }

        async Task ChangeServiceOrderTask(object _item)
        {
            try
            {
                await Task.CompletedTask;
                var item = _item as ServiceOrderTaskDto;
                Supply.ServiceOrderTask = item!;
            }
            catch (Exception)
            {
            }
        }

        protected async Task Submit()
        {
            try
            {
                Busy = true;
                Supply.SupplyOperationId = Supply.SupplyOperation.Id;
                Supply.ServiceOrderTaskId = Supply.ServiceOrderTask.Id;

                if (Supply.Id == 0)
                {
                    var response = await CreateSupplyService.Handle(Supply);
                    NotificationService.ShowNotification(response.Succeeded,
                                                         response.StatusCode.ToString(),
                                                         Supply.Description);
                    CloseDialog(response.Succeeded);
                }
                else
                {
                    var response = await UpdateSupplyService.Handle(Supply);
                    NotificationService.ShowNotification(response.Succeeded,
                                                        response.StatusCode.ToString(),
                                                        Supply.Description);
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
