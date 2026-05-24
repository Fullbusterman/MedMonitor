# =============================================================
# publish_to_dockerhub.ps1
# Скрипт сборки и публикации образа MedMonitor на Docker Hub
# для Windows (PowerShell)
#
# Использование:
#   .\publish_to_dockerhub.ps1 -DockerUser johndoe -Version 1.0.0
# =============================================================

param(
    [Parameter(Mandatory=$true)]
    [string]$DockerUser,

    [string]$Version = "1.0.0",
    [string]$ImageName = "medmonitor"
)

$ErrorActionPreference = "Stop"
$FullTag = "${DockerUser}/${ImageName}"

Write-Host "==============================================" -ForegroundColor Cyan
Write-Host " MedMonitor — Публикация на Docker Hub"        -ForegroundColor Cyan
Write-Host " Пользователь : $DockerUser"                   -ForegroundColor White
Write-Host " Образ        : $FullTag"                      -ForegroundColor White
Write-Host " Версия       : $Version"                      -ForegroundColor White
Write-Host "==============================================" -ForegroundColor Cyan

# 1. Авторизация
Write-Host "`n[1/5] Авторизация в Docker Hub..." -ForegroundColor Yellow
docker login
Write-Host "✅ Авторизация успешна" -ForegroundColor Green

# 2. Сборка
Write-Host "`n[2/5] Сборка Docker-образа..." -ForegroundColor Yellow
$BuildDate = (Get-Date -Format "yyyy-MM-ddTHH:mm:ssZ")
docker build `
    --no-cache `
    --label "version=$Version" `
    --label "build-date=$BuildDate" `
    --label "maintainer=$DockerUser" `
    -t "${FullTag}:latest" `
    -t "${FullTag}:${Version}" `
    .
Write-Host "✅ Образ собран: ${FullTag}:${Version}" -ForegroundColor Green

# 3. Проверка
Write-Host "`n[3/5] Информация об образе..." -ForegroundColor Yellow
docker images $FullTag

# 4. Публикация
Write-Host "`n[4/5] Загрузка на Docker Hub..." -ForegroundColor Yellow
docker push "${FullTag}:latest"
docker push "${FullTag}:${Version}"
Write-Host "✅ Образ опубликован" -ForegroundColor Green

# 5. Итог
Write-Host "`n[5/5] Готово!" -ForegroundColor Green
Write-Host "==============================================" -ForegroundColor Cyan
Write-Host "🐳 Образ доступен:" -ForegroundColor White
Write-Host "   docker pull ${FullTag}:latest"
Write-Host "   docker pull ${FullTag}:${Version}"
Write-Host ""
Write-Host "🌐 https://hub.docker.com/r/${DockerUser}/${ImageName}"
Write-Host ""
Write-Host "▶  Быстрый запуск:"
Write-Host "   docker run -d -p 8080:8080 -v medmonitor_data:/app/data --name medmonitor ${FullTag}:latest"
Write-Host "   http://localhost:8080"
Write-Host "==============================================" -ForegroundColor Cyan
