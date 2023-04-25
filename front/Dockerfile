FROM node:15.11.0-alpine3.13 as builder

WORKDIR /app

RUN yarn global add vite@2.0.5
COPY ./package*.json ./
COPY ./yarn.lock ./
RUN yarn install --frozen-lockfile

COPY . .
RUN vite build

FROM nginx:1.19.8-alpine

COPY ./nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
