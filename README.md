# RCL_UI

https://stackoverflow.com/questions/38051528/passing-arguments-for-dockerfiles-using-docker-compose
https://stackoverflow.com/questions/58695423/pass-args-to-the-dockerfile-from-docker-compose

primera opcion
docker-compose -f "docker-compose.yml" up -d --build

segunda opcion

docker-compose -f "docker-compose.yml" build --build-arg FEED_ACCESSTOKEN="6xqkiqZd4aMMhxwqRliozxY5Z7qtDQ3edkLKBvwj1XqPy16K6IMhJQQJ99BCACAAAAAgJncgAAASAZDO2wAw"