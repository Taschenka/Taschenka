#!/bin/bash

kubectl create secret generic taschenka-secrets --from-literal=mongodb-password='mongoadminpassword'

kubectl apply -f ./kubernetes/taschenka_database.yaml

kubectl apply -f ./kubernetes/taschenka_api.yaml

kubectl apply -f ./kubernetes/taschenka_web.yaml

echo Run 'kubectl get services', to get the EXTERNAL-IP

minikube tunnel
