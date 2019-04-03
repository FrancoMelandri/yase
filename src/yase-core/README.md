# yase core



Core service to provde tha main functionalities of the shortener engine.

The aim of this module is to encapsulate all the logic to generate, applying time to live, store and get the short URL.



## Setup

The command to build the docker image

```bash
$ docker build -r yase-core .
```

Tagging the image and push it to the docker hub (you need a docker login in order to push to the remote docker hub)

```bash
$ docker tag yase-core francomelandri/yase-core
$ dokcer push francomelandri/yase-core
```

Running locally using docker

```bash
$ docker run -it -p 9000:9000 yase-core
```

Creating a deployment resource inside k8s cluster (minikube for us)

```bash
$ kubectl create -f ./k8s/deployment.yaml
```

Creating and exposing a service for the yase-core

````bash
$ kubectl create -f ./k8s/service.yaml
````



## Resources

### URL creation

using the API **/engine** you are able to retrieve the tiny format of the original URL

```bash
$ curl -X PUT \
  	http://localhost:9000/engine \
  	-H 'cache-control: no-cache' \
  	-H 'content-type: application/json' \
  	-d '{
		"url": "https://www.example.com/param1=1&param2=2&param3=3&param4=4"
	}'

---

{"tinyUrl":"http://base.com/5e7def8","originalUrl":"https://www.example.com/param1=1&param2=2&param3=3&param4=4","hashedUrl":"5e7def8","hitted":0}
```

As you can see the response contains informations about the **tinyUrl**l generated and how many times the short URL was hitted by any user.



### URL match

Calling the same resource using the **POST** HTTP verb we are able to retrieve the original URL associated to a tiny URL previously created.

```bash
$ curl -X POST \
      http://localhost:9000/engine \
      -H 'cache-control: no-cache' \
      -H 'content-type: application/json' \
      -d '{
        "url": "http://base.com/69208a74"
    }'
 
 ---
 
 {"tinyUrl":"http://base.com/69208a7","originalUrl":"https://www.example.com/param1=1&param2=2&param3=3&param4=4"}
```



### URL delete

Calling the same resource using the **DELETE** HTTP verb we are able to remove the original URL associated to a tiny URL previously created.

```bash
$ curl -X DELETE \
      http://localhost:9000/engine \
      -H 'cache-control: no-cache' \
      -H 'content-type: application/json' \
      -d '{
        "url": "http://base.com/69208a74"
    }'
 
 ---
 
200 OK
```

