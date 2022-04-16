#!/bin/bash

kubectl create secret generic taschenka-secrets --from-literal=mongodb-password='mongoadminpassword'

kubectl apply -f ./kubernetes/taschenka_database.yaml

kubectl apply -f ./kubernetes/taschenka_api.yaml

kubectl apply -f ./kubernetes/taschenka_web.yaml

helm upgrade --install ingress-nginx ingress-nginx --repo https://kubernetes.github.io/ingress-nginx --namespace ingress-nginx --create-namespace

kubectl wait --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=120s

kubectl apply -f ./kubernetes/taschenka_ingress.yaml

kubectl port-forward --namespace=ingress-nginx service/ingress-nginx-controller 8080:80
