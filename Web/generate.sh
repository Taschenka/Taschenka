java -jar /opt/swagger-codegen/modules/swagger-codegen-cli/target/swagger-codegen-cli.jar generate \
--input-spec http://localhost:5210/swagger/v1/swagger.json \
--lang typescript-angular \
--output test \
--additional-properties npmName=taschenka-api,ngVersion=13.2.0
