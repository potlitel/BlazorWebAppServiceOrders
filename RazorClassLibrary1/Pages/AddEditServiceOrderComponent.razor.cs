﻿using Bogus;
using FSA.Core.ServiceOrders.Models;
using FSA.Management.Application.Features.CompanyGroups.GetAll;
using FSA.Management.Application.Features.CompanyTypes.GetAll;
using FSA.Management.Application.Features.Users;
using FSA.Management.Application.Features.Users.CreateAdminEntity;
using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages
{
    public partial class AddEditServiceOrderComponent
    {
        #region Parameters
        [Parameter, EditorRequired]
        public ServiceOrderDto ServiceOrder { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;
        #endregion

        #region Properties
        private bool IsLoading { get; set; } = true;
        private bool Busy { get; set; } = false;
        public bool IsCreateItem { get; set; } = false;

        IEnumerable<ServiceOrderDto> ServiceOrdersParents = [];

        IEnumerable<ServiceOrderTypeDto> Types = [];
        IEnumerable<UserDto> Owners = [];
        IEnumerable<UserDto> Executors = [];

        Faker<ServiceOrderDto> faker = new();

        Faker<ServiceOrderTypeDto> fakerDoc = new();

        Faker<UserDto> fakerUsrs = new();
        #endregion
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Task.CompletedTask;
                IsLoading = true;

                #region Bogus-ToDelete

                //https://github.com/bchavez/Bogus
                //https://medium.com/@fahrican.kcn/generating-mock-data-in-net-applications-a-comprehensive-guide-to-using-bogus-3fd60cc09f18#id_token=eyJhbGciOiJSUzI1NiIsImtpZCI6IjkxNGZiOWIwODcxODBiYzAzMDMyODQ1MDBjNWY1NDBjNmQ0ZjVlMmYiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTU2MzUxODk4NjQzOTMyNzIzMDIiLCJlbWFpbCI6InBvdGxpdGVsQGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYmYiOjE3NDE3MDc1MTMsIm5hbWUiOiJBbGFpbiBKb3JnZSIsInBpY3R1cmUiOiJodHRwczovL2xoMy5nb29nbGV1c2VyY29udGVudC5jb20vYS9BQ2c4b2NMajJCbE9zZmZsM21IQWUxREd3dGlkUFkxdDF5cEpMM3U5aWxuWGwzOHAzN0tzUkp2bz1zOTYtYyIsImdpdmVuX25hbWUiOiJBbGFpbiIsImZhbWlseV9uYW1lIjoiSm9yZ2UiLCJpYXQiOjE3NDE3MDc4MTMsImV4cCI6MTc0MTcxMTQxMywianRpIjoiM2UyMDYyZTY3MzJmNzBmODgzY2JjMjUzYjE0ZTcxM2RhNzI3YmY2NCJ9.GDPLzC40vJtA0hCz9whdNZSJ8DzPPJQcGeWQY1fBqPWoS3VVH0soqoYlvg_bAexkZkH1X9RiDd7CmJ02wvxQhQSJIdCMMUH2xUMsu5bqJOD3koiWQ7J44dBHMlyMwlZ18cIgZHMvRPgQUH-Z6yEp8viggoCG83mllEnTOH8DobsFQxqstYhMfmJix_THkIfuLRqxcxSlZxxJwvYEq4CxDwHS0Wx3cQbSL3sPWVpUx4OBfEOdafeLb_4Q2ISUSEZjOhbPJrliIyUBWhoNn0UF1I8UgWd3bzPpTutuK6NSpukoKAKC6I42U8U6pStb7V_3Dosp2YEHhNwz_KEkExzp7w
                //ServiceOrders = faker
                //                     .RuleFor(x => x.Number, f => f.Finance.Account(15))
                //                     .RuleFor(x => x.EstimatedEndingDate, f => f.Date.Future(1))
                //                     .RuleFor(x => x.Observations, f => f.Address.FullAddress())
                //                     .RuleFor(x => x.Address, f => f.Address.FullAddress())
                //                     .Generate(50).ToList();

                //Types = fakerDoc
                //                     .RuleFor(x => x.Code, f => f.Finance.Account(15))
                //                     .RuleFor(x => x.Description, f => f.Address.FullAddress())
                //                     .Generate(50).ToList();
                #endregion

                var serviceOrdersTask = GetAllServiceOrderService.Handle(null);
                var serviceOrdersTypesTask = GetAllServiceOrderTypesService.Handle(null);

                var serviceOrders = await serviceOrdersTask;
                var serviceOrdersTypes = await serviceOrdersTypesTask;

                if (serviceOrders.Succeeded)
                {
                    ServiceOrdersParents = serviceOrders.Data!.ToList();
                    //if (ServiceOrder.ParentServiceOrderId == 0 || ServiceOrder.ParentServiceOrderId is null)
                    //    ServiceOrder.ParentServiceOrder = ServiceOrdersParents.First();
                    //else
                    //{
                    if (ServiceOrder.Id != 0 && ServiceOrder.ParentServiceOrderId != null) //Edit case
                    {
                        var parent = ServiceOrdersParents.FirstOrDefault(item => item.Id == ServiceOrder.ParentServiceOrderId);
                        ServiceOrder.ParentServiceOrder = parent;
                    }
                    //}
                }

                if (serviceOrdersTypes.Succeeded)
                {
                    Types = serviceOrdersTypes.Data.ToList();
                    if (ServiceOrder.ServiceOrderTypeId == 0)
                        ServiceOrder.ServiceOrderType = Types.First();
                    else {
                        var type = Types.FirstOrDefault(item => item.Id == ServiceOrder.ServiceOrderTypeId);
                        ServiceOrder.ServiceOrderType = type;
                    }
                }


                Owners = fakerUsrs
                                     .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                                     .RuleFor(x => x.LastName, f => f.Person.LastName)
                                     .Generate(50).ToList();

                Executors = fakerUsrs
                                     .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                                     .RuleFor(x => x.LastName, f => f.Person.LastName)
                                     .Generate(50).ToList();
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
                await Task.FromResult(ServiceOrder.ParentServiceOrder = item!);
            }
            catch (Exception)
            {
            }
        }

        async Task ChangeType(object _item)
        {
            try
            {
                var item = _item as ServiceOrderTypeDto;
                await Task.FromResult(ServiceOrder.ServiceOrderType = item!);
            }
            catch (Exception)
            {
            }
        }

        async Task ChangeOwners(object _item)
        {
            try
            {
                //await Task.CompletedTask;
                var item = _item as UserDto;
                //UserDto item = new UserDto();
                //item.Id = 55;
                await Task.FromResult(ServiceOrder.Owner = item!);
            }
            catch (Exception)
            {
            }
        }

        async Task ChangeExecutors(object _item)
        {
            try
            {
                var item = _item as UserDto;
                await Task.FromResult(ServiceOrder.Executor = item!);
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

                ServiceOrder.ExecutorId = 18;
                ServiceOrder.OwnerId = 54;
                ServiceOrder.ParentServiceOrderId = ServiceOrder.ParentServiceOrder?.Id; //using null propagation.
                ServiceOrder.ServiceOrderTypeId = ServiceOrder.ServiceOrderType!.Id;


                if (ServiceOrder.Id == 0)
                {
                    Busy = true;

                    var response = await CreateServiceOrderService.Handle(ServiceOrder);
                    NotificationService.ShowNotification(response.Succeeded,
                                                         response.StatusCode.ToString(),
                                                         ServiceOrder.Number);

                    CloseDialog(response.Succeeded);
                }
                else 
                {
                    //Put Update logic here!!!
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
