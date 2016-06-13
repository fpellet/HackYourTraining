#!/bin/bash

echo off
clear

if [ ! -d ".paket" ]; then
  echo "Installing Paket"
  mkdir .paket
  curl https://github.com/fsprojects/Paket/releases/download/2.50.6/paket.bootstrapper.exe -L --insecure -o .paket/paket.bootstrapper.exe
fi

if [ ! -f ".paket/paket.exe" ]; then
  echo "Downloading Paket"
  mono .paket/paket.bootstrapper.exe prerelease
fi

if [ ! -f "paket.lock" ]; then
  echo "Installing dependencies"
  mono .paket/paket.exe install
fi

echo "Restoring dependencies"
mono .paket/paket.exe restore
