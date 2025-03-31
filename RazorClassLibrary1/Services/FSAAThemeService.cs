using FSA.Cache.Models;
using Radzen;
using RazorClassLibrary1.Components.Helpers;

namespace RazorClassLibrary1.Services
{
    public interface IFSASOThemeService
    {
        Task Update();
    }
    internal class FSAAThemeService : IFSASOThemeService
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
                ThemeCache themeCache = await appCache.GetItem<ThemeCache>("FSASOWebTheme", CacheType.LocalStorage);
                if (themeCache != null)
                {
                    themeService.SetTheme(themeCache.theme);
                    return;
                }

                appCache.SetItem("FSASOWebTheme", new ThemeCache("material"), CacheType.LocalStorage, 0);
                themeService.SetTheme("material");
            }
            catch (Exception)
            {
                appCache.SetItem("FSASOWebTheme", new ThemeCache("material"), CacheType.LocalStorage, 0);
                themeService.SetTheme("material");
            }
        }
    }
}
