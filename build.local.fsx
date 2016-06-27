#I "packages/FAKE/tools" //used for linux build
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO
open System.Diagnostics

let build () = 
    MSBuildHelper.MSBuildLoggers <- []
    MSBuildDebug "./build" "Build" [__SOURCE_DIRECTORY__ + "/HackYourTraining.fsproj"]

let runAndForget () = 
    fireAndForget (fun info -> 
        info.FileName <- "./build/HackYourTraining.exe"
        info.Arguments <- Path.Combine(__SOURCE_DIRECTORY__, "www") + " 8083")

let stop () = killProcess "HackYourTraining"

let reload = stop >> build >> ignore >> runAndForget

let waitUserStopRequest () = 
    () |> traceLine |> traceLine
    traceImportant "Press any key to stop."
    () |> traceLine |> traceLine

    System.Console.ReadLine() |> ignore
    
let watchSource action =
    !! (__SOURCE_DIRECTORY__ </> "*.fs") 
        |> WatchChanges (fun _ -> action ())
        |> ignore

let reloadOnChange () =
    watchSource reload

let askStop = waitUserStopRequest >> stop

Target "build" (build >> ignore)

Target "run" (runAndForget >> askStop)

Target "watch" (runAndForget >> reloadOnChange >> askStop)

"build"
    ==> "run"

"build"
    ==> "watch"

RunTargetOrDefault "build"