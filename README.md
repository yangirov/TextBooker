## Фронтенд

Для сборки фронтенда нужен [npm](https://www.npmjs.com/) (на компьютере должен быть [node.js](https://nodejs.org/)).

Дев-режим:
```bash
cd sources/frontend
npm i
npm run serve
```

Продакшн-сборка:

```bash
npm run build
```

## Бекенд

```bash
cd sources
docker-compose up -d --build
```

```
docker volume rm textbooker_db-data
docker volume rm textbooker_db-logs
docker volume rm textbooker_grafana-data
docker volume rm textbooker_loki-data
docker volume rm textbooker_nginx-logs
docker volume rm textbooker_prometheus-data
```