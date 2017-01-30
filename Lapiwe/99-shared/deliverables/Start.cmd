@echo on

docker network create lapiwe-eventbus-network

CD C:\TFS\Lapiwe\RabbitMq.Dockercompose
start "" docker-compose up -d

CD C:\TFS\Lapiwe\Lapiwe.IS.RDW\src\Lapiwe.IS.RDW
start "" docker-compose up -d

CD C:\TFS\Lapiwe\Lapiwe.OnderhoudService\src\Lapiwe.OnderhoudService.Facade
start "" docker-compose up -d

CD C:\TFS\Lapiwe\Lapiwe.GMS.FrontEnd\src\Lapiwe.GMS.FrontEnd
start "" docker-compose up -d

CD C:\TFS\Lapiwe\Lapiwe.Audit\src\Lapiwe.Audit
start "" docker-compose up -d

ECHO "Services are starting up. You can close this window."
pause