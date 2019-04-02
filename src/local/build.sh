docker build -t yase-core ../yase-core/source
docker tag yase-core francomelandri/yase-core

docker build -t yase-storage ../yase-storage/source
docker tag yase-storage francomelandri/yase-storage

docker build -t yase-ui ../yase-ui
docker tag yase-ui francomelandri/yase-ui
