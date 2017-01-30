FROM microsoft/dotnet:1.0.1-core
ENTRYPOINT ["dotnet", "HAZ.FeBestellingen.Listener.dll"]
ARG source=.
WORKDIR /app
COPY $source .
