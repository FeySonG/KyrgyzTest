# CI/CD на ветке `CICDNew`

## Что происходит

- Pull request в `CICDNew`: GitHub Actions собирает Docker-образы API и фронтенда, но не публикует их.
- Push в `CICDNew` или ручной запуск workflow: образы собираются и публикуются в GitHub Container Registry (GHCR) как `latest` и как образ с SHA коммита.
- Сервер отдельно скачивает образы и перезапускает контейнеры через Docker Compose. GitHub Actions пока не подключается к серверу и не делает автоматический деплой.

Образы: `ghcr.io/feysong/kyrgyztest-api` и `ghcr.io/feysong/kyrgyztest-frontend`.

## Один раз настроить GitHub

1. Запушить ветку `CICDNew` в GitHub и открыть вкладку **Actions** репозитория. Первый запуск workflow появится после push.
2. В **Settings → Actions → General → Workflow permissions** выбрать разрешение на чтение и запись (workflow также задаёт `packages: write`).
3. После успешной публикации открыть **GitHub → профиль → Packages**, выбрать два созданных пакета. Если сервер не может скачать их анонимно, выставить package visibility в **Private** и использовать PAT с `read:packages` на сервере.

## Настроить сервер

Нужен сервер с Docker Engine и Docker Compose plugin, доступом к SQL Server и Meilisearch. SQL Server не входит в Compose: API подключается к существующим базам через строки подключения. `host.docker.internal` уже настроен для подключения к сервисам на Docker-хосте; если SQL или Meilisearch находятся на другой машине, укажите её адрес.

Скопируйте в папку деплоя `compose.prod.yml` из репозитория. Создайте рядом `.env` и заполните:

```dotenv
HTTP_PORT=8080
DEFAULT_CONNECTION_STRING=Server=host.docker.internal,1433;Database=KyrgyzTestDB;User Id=...;Password=...;TrustServerCertificate=True;MultipleActiveResultSets=true
OLD_DB_CONNECTION_STRING=Server=host.docker.internal,1433;Database=AVN_test;User Id=...;Password=...;TrustServerCertificate=True
OLD_DB_REGION_CONNECTION_STRING=Server=host.docker.internal,1433;Database=AVN_test_region;User Id=...;Password=...;TrustServerCertificate=True
MEILISEARCH_URL=http://host.docker.internal:7700
MEILISEARCH_API_KEY=
```

Используйте отдельные учётные данные SQL с минимально нужными правами. Не коммитьте `.env` и не публикуйте его содержимое. Для HTTPS поставьте перед портом приложения reverse proxy (например, IIS, Caddy или nginx) с сертификатом.

Если GHCR-пакеты private, на сервере войдите в registry с GitHub PAT, имеющим только `read:packages`:

```sh
docker login ghcr.io -u YOUR_GITHUB_USERNAME
```

Вставьте PAT в запрос пароля. Затем запустите:

```sh
docker compose -f compose.prod.yml pull
docker compose -f compose.prod.yml up -d
docker compose -f compose.prod.yml logs -f
```

Откройте `http://SERVER_ADDRESS:8080`. В production вызов API идёт через nginx контейнера фронтенда к API контейнеру, поэтому браузеру не нужен отдельный CORS origin.

## Обычное использование

1. Разрабатывайте в `CICDNew` или создайте PR в неё. PR проверит сборку обоих образов.
2. После push/merge в `CICDNew` дождитесь зелёного запуска **Actions**: он обновит `latest` и добавит неизменяемые теги SHA.
3. Для ручного обновления сервера выполните в папке Compose: `docker compose -f compose.prod.yml pull`, затем `docker compose -f compose.prod.yml up -d`.
4. Чтобы откатиться, замените `:latest` в Compose на нужный SHA-тег образа для обоих сервисов, затем повторите `pull` и `up -d`.

Workflow пока не выполняет удалённый деплой. Сначала проверьте сборку и запуск контейнеров на тестовом сервере; для полноценного автодеплоя потребуется согласовать и добавить доступ Actions к вашему серверу. Автоматическое применение SQL миграций не настроено: API сам вызывает существующий `IDatabaseInitializer` при старте.
