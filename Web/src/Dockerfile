#stage 1
FROM node:16.14 as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

# stage 2
FROM nginx:1.21
COPY --from=node /app/dist/taschenka /usr/share/nginx/html
