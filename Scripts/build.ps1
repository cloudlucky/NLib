#!/usr/bin/env powershell
#requires -version 4

SlnPath="./Source/NLib.sln"

dotnet restore $SlnPath
dotnet build $SlnPath
dotnet test $SlnPath