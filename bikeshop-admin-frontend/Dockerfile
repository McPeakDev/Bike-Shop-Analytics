FROM node:10-alpine
COPY ./bikeshop-frontend /var/www/html
WORKDIR /var/www/html
RUN npm install -g serve
RUN npm install
RUN npm run build
EXPOSE 3000/tcp
ENTRYPOINT ["serve", "-p", "3000", "-s", "/var/www/html/build"]