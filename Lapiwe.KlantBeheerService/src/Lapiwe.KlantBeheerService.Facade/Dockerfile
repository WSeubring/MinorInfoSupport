FROM microsoft/dotnet:1.0.1-core
ENTRYPOINT ["dotnet", "Lapiwe.KlantBeheerService.Facade.dll"]
ARG source=.
WORKDIR /app
COPY $source .
