

# yase local



Here you can find the instruction to run the entire project in your local environment.



## Docker compose

first of all you have to build all the images (these commands are valid starting from the **src/local** folder)

```bash
$ docker build -t yase-core ../yase-core/source
$ docker build -t yase-storage ../yase-storage/source
```

 Now you can run docker compose to start up all the application's layer, starting from local folder

```bash
$ docker-compose up
```

The application stack is up and running on your local machine so you can try to perform some request in order to check the functionalities.



## Minikube

On the other way we can deploy all the application's service on a minikube cluster; this is the similar way we are going to expected to have in production.

