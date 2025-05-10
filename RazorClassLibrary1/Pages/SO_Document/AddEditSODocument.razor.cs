using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using RazorClassLibrary1.Dtos;
using System;

namespace RazorClassLibrary1.Pages.SO_Document
{
    public partial class AddEditSODocument
    {
        #region Parameters
        [Parameter, EditorRequired]
        public ServiceOrderDocumentDto ServiceOrderDocument { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = false;

        public bool Chosse { get; set; } = false;
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

                var serviceOrdersTask = GetAllServiceOrderService.Handle(null);
                var documentTypesTask = GetAllDocumentTypesService.Handle(null);

                var serviceOrders = await serviceOrdersTask;
                var documentTypes = await documentTypesTask;

                if (serviceOrders.Succeeded)
                {
                    ServiceOrders = serviceOrders.Data!.ToList();
                    if (ServiceOrderDocument.ServiceOrderId == 0)
                        ServiceOrderDocument.ServiceOrder = ServiceOrders.First();
                }

                if (documentTypes)
                {
                    DocumentTypes = documentTypes.Data.ToList();
                    if (ServiceOrderDocument.DocumentTypeId == 0)
                        ServiceOrderDocument.DocumentType = DocumentTypes.First();
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
                await Task.CompletedTask;
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
                Busy = true;
                ServiceOrderDocument.ServiceOrderId = ServiceOrderDocument.ServiceOrder.Id;
                ServiceOrderDocument.DocumentTypeId = ServiceOrderDocument.DocumentType.Id;

                var response = await CreateServiceOrderDocumentService.Handle(ServiceOrderDocument);
                NotificationService.ShowNotification(response.Succeeded,
                                                     response.StatusCode.ToString(),
                                                     ServiceOrderDocument.Name);
                CloseDialog(response.Succeeded);
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

        //private async Task OnChange(InputFileChangeEventArgs e)
        //{
        //    long MaxAllowedSize = (long)Math.Pow(1048576, 1);
        //    try
        //    {
        //        Chosse = true;
        //        var stream = e.File.OpenReadStream(MaxAllowedSize);
        //        ServiceOrderDocument.Name = e.File.Name;
        //        using var ms = new MemoryStream();
        //        await stream.CopyToAsync(ms);
        //        ServiceOrderDocument.File = ms.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        NotificationService.ShowNotification(NotificationSeverity.Error, "DOC_ERROR", ex.Message);
        //    }
        //}

        //void OnChange(byte[] bytes, string name)
        //{
        //    //console.Log($"{name} value changed");
        //    ServiceOrderDocument.Name = name;
        //    ServiceOrderDocument.File = bytes;
        //}

        //void OnError(UploadErrorEventArgs args, string name)
        //{
        //    NotificationService.ShowNotification(NotificationSeverity.Error, "DOC_ERROR", args.Message);
        //}

        async Task OnChange(UploadChangeEventArgs args)
        {
            long MaxAllowedSize = (long)Math.Pow(1048576, 2);
            try
            {
                if (args.Files.Any())
                {
                    Chosse = true;
                    var stream = args.Files.ElementAt(0).OpenReadStream(MaxAllowedSize);
                    ServiceOrderDocument.Name = args.Files.ElementAt(0).Name;
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    ServiceOrderDocument.File = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                NotificationService.ShowNotification(NotificationSeverity.Error, "DOC_ERROR", ex.Message);
            }
        }
    }
}
