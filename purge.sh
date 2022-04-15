#!/bin/bash

kubectl delete secret taschenka-secrets

kubectl delete statefulset taschenka-database-statefulset
kubectl delete service taschenka-database-service

kubectl delete deployment taschenka-api-deployment
kubectl delete service taschenka-api-service

kubectl delete deployment taschenka-web-deployment
kubectl delete service taschenka-web-service
