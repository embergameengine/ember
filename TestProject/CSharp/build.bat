@echo off

REM Build application
dotnet build

REM Copy assets
robocopy ..\Assets bin\Debug\net6.0 /s /e

REM Copy libraries
robocopy lib bin\Debug\net6.0 /s /e