FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
COPY . .
RUN dotnet build --configuration Release
RUN dotnet test PhotoAlbum.PlaceholderPhotos.Tests
RUN dotnet publish PhotoAlbum.Console/PhotoAlbum.Console.csproj -c Release -o out --no-build --no-restore

FROM mcr.microsoft.com/dotnet/runtime:6.0 as runtime
WORKDIR /app 
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "PhotoAlbum.Console.dll"]
