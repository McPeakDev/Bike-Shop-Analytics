FROM node:10-alpine
COPY ./bikeshop-admin-frontend /var/www/html
WORKDIR /var/www/html
RUN npm install -g serve
RUN npm install
RUN npm run build
EXPOSE 3002/tcp
ENTRYPOINT ["serve", "-p", "3002", "-s", "/var/www/html/build"]