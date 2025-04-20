using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using RazorClassLibrary1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RazorClassLibrary1.Pages.SO_Register
{
    public partial class ServiceOrderRegisterDetailsComponent
    {
        [Parameter]
        public ServiceOrderRegisterDto ServiceOrderRegister { get; set; } = new();

        [Parameter]
        public bool IsSideDialog { get; set; } = false;
    }
}
