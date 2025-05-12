using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Pages.SO_Document
{
    public partial class SO_DocumentDetails
    {
        [Parameter]
        public ServiceOrderDocumentDto Document { get; set; } = new();

        [Parameter]
        public bool IsSideDialog { get; set; } = false;

        [Parameter]
        public string DocUrlViewer { get; set; }
    }
}
