FROM microsoft/aspnetcore:1.0.1
ENTRYPOINT ["dotnet", "HAZ.PsWinkelen.Api.dll"]
ARG source=.
WORKDIR /app
EXPOSE 80
COPY $source .
