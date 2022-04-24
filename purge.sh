#!/bin/bash

# Ingress

kubectl delete ingress taschenka-ingress
helm uninstall ingress-nginx --namespace ingress-nginx

# Taschenka

kubectl delete service taschenka-web-service
kubectl delete deployment taschenka-web-deployment

kubectl delete service taschenka-api-service
kubectl delete deployment taschenka-api-deployment

kubectl delete service taschenka-database-service
kubectl delete statefulset taschenka-database-statefulset

kubectl delete secret taschenka-secrets

# EFK

kubectl delete ingress kibana

helm delete kibana

helm delete fluentd

helm delete elasticsearch

# TODO: Elasticsearch password

minikube addons disable default-storageclass
minikube addons disable storage-provisioner

# Minikube

minikube delete
