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




primera opcion
docker-compose -f "docker-compose.yml" up -d --build

segunda opcion

docker-compose -f "docker-compose.yml" build --build-arg FEED_ACCESSTOKEN="your-PAT"
