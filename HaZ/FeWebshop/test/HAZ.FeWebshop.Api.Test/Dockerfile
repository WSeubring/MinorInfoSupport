FROM microsoft/dotnet:1.0.1-sdk-projectjson
ENTRYPOINT dotnet restore ../../ --no-cache && dotnet test
ARG source=.
WORKDIR /app/test/HAZ.FeWebshop.Api.Test
EXPOSE 80
COPY $source /app
