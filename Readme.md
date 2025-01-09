host.docker.internal
docker build --no-cache -t contact-creation-controller . -f Dockerfile_creation_controller --label contact-creation-controller
docker build --no-cache -t contact-creation-consumer . -f Dockerfile_creation_consumer --label contact-creation-consumer

docker build --no-cache -t contact-update-controller . -f Dockerfile_update_controller --label contact-update-controller
docker build --no-cache -t contact-update-consumer . -f Dockerfile_update_consumer --label contact-update-consumer

docker build --no-cache -t contact-delete-controller . -f Dockerfile_delete_controller --label contact-delete-controller
docker build --no-cache -t contact-delete-consumer . -f Dockerfile_delete_consumer --label contact-delete-consumer

docker build --no-cache -t contact-search-controller . -f Dockerfile_search_controller --label contact-search-controller
