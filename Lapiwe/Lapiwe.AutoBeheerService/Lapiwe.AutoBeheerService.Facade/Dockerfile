FROM microsoft/dotnet:1.0.1-core
ENTRYPOINT ["dotnet", "Lapiwe.AutoBeheerService.Facade.dll"]
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ARG source=.
WORKDIR /app
COPY $source .
