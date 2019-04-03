

# yase storage



The aim of this service is to provide the persistence layer to the core engine.

As I said I have decided to decouple core logic from persistence logic in order to allow the application stack to be deployed where we want avoiding vendor lock-in.

For example we ca use the mongo db configuration in case of on-premise installation.

In case of AWS installation we should move into **DynamoDB**. We can do this replacing the storage service with a new one and without changing the core logic.

 

## Setup

The command to build the docker image

```bash
$ docker build -r yase-storage .
```

Tagging the image and push it to the docker hub (you need a docker login in order to push to the remote docker hub)

```bash
$ docker tag yase-storage francomelandri/yase-storage
$ dokcer push francomelandri/yase-storage
```

Running locally using docker

```bash
$ docker run -it -p 9001:9001 yase-storage
```

Creating a deployment resource inside k8s cluster (minikube for us)

```bash
$ kubectl create -f ./k8s/deployment.yaml
```

Creating and exposing a service for the yase-core

```bash
$ kubectl create -f ./k8s/service.yaml
```



## Resources

### URL retrieve

using the API **/storage/{tinyUrl}** using **GET** verb, we are able to retrieve the tiny URL stored

```bash
$ curl -X GET \
  	http://localhost:9001/storage/075cd98 \
  	-H 'cache-control: no-cache' \
  	-H 'content-type: application/json'

---

{"originalUrl": "http://www.example.con/yase=1", "tinyUrl": "075cd98" }
```



### URL store

Calling the resources **/starge** using the **POST** HTTP verb we are able to store the information in the DB.

```bash
$ curl -X POST \
      http://localhost:9001/storage \
      -H 'cache-control: no-cache' \
      -H 'content-type: application/json' \
      -d '{
        "originalUrl": "https://www.example.com/param1=1&param2=2&param3=3&param4=4"
        "tinyUrl": "69208a74"
    }'
 
 ---
 
 {"tinyUrl":"http://base.com/69208a7","originalUrl":"https://www.example.com/param1=1&param2=2&param3=3&param4=4","hashedUrl":"69208a7","hitted":0}
```



### URL delete

Calling the resource **/storage/{tinyUrl}** using the **DELETE** HTTP verb we are able to remove the original URL associated to a tiny URL previously created.

```bash
$ curl -X DELETE \
      http://localhost:9001/storage/075cd98 \
      -H 'cache-control: no-cache' \
      -H 'content-type: application/json' 
 
 ---
 
 200 OK
```

