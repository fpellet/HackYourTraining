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

Target "build" (fun _ -> 
    build () |> ignore
)

Target "run" (fun _ -> 
    run ()
)

Target "watch" (fun _ -> 
    let reload _ =
        killProcess "HackYourTraining"
        build () |> ignore
        runAndForget ()

    !! (__SOURCE_DIRECTORY__ </> "*.fs") 
        |> WatchChanges reload 
        |> ignore
)

RunTargetOrDefault "build"