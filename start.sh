#!/bin/bash

# Minikube

minikube start --memory 5120 --cpus 4

# EFK

minikube addons enable default-storageclass
minikube addons enable storage-provisioner

# TODO: Password

helm upgrade --install elasticsearch elasticsearch --repo https://helm.elastic.co --values ./kubernetes/elasticsearch_values.yaml --wait --timeout=1200s

helm upgrade --install fluentd fluentd --repo https://fluent.github.io/helm-charts --values ./kubernetes/fluentd_values.yaml --wait

helm upgrade --install kibana kibana --repo https://helm.elastic.co --values ./kubernetes/kibana_values.yaml --wait

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
