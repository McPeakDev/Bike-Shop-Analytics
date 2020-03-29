FROM node:10-alpine
COPY ./bikeshop-admin-frontend /var/www/html
WORKDIR /var/www/html
RUN mkdir admin/
RUN npm install -g serve
RUN npm install
RUN npm run build
EXPOSE 3000/tcp
ENTRYPOINT ["serve", "-p", "3000", "/var/www/html/build"]