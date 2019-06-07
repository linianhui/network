#!/bin/sh

SCRIPT='0-build/build.cake'
CAKE_VERSION='0.33.0'

# git commit sha
GIT_COMMIT_SHA=$(git rev-parse --short HEAD)

# Install  cake.tool
dotnet tool install --global cake.tool --version $CAKE_VERSION
export PATH="$PATH:$HOME/.dotnet/tools"

# Start Cake
CAKE_ARGS="$SCRIPT --verbosity=diagnostic -git-commit-sha=$GIT_COMMIT_SHA"

echo "\033[32mdotnet cake $CAKE_ARGS $@"

dotnet cake $CAKE_ARGS "$@"
