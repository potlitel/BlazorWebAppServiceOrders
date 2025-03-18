using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.RegularExpressions;


namespace RazorClassLibrary1.Components.Shared
{
    public partial class CultureSelectorComponent
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        private string _selectedCountryCode;
        private HashSet<string> CountryCodes = new HashSet<string> { "US", "PT" };

        private List<Country> Countries => CountryCodes
            .Select(x => new Country
            {
                CountryCode = x,
                FlagEmoji = IsoCountryCodeToFlagEmoji(x)
            })
            .ToList();

        private string IsoCountryCodeToFlagEmoji(string countryCode) =>
            string.Concat(countryCode.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));

        public class Country
        {
            public string CountryCode { get; set; } = string.Empty;
            public string FlagEmoji { get; set; } = string.Empty;
        }

        //[Inject]
        //public IJSRuntime JSRuntime { get; set; }

        CultureInfo[] cultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("pt-PT")
        };

        //https://blazorschool.com/tutorial/blazor-server/dotnet7/multilingual-website-697763
        //https://phrase.com/blog/posts/blazor-webassembly-i18n/
        //https://code-maze.com/localization-in-blazor-webassembly-applications/
        //https://github.com/CodeMazeBlog/blazor-wasm-localization

        //https://stackoverflow.com/questions/73944918/adding-a-img-in-a-dropdown-menu
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

        protected override async Task OnInitializedAsync()
        {
            _selectedCountryCode = (Culture.Name == "en-US") ? "US" : "PT";
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

            _selectedCountryCode = (Culture.Name == "en-US") ? "US" : "PT";
        }

        async Task languageChange() 
        {
            if (CultureInfo.CurrentCulture.Name != _selectedCountryCode)
            {
                jsRuntime.InvokeVoidAsync("blazorCulture.set", _selectedCountryCode);
                NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
            }
        }
	}
}
