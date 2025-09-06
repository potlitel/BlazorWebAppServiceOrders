# 🖥️ Platform for displaying general user interfaces for managing service orders.

## 🛠️ Tech Stack
**UI:** [Radzen Blazor Components](https://blazor.radzen.com/?theme=material3) 🎨  
**Build Tool:** [Visual Studio](https://visualstudio.microsoft.com/) 💻  
**Icons:** [Radzen Icons](https://blazor.radzen.com/icon?theme=material3) 🧩  

## ✨ Features
- 🌔 Light/dark mode
- 📱 Responsive
- ♿ Accessible
- 🧭 With built-in Sidebar component
- 🌐 Multi languages
- 🛠️ Extra custom components
- 📑 Pdf viewer
- 🗃️ Blog storage

## 🚀 Run Locally

**Clone the project**

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


## 🛠️ Fix warning error

> warning NU1900: Error occurred while getting package vulnerability data: Response status code does not indicate success: 403 (Forbidden - User 'f769d339-28da-4051-b12f-9373a2b17a97' lacks permission to complete this action. You need to have 'ReadPackages'.)

Open the corresponding project file and add the following statement for each warning:

```bash
  <NoWarn>$(NoWarn);NU1900</NoWarn>
```
in the section 

```bash
  <PropertyGroup>
```

remaining as follows:

```bash
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <Nullable>enable</Nullable>
  <ImplicitUsings>enable</ImplicitUsings>
	<NoWarn>$(NoWarn);NU1900</NoWarn>
</PropertyGroup>
```

## 🐳Docker deployment

- docker-compose -f "docker-compose.yml" up -d --build
  
  > 📛Use this command if you have PAT embedded in the docker file (not secure)

Passing the PAT via args

- Build the image using the follow command: 

    ```bash
  docker-compose -f "docker-compose.yml" build --build-arg FEED_ACCESSTOKEN="your-PAT"
    ```    
- Run container as follow: 
    
    ```bash
  docker-compose -f "docker-compose.yml" up -d
    ```
    
- 🌐Navigate to: https://server-name/
