echo off

if [ ! -f ".paket/paket.exe" ]; then
  echo "Downloading Paket"
  mono .paket/paket.bootstrapper.exe
fi

echo "Restoring dependencies"
mono .paket/paket.exe restore

echo "Running web server"
mono packages/FAKE/tools/FAKE.exe run --fsiargs build.local.fsx