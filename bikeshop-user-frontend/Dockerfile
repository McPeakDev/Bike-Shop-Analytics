FROM node:10-alpine
COPY ./bikeshop-user-frontend /var/www/html
WORKDIR /var/www/html
RUN npm install -g serve
RUN npm install
RUN npm run build
EXPOSE 3001/tcp
ENTRYPOINT ["serve", "-p", "3001", "-s", "/var/www/html/build"]