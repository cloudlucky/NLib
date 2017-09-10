#!/usr/bin/env bash

SlnPath="./Source/NLib.sln"

dotnet restore $SlnPath
dotnet build $SlnPath
dotnet test $SlnPath