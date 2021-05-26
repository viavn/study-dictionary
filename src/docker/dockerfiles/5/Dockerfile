# Pulls the official image
FROM node:13.12.0-alpine as builder

# Sets the working dir
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# Install all the dependencies
COPY package.json ./
RUN npm install babel-polyfill # Necessary dependency for production build
RUN npm install --silent --only=prod # Only production dependencies

# Add the App Code
COPY . ./

# Script that starts the app
RUN npm run build

# ------------------------------------------------------
# Production Build
# ------------------------------------------------------
FROM nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /app/build /usr/share/nginx/html
