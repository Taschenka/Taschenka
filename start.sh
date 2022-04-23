#!/bin/bash

# Minikube

minikube start --memory 4096 --cpus 4

# EFK

minikube addons enable default-storageclass
minikube addons enable storage-provisioner

helm upgrade --wait --timeout=1200s --install --values ./kubernetes/elasticsearch_values.yaml helm-es-minikube elasticsearch --repo https://helm.elastic.co

# Taschenka

kubectl create secret generic taschenka-secrets --from-literal=mongodb-password='mongoadminpassword'

kubectl apply -f ./kubernetes/taschenka_database.yaml

kubectl apply -f ./kubernetes/taschenka_api.yaml

kubectl apply -f ./kubernetes/taschenka_web.yaml

# Ingress

helm upgrade --install ingress-nginx ingress-nginx --repo https://kubernetes.github.io/ingress-nginx --namespace ingress-nginx --create-namespace

kubectl wait --namespace ingress-nginx --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=120s

kubectl annotate ingressclass nginx "ingressclass.kubernetes.io/is-default-class=true"

kubectl apply -f ./kubernetes/taschenka_ingress.yaml

# To make it work :D

kubectl port-forward --namespace=ingress-nginx service/ingress-nginx-controller 8080:80
