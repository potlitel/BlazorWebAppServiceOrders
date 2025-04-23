using Microsoft.AspNetCore.Components;
using RazorClassLibrary1.Dtos;


namespace RazorClassLibrary1.Pages.SO_Details
{
    public partial class SO_BasicInfoComponent
    {
        [CascadingParameter]
        public ServiceOrderDto ServiceOrder { get; set; } = new();
    }
}
