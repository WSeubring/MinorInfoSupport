@echo off
SET TAG=latest
echo Clean docker images


echo Create Network
docker network create eventbus
echo Deploy EventBus
docker pull heroesandzeroes/kantilever.auditlogservice:latest
docker-compose -p haz.eventbus -f ./docker-composes/event-bus.yml up -d
timeout 60

echo Deploy HAZ.FeWebshop.Api
docker pull heroesandzeroes/haz.fewebshop.api:latest
docker pull heroesandzeroes/haz.fewebshop.wwwroot:latest
docker-compose -p haz.fewebshop.api -f ./docker-composes/haz.fewebshop.api.yml up -d
timeout 60

echo Deploy HAZ.DsBestellingenBeheer.Api
docker pull heroesandzeroes/haz.dsbestellingenbeheer.facade:latest
docker-compose -p haz.dsbestellingenbeheer.api -f ./docker-composes/haz.dsbestellingenbeheer.api.yml up -d
timeout 60

echo Deploy HAZ.PsWinkelen.Api
docker pull heroesandzeroes/haz.pswinkelen.api:latest
docker-compose -p haz.pswinkelen.api -f ./docker-composes/haz.pswinkelen.api.yml up -d
timeout 60

echo Deploy HAZ.FeBestellingen.Api
docker pull heroesandzeroes/haz.febestellingen.api:latest
docker-compose -p haz.febestellingen.api -f ./docker-composes/haz.febestellingen.api.yml up -d
timeout 60

echo Deploy Magazijnbeheer app
docker pull heroesandzeroes/kantilever.magazijnbeheer:latest
docker-compose -p haz.magazijnbeheer.api -f ./docker-composes/magazijnbeheer.yml up -d
timeout 60

echo Deploy HAZ.FeWebshop.Listener
docker pull heroesandzeroes/haz.fewebshop.listener:latest
docker-compose -p haz.fewebshop.listener -f ./docker-composes/haz.fewebshop.listener.yml up -d

echo Deploy HAZ.FeBestellingen.Listener
docker pull heroesandzeroes/haz.febestellingen.listener:latest
docker-compose -p haz.febestellingen.listener -f ./docker-composes/haz.febestellingen.listener.yml up -d

echo Deploy HAZ.PsWinkelen.Listener
docker pull heroesandzeroes/haz.pswinkelen.listener:latest
docker-compose -p haz.pswinkelenlistenerapi -f ./docker-composes/haz.pswinkelen.listener.yml up -d

echo Deploy Reverse proxy
docker-compose -p haz.proxy -f ./docker-composes/haproxy.yml up -d
timeout 10

echo ============ Status ============
docker ps -a

echo =====================================
echo   Running at http://localhost/Kantilever/
echo   Running at http://localhost/Intranet/
echo =====================================
pause