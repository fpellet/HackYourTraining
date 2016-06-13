echo off

sh ./restore.dependencies.sh

echo "Running web server"
mono packages/FAKE/tools/FAKE.exe run --fsiargs build.local.fsx