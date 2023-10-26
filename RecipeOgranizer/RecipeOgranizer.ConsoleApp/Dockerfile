FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RecipeOgranizer.ConsoleApp/RecipeOgranizer.ConsoleApp.csproj", "RecipeOgranizer.ConsoleApp/"]
RUN dotnet restore "RecipeOgranizer.ConsoleApp/RecipeOgranizer.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/RecipeOgranizer.ConsoleApp"
RUN dotnet build "RecipeOgranizer.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RecipeOgranizer.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeOgranizer.ConsoleApp.dll"]
