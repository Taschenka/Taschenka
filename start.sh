#!/bin/bash

kubectl create secret generic taschenka-secrets --from-literal=mongodb-password='mongoadminpassword'

kubectl apply -f ./kubernetes/taschenka_api.yaml

kubectl apply -f ./kubernetes/taschenka_database.yaml
