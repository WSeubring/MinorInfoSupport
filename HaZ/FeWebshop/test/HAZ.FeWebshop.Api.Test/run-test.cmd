
docker-compose up -d --build

docker logs -f haz.fewebshop.api.test

for /f %%i in ('docker wait haz.fewebshop.api.test') do set EXIT_CODE=%%i


docker-compose down -v

exit %EXIT_CODE%