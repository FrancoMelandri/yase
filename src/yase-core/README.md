# yase core



Core service to provde tha main functionalities of the shortener engine.

The aim of this module is to encapsulate all the logic to generate, applying time to live, store and get the short URL.





The command to build the docker image

```bash
$ docker build -r yase-core .
```

Tagging the image and push it to the docker hub (you need a docker login in order to tpush to the remote docker hub)

```bash
$ docker tag yase-core francomelandri/yase-core
$ dokcer push francomelandri/yase-core
```

Creating a deployment resource inside k8s cluster (minikube for us)

```bash
$ kubectl create -f ./k8s/deployment.yaml
```

Createing and expose a service for the tase-core

````bash
$ kubectl create -f ./k8s/service.yaml
````



