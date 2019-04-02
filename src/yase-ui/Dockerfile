FROM node:8.10.0


# set working directory
RUN mkdir /usr/src/app
RUN mkdir /usr/src/app/public
RUN mkdir /usr/src/app/src

WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY package.json /usr/src/app/package.json
COPY ./public /usr/src/app/public
COPY ./src /usr/src/app/src

RUN npm install
RUN npm install react-scripts@1.1.1 -g --silent

# start app
CMD ["npm", "start"]
