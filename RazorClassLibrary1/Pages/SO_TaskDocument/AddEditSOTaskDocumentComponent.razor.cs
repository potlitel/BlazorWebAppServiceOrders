using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
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
        #endregion
        public AddEditSOTaskDocumentComponent()
        {

        }
    }
}
