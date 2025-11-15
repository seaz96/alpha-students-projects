FROM oven/bun:latest AS build

ARG API_URL
ENV VITE_API_URL=$API_URL
ENV VITE_APP_NAME=$APP_NAME
WORKDIR /app
COPY package*.json ./
RUN bun install
COPY . .
RUN bun run build

FROM nginx:stable
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]