## Platform for displaying general user interfaces for managing service orders.

## Tech Stack

**UI:** [Radzen Blazor Components](https://blazor.radzen.com/?theme=material3)

**Build Tool:** [Visual Studio](https://visualstudio.microsoft.com/)

**Icons:** [Radzen Icons](https://blazor.radzen.com/icon?theme=material3)


## Features

- Light/dark mode
- Responsive
- Accessible
- With built-in Sidebar component
- Multi languages
- Extra custom components

## Run Locally

Clone the project

```bash
  git clone https://github.com/potlitel/BlazorWebAppServiceOrders.git
```

Go to the project directory

```bash
  cd BlazorWebAppServiceOrders
```

Install dependencies

```bash
  dotnet restore --verbosity normal
```

Start the server

```bash
  dotnet run --project BlazorWebApp/BlazorWebApp.csproj
```

---------------------------------------------------------
Fix error:
C:\Users\potli\source\repos\RCL_UI\RazorClassLibrary1\RazorClassLibrary1.csproj : warning NU1900: Error occurred while getting package vulnerability data: Response status code does not indicate success: 403
        (Forbidden - User 'f769d339-28da-4051-b12f-9373a2b17a97' lacks permission to complete this action. You need to have 'ReadPackages'. (DevOps Activity ID: 40275F17-7813-4683-B7CC-3634EBF96715)). [C:\Users\po
       tli\source\repos\RCL_UI\RCL_UI.sln]

<NoWarn>$(NoWarn);NU1900</NoWarn> on 

<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <Nullable>enable</Nullable>
  <ImplicitUsings>enable</ImplicitUsings>
	<NoWarn>$(NoWarn);NU1900</NoWarn>
</PropertyGroup>
 
 for project.
 ----------------------------------------------------

primera opcion
docker-compose -f "docker-compose.yml" up -d --build

segunda opcion

docker-compose -f "docker-compose.yml" build --build-arg FEED_ACCESSTOKEN="your-PAT"
