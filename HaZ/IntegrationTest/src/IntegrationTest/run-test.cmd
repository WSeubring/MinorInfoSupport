
cd ..\..\..\
dotnet restore --no-cache

cd FeWebshop\src\HAZ.FeWebshop.Api
dotnet publish
cd ..\HAZ.FeWebshop.Listener
dotnet publish

cd ..\..\..\FeWebshop\src\FeBestellingen\src\HAZ.FeBestellingen.Api
dotnet publish
cd ..\HAZ.FeBestellingen.Listener
dotnet publish

cd ..\..\..\PsWinkelen\src\HAZ.PsWinkelen.Api
dotnet publish
cd ..\HAZ.PsWinkelen.Listener
dotnet publish

cd ..\..\..\DSBestellingenBeheer\src\HaZ.DSBestellingenBeheer.Facade
dotnet publish

cd ..\..\..\IntegrationTest\src\IntegrationTest

docker-compose up -d --build

docker logs -f haz.integration.test

for /f %%i in ('docker wait haz.integration.test') do set EXIT_CODE=%%i

REM docker-compose down -v

exit %EXIT_CODE%