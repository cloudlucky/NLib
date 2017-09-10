#!/usr/bin/env bash

SlnPath="./../Source/NLib.Sln"

dotnet restore $SlnPath
dotnet build $SlnPath