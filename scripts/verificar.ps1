<#
.SYNOPSIS
    Restaura, compila y prueba la solución DiagnosticaTuMascota.sln.
.DESCRIPTION
    Útil antes de abrir un pull request. Se puede ejecutar desde cualquier
    directorio porque usa la ubicación del script como raíz.
.EXAMPLE
    ./scripts/verificar.ps1
#>

$ErrorActionPreference = 'Stop'

$raiz = Split-Path -Parent $PSScriptRoot
$solution = Join-Path $raiz 'DiagnosticaTuMascota.sln'
$proyectoTest = Join-Path $raiz 'tests/DiagnosticaTuMascota.Tests/DiagnosticaTuMascota.Tests.csproj'

Write-Host "==> SDK de .NET" -ForegroundColor Cyan
dotnet --version

Write-Host "==> Restaurar" -ForegroundColor Cyan
dotnet restore $solution

Write-Host "==> Compilar (Release)" -ForegroundColor Cyan
dotnet build $solution -c Release --no-restore

if (Test-Path $proyectoTest) {
    Write-Host "==> Pruebas" -ForegroundColor Cyan
    dotnet test $solution -c Release --no-build
}
else {
    Write-Host "==> Sin proyecto de pruebas; se omite dotnet test" -ForegroundColor Yellow
}

Write-Host "Verificación completada." -ForegroundColor Green
