

# yase local



Here you can find the instruction to run the entire project in your local environment.

You must have installed ***Docker*** in order to start the entire stack using docker compose. If you wanna use the K8s style you have to install **minikube** and **Kubectl**.



## Docker compose

first of all you have to build all the images (these commands are valid starting from the **src/local** folder)

```bash
$ docker build -t yase-core ../yase-core/source
$ docker build -t yase-storage ../yase-storage/source
$ docker build -t yase-ui ../yase-ui
```

 Now you can run docker compose to start up all the application's layer, starting from local folder

```bash
$ docker-compose up
```

The application stack is up and running on your local machine so you can try to perform some request in order to check the functionalities.

The entry point is the web application running at http://localhost:8080

You can also call directly the core service (**yase-core**) at the address http://localhost:9000 and the storage service (**yase-storage**) at the address http://localhost:9001



## Minikube

On the other way we can deploy all the application's service on a minikube cluster; this is the similar way we are going to expected to have in production.

Using the **kompose** utility(<http://kompose.io/> we can generate all the resources file we need to setup the kubernetes cluster.

```bash
$ kompose convert
```



Once the minikube cluster is up 