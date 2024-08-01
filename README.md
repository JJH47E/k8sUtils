How to set up for local development:

```bash
minikube start
kubectl create namespace test
kubectl create deployment hello-minikube --image=kicbase/echo-server:1.0 -n test
kubectl expose deployment hello-minikube --type=NodePort --port=8080 -n test
kubectl logs service/hello-minikube -n test
```
