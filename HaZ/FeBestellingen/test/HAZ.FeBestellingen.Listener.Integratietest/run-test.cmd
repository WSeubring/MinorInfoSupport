
docker-compose up -d --build

docker logs -f haz.febestellingen.listener.test

for /f %%i in ('docker wait haz.febestellingen.listener.test') do set EXIT_CODE=%%i

docker-compose down -v

exit %EXIT_CODE%