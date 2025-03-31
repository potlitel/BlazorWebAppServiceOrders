using FSA.Management.Application.Features.Consortiums.Create;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Radzen;
using RazorClassLibrary1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FSA.Management.Application.Domain.Constants.Permissions;


namespace RazorClassLibrary1.Pages.Masters.DocumentType
{
    public partial class AddEditDocumentTypeComponent
    {
        #region Parameters
        [Parameter]
        public DocumentTypeDto DocumentType { get; set; } = new();
        [Parameter]
        public bool IsSideDialog { get; set; } = true;
        #endregion

        #region Properties
        private bool IsLoading { get; set; } = true;
        private bool Busy { get; set; } = false;

        #endregion
        public AddEditDocumentTypeComponent()
        {

        }

        async Task MasterSubmit()
        {
            await Task.CompletedTask;
            //TODO: Implement create/update actions
        }
    }
}
