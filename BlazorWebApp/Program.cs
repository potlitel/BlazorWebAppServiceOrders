using BlazorWebApp.Components;
using FSA.Cache.Models;
using FSA.Razor.Components.Helper;
using FSA.Razor.Components.Services;
using Radzen;
using RazorClassLibrary1.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//builder.Services.AddRadzenComponents();//Radzen
builder.Services.ConfigureSOComponents(builder.Configuration);

builder.Services.AddFSASOCustomComponentsService();

//builder.Services.AddLocalization();

//IServiceProvider sp = builder.Services.BuildServiceProvider();
////await FSAThemeService.Update();
//using var scope = sp.CreateScope();
//var appCache = scope.ServiceProvider.GetRequiredService<IFSAAppCache>();
//var themeService = scope.ServiceProvider.GetRequiredService<ThemeService>();
//ThemeCache themeCache = await appCache.GetItem<ThemeCache>("FSAWebTheme", CacheType.LocalStorage);
//if (themeCache != null)
//{
//    themeService.SetTheme(themeCache.theme);
//    return;
//}

var app = builder.Build();

//await app.Services.InitialiseAppThemeAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

List<Assembly> _extraAssemblies = new List<Assembly>()
{
    typeof(RazorClassLibrary1._Imports).Assembly
};

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies([.. _extraAssemblies]);

app.Run();
