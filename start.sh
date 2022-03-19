docker network create todos-network
cd ./Database
./start.sh
cd ..
cd ./Api
./build.sh
./start.sh
cd ..
