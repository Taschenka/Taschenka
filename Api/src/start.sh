sudo docker run -d --rm --name taschenka_api -p 5210:80 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password=mongoadminpassword --network=todos-network taschenka_api:latest
