docker build --no-cache -t ronanavila/creation-controller . -f Dockerfile_creation_controller --label creation-controller
docker build --no-cache -t ronanavila/creation-consumer . -f Dockerfile_creation_consumer --label creation-consumer

docker build --no-cache -t ronanavila/update-controller . -f Dockerfile_update_controller --label update-controller
docker build --no-cache -t ronanavila/update-consumer . -f Dockerfile_update_consumer --label update-consumer

docker build --no-cache -t ronanavila/delete-controller . -f Dockerfile_delete_controller --label delete-controller
docker build --no-cache -t ronanavila/delete-consumer . -f Dockerfile_delete_consumer --label delete-consumer

docker build --no-cache -t ronanavila/search-controller . -f Dockerfile_search_controller --label search-controller


docker push ronanavila/creation-controller
docker push ronanavila/creation-consumer

docker push ronanavila/update-controller
docker push ronanavila/update-consumer

docker push ronanavila/delete-controller
docker push ronanavila/delete-consumer

docker push ronanavila/search-controller


kubectl create deployment creation-controller --image=ronanavila/creation-controller

kubectl apply -f config-map-creation-controller.yml