#!/usr/bin/env bash

echo "dotnet restore"
dotnet restore "./../Source/NLib.Sln"
dotnet build "./../Source/NLib.Sln"