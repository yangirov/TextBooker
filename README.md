# TextBooker

> Open Source static sites generator with content management system

## Features

- Templates
  - Choose templates from gallery
  - Create your own templates
- Editor
  - Сreate pages and blocks with rich-text editing
  - Real-time preview
- Deploy
  - FTP
  - Github Pages
  - Netlify

## Guide

### Frontend

Make sure you have a [Node.js](https://nodejs.org/) and [npm](https://www.npmjs.com/).

### Project setup

```
cd src/frontend
npm i
```

#### Compiles and hot-reloads for development

```
npm run serve
```

#### Compiles and minifies for production

```
npm run build
```

#### Lints and fixes files

```
npm run lint
```

### Backend

Make sure you have a [.NET Core 3.1](https://dotnet.microsoft.com/download) and [Docker](https://www.docker.com/).

```sh
cd src/backend
dotnet build
dotnet run
```

### Compose

Use docker-compose to deploy application:

```sh
cd src
docker-compose up --build -d
```
