#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG FEED_ACCESSTOKEN
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG FEED_ACCESSTOKEN
#USER app
#WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BlazorWebApp/BlazorWebApp.csproj", "BlazorWebApp/"]
COPY ["RazorClassLibrary1/RazorClassLibrary1.csproj", "RazorClassLibrary1/"]
#COPY ./nuget.config .

RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | sh
COPY ./nuget.config .
COPY ./MapsApiSSLCert.pfx https/

#ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS="{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/fsaltdaqqp/_packaging/FSA.NugetPackages%40Local/nuget/v3/index.json\", \"username\":\"blazorwebapp\", \"password\":\"8P8VPVpwD4TGnj0dizQR2eGaVUz9KNIjNrJWAiMIPvk4thrkmAt8JQQJ99BCACAAAAAgJncgAAASAZDO17GJ\"}]}"

ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS="{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/fsaltdaqqp/_packaging/FSA.NugetPackages%40Local/nuget/v3/index.json\", \"username\":\"docker\", \"password\":\"6xqkiqZd4aMMhxwqRliozxY5Z7qtDQ3edkLKBvwj1XqPy16K6IMhJQQJ99BCACAAAAAgJncgAAASAZDO2wAw\"}]}"
# ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS="{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/fsaltdaqqp/_packaging/FSA.NugetPackages%40Local/nuget/v3/index.json\", \"username\":\"docker\", \"password\":\"${FEED_ACCESSTOKEN}\"}]}"



RUN echo $VSS_NUGET_EXTERNAL_FEED_ENDPOINTS

RUN dotnet restore "./BlazorWebApp/BlazorWebApp.csproj" --configfile nuget.config

COPY . .
WORKDIR "/src/BlazorWebApp"
RUN dotnet build "./BlazorWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorWebApp.dll"]