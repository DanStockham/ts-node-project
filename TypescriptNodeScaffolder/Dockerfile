FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TypescriptNodeScaffolder/TypescriptNodeScaffolder.csproj", "TypescriptNodeScaffolder/"]
RUN dotnet restore "TypescriptNodeScaffolder/TypescriptNodeScaffolder.csproj"
COPY . .
WORKDIR "/src/TypescriptNodeScaffolder"
RUN dotnet build "TypescriptNodeScaffolder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TypescriptNodeScaffolder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TypescriptNodeScaffolder.dll"]
