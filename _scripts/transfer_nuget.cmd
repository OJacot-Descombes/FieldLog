@echo off
cd /d "%~dp0"
%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe -ExecutionPolicy unrestricted -File buildscript\psbuild.ps1 "transfer-nuget" %*
exit /b %errorlevel%
