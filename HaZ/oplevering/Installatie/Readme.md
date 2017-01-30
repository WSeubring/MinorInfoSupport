# Deployment

## Requirements:
* Docker heeft minimaal 4gb ram nodig
* Docker moet toegang hebben tot de C schijf

## Installation
1. Run ``deploy.bat``
2. The website is available at ``http://[machinenaam]/Kantilever``
3. The inernal website is available at ``http://[machinenaam]/Intranet``
4. Log files zijn te vinden in de ``logs`` map
5. Data is gepersisteerd in de ``data`` map

## Troubleshooting
Na het runnen van ``deploy.bat`` kunnen sommige services nog niet up zijn doordat de databases te langzaam opstart (dit is afhankelijk van het systeem)
Check 2 minuten nadat het script klaar is of services down zijn door middel van ``docker ps -a`` en dan zoeken naar containers met de status ``Exited`` (de container ``hazfewebshopapi_haz.fewebshop.wwwroot_1`` hoort de status ``Exited`` te hebben)
Als er containers zijn met die down zijn kan je ze herstarten met ``docker restart <container_name|container_id>``

## Replay events
After the Installation is complete you can replay the snapshot by running ``run-snapshot.bat``