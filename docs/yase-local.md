

# yase-local



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

Using the **kompose** utility(<http://kompose.io/> we can generate all the resources file we need to setup the kubernetes cluster. In order to run theweb application under minikube we have to change the yase-ui-service using NodePort as type, that is the only one supported (<https://kubernetes.io/docs/concepts/services-networking/service/>)

```bash
$ kompose convert
```

Once the minikube cluster is up after the command

```bash
$ minikube start
```

We can create all the resources inside the cluster using the **kubectl** utility

```bash
$ minikube apply -f filename.yaml
```

At the end of the command list we can check the minikube cluster status

```bash
$ minikube get all
NAME                                READY   STATUS    RESTARTS   AGE
pod/mongodb-66f65d79cd-cqtjh        1/1     Running   0          5m45s
pod/yase-core-58f8dc4889-xdkkj      1/1     Running   0          67s
pod/yase-storage-55db8776bd-jtxz6   1/1     Running   0          3m23s
pod/yase-ui-69cfc6f749-hrt8d        1/1     Running   0          97m

NAME                   TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)          AGE
service/kubernetes     ClusterIP   10.96.0.1       <none>        443/TCP          101m
service/mongodb        ClusterIP   10.109.37.22    <none>        27017/TCP        5m38s
service/yase-core      NodePort    10.99.169.154   <none>        9000:30001/TCP   14s
service/yase-storage   NodePort    10.108.7.131    <none>        9001:30669/TCP   3m17s
service/yase-ui        NodePort    10.96.253.232   <none>        8080:30000/TCP   94m

NAME                           READY   UP-TO-DATE   AVAILABLE   AGE
deployment.apps/mongodb        1/1     1            1           5m45s
deployment.apps/yase-core      1/1     1            1           67s
deployment.apps/yase-storage   1/1     1            1           3m23s
deployment.apps/yase-ui        1/1     1            1           97m

NAME                                      DESIRED   CURRENT   READY   AGE
replicaset.apps/mongodb-66f65d79cd        1         1         1       5m45s
replicaset.apps/yase-core-58f8dc4889      1         1         1       67s
replicaset.apps/yase-storage-55db8776bd   1         1         1       3m23s
replicaset.apps/yase-ui-69cfc6f749        1         1         1       97m

```

Once the resources are created we can ask minikube for the external address of the web application invoking the command

```bash
$ minikube service yase-ui --url
$ minikube service yase-core --url
```



Command to update the rollout a new image after an upgrade

```bash
$ kubectl rollout status deployment.apps/yase-ui
```

If you wanna check the status of the entire stack running you can use the command 

```bash
$ minikube dashboard
```

If you wanna enter in intercative mode with bash in a pod you can type the command

```bash
$  kubectl exec -it yase-ui-69cfc6f749-hrt8d /bin/bash
```

 