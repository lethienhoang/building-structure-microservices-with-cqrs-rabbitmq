#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd ../App.Services.Identity
dotnet run --no-restore