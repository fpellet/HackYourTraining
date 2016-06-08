echo off

sh ./restore.dependencies.linux.sh

echo "Running web server"
mono packages/FAKE/tools/FAKE.exe run --fsiargs build.local.fsx