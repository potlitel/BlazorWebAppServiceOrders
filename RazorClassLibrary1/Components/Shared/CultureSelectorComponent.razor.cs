using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Globalization;


namespace RazorClassLibrary1.Components.Shared
{
	public partial class CultureSelectorComponent
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        //[Inject]
        //public IJSRuntime JSRuntime { get; set; }

        CultureInfo[] cultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("pt")
        };

        //https://blazorschool.com/tutorial/blazor-server/dotnet7/multilingual-website-697763
        //https://phrase.com/blog/posts/blazor-webassembly-i18n/
        //https://code-maze.com/localization-in-blazor-webassembly-applications/
        //https://github.com/CodeMazeBlog/blazor-wasm-localization
        CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    //var js = (IJSInProcessRuntime)jsRuntime;
                    //js.InvokeVoid("blazorCulture.set", value.Name);

                    jsRuntime.InvokeVoidAsync("blazorCulture.set", value.Name);


					NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
                }
            }
        }

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			//IServiceProvider serviceProvider = services.BuildServiceProvider();
			//var jsInterop = serviceProvider.GetRequiredService<IJSRuntime>();
			var result = await jsRuntime.InvokeAsync<string>("blazorCulture.get");

			CultureInfo culture;

			if (result != null)
				culture = new CultureInfo(result);
			else
				culture = new CultureInfo("en-US");

			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;
		}
	}
}
