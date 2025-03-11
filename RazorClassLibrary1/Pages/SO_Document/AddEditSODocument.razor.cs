using FSA.Management.Application.Features.Policies;
using FSA.Management.Application.Features.PolicyGroups;
using FSA.Razor.Components.Components.Shared;
using Microsoft.AspNetCore.Components;
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
        //private LogoComponent? Logo { get; set; }
        //IEnumerable<PolicyGroupDto> PolicyGroups = new List<PolicyGroupDto>() { };
        #endregion

        protected override async Task OnInitializedAsync()
        { 
        }
    }
}
