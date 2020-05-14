## Фронтенд

Для сборки фронтенда нужен [npm](https://www.npmjs.com/) (на компьютере должен быть [node.js](https://nodejs.org/)).

Дев-режим:
```bash
cd frontend
npm i
npm run serve
```

Продакшн-сборка:

```bash
npm run build
```

## Бекенд

```bash
docker-compose up -d --build
```