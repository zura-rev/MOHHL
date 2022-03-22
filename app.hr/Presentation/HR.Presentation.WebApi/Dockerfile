#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Presentation/HR.Presentation.WebApi/HR.Presentation.WebApi.csproj", "Presentation/HR.Presentation.WebApi/"]
COPY ["Infrastructure/HR.Infrastructure.Persistence/HR.Infrastructure.Persistence.csproj", "Infrastructure/HR.Infrastructure.Persistence/"]
COPY ["Core/HR.Core.Application/HR.Core.Application.csproj", "Core/HR.Core.Application/"]
COPY ["Core/HR.Core.Domain/HR.Core.Domain.csproj", "Core/HR.Core.Domain/"]
RUN dotnet restore "Presentation/HR.Presentation.WebApi/HR.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/Presentation/HR.Presentation.WebApi"
RUN dotnet build "HR.Presentation.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR.Presentation.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.Presentation.WebApi.dll"]
