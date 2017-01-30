
docker-compose up -d --build

docker logs -f haz.fewebshop.listener.test

for /f %%i in ('docker wait haz.fewebshop.listener.test') do set EXIT_CODE=%%i

docker-compose down -v

exit %EXIT_CODE%