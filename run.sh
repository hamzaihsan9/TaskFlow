#!/usr/bin/env bash
set -e

cd "$(dirname "$0")"

echo "=========================================="
echo "Starting TaskFlow Web API (.NET 6)..."
echo "=========================================="

dotnet run --project src/TaskFlow.API/TaskFlow.API.csproj
