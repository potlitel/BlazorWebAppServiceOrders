using FSA.Cache.Models;
using FSA.Razor.Components.Helper;
using FSA.Razor.Components.Services;
using Radzen;

namespace RazorClassLibrary1.Services
{
    internal class FSAAThemeService : IFSAThemeService
    {
        private readonly IFSAAppCache appCache;

        private readonly ThemeService themeService;

        public FSAAThemeService(IFSAAppCache appCache, ThemeService themeService)
        {
            this.appCache = appCache;
            this.themeService = themeService;
        }

        public async Task Update()
        {
            try
            {
                ThemeCache themeCache = await appCache.GetItem<ThemeCache>("FSAWebTheme", CacheType.LocalStorage);
                if (themeCache != null)
                {
                    themeService.SetTheme(themeCache.theme);
                    return;
                }

                appCache.SetItem("FSAWebTheme", new ThemeCache("material"), CacheType.LocalStorage, 0);
                themeService.SetTheme("material");
            }
            catch (Exception)
            {
                appCache.SetItem("FSAWebTheme", new ThemeCache("material"), CacheType.LocalStorage, 0);
                themeService.SetTheme("material");
            }
        }
    }
}
