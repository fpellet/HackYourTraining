#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO
open System.Diagnostics

let build () = 
    MSBuildHelper.MSBuildLoggers <- []
    MSBuildDebug "./build" "Build" [__SOURCE_DIRECTORY__ + "/HackYourTraining.fsproj"]

let run' (runner: (ProcessStartInfo -> unit) -> unit) =
    runner (fun info -> 
        info.FileName <- "./build/HackYourTraining.exe"
        info.Arguments <- Path.Combine(__SOURCE_DIRECTORY__, "www") + " 8083")

let run () = run' (directExec>>ignore)

let runAndForget () = run' fireAndForget

let stop () = killProcess "HackYourTraining"

let waitUserStopRequest () = 
    () |> traceLine |> traceLine
    traceImportant "Press any key to stop."
    () |> traceLine |> traceLine

    System.Console.ReadLine() |> ignore

Target "build" (fun _ -> 
    build () |> ignore
)

Target "run" (fun _ -> 
    ()
    |> runAndForget
    |> waitUserStopRequest
    |> stop
)

Target "watch" (fun _ -> 
    let reload _ =
        stop ()
        build () |> ignore
        runAndForget ()

    !! (__SOURCE_DIRECTORY__ </> "*.fs") 
        |> WatchChanges reload 
        |> ignore

    ()
    |> waitUserStopRequest
    |> stop
)

"build"
    ==> "run"

"build"
    ==> "watch"

RunTargetOrDefault "build"