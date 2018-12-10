FROM ubuntu:16.04 as builder
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_8.x | bash - && \
apt-get install -y nodejs && \
apt-get install -y build-essential
RUN node -v && npm -v

COPY ./OttooDoAngular /OttooDoAngular
RUN  npm update npm -g && npm install -g @angular/cli@7.0.2
## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
RUN npm cache clean --force || true

WORKDIR /OttooDoAngular
RUN npm install

ARG BUILD_ENV=prod
## Build the angular app in production mode and store the artifacts in dist folder
#RUN ng build --configuration=$BUILD_ENV
RUN  node --max_old_space_size=1024 ./node_modules/@angular/cli/bin/ng build --configuration=$BUILD_ENV
##RUN $(npm bin)/ng build --prod --aot --build-optimizer
### STAGE 2: Setup ###
FROM nginx:1.13.3-alpine
## Copy our default nginx config
COPY OttooDoAngular/nginx/default.conf /etc/nginx/conf.d/
## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*
## From 'builder' stage copy over the artifacts in dist folder to default nginx public folder
COPY --from=builder /OttooDoAngular/dist /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]
