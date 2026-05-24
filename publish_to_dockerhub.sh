#!/usr/bin/env bash
# =============================================================
# publish_to_dockerhub.sh
# Скрипт сборки и публикации образа MedMonitor на Docker Hub
#
# Использование:
#   chmod +x publish_to_dockerhub.sh
#   ./publish_to_dockerhub.sh <dockerhub_username> [version_tag]
#
# Примеры:
#   ./publish_to_dockerhub.sh johndoe
#   ./publish_to_dockerhub.sh johndoe 1.0.0
# =============================================================

set -e  # Остановить при любой ошибке

# ---- Параметры ----
DOCKER_USER="${1:-youruser}"
VERSION="${2:-1.0.0}"
IMAGE_NAME="medmonitor"
FULL_TAG="${DOCKER_USER}/${IMAGE_NAME}"

echo "=============================================="
echo " MedMonitor — Публикация на Docker Hub"
echo " Пользователь : ${DOCKER_USER}"
echo " Образ        : ${FULL_TAG}"
echo " Версия       : ${VERSION}"
echo "=============================================="

# 1. Авторизация в Docker Hub
echo ""
echo "[1/5] Авторизация в Docker Hub..."
docker login
echo "✅ Авторизация успешна"

# 2. Сборка образа
echo ""
echo "[2/5] Сборка Docker-образа..."
docker build \
  --no-cache \
  --label "version=${VERSION}" \
  --label "build-date=$(date -u +%Y-%m-%dT%H:%M:%SZ)" \
  --label "maintainer=${DOCKER_USER}" \
  -t "${FULL_TAG}:latest" \
  -t "${FULL_TAG}:${VERSION}" \
  .
echo "✅ Образ собран: ${FULL_TAG}:${VERSION}"

# 3. Проверка образа
echo ""
echo "[3/5] Проверка образа..."
docker images "${FULL_TAG}" --format "table {{.Repository}}\t{{.Tag}}\t{{.Size}}\t{{.CreatedAt}}"

# 4. Публикация на Docker Hub
echo ""
echo "[4/5] Загрузка образа на Docker Hub..."
docker push "${FULL_TAG}:latest"
docker push "${FULL_TAG}:${VERSION}"
echo "✅ Образ опубликован"

# 5. Итог
echo ""
echo "[5/5] Готово!"
echo "=============================================="
echo "🐳 Образ доступен по адресам:"
echo "   docker pull ${FULL_TAG}:latest"
echo "   docker pull ${FULL_TAG}:${VERSION}"
echo ""
echo "🌐 Docker Hub:"
echo "   https://hub.docker.com/r/${DOCKER_USER}/${IMAGE_NAME}"
echo ""
echo "▶  Быстрый запуск:"
echo "   docker run -d -p 8080:8080 \\"
echo "     -v medmonitor_data:/app/data \\"
echo "     --name medmonitor \\"
echo "     ${FULL_TAG}:latest"
echo ""
echo "   Приложение: http://localhost:8080"
echo "=============================================="
