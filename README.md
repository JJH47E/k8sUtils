# K8sUtils

A GUI tool for overseeing kubernetes deployments. *This is not a tool for managing clusters.*

This application is not intended to replace `kubectl`, its aim is purely to make simple `kubectl` tasks simpler.

K8sUtils was developed to support Windows, Linux & MacOS. It has been tested on MacOS.

## Setup

### Prerequisites

- `kubectl` must be installed and on your system PATH

Furthermore, `kubectl` should be preconfigured with the correct context.

**If you do not have access to a kubernetes cluster, you can use `minikube` to run one locally**

Once installed, you can run the following commands to create a deployment to the `test` namespace:

```bash
minikube start
kubectl create namespace test
kubectl create deployment hello-minikube --image=kicbase/echo-server:1.0 -n test
kubectl expose deployment hello-minikube --type=NodePort --port=8080 -n test
kubectl logs service/hello-minikube -n test
```
