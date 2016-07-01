@echo off

set encoding=utf-8
packages\FAKE\tools\FAKE.exe build.local.fsx %*
