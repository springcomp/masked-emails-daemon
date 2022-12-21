#!/bin/env bash
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
pushd $SCRIPT_DIR
func host start \
    --no-build \
    --language-worker dotnet-isolated
