@echo off
docker pull heroesandzeroes/kantilever.snapshotbuilder:latest
docker-compose -p haz.eventbus -f ./docker-composes/snapshot.yml up
pause