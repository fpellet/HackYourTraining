open Suave
open app
open System
open System.Net

[<EntryPoint>]
let main argv = 
    let publicDirectory = argv.[0]
    let port = argv.[1] |> UInt16.Parse

    Console.WriteLine(publicDirectory)

    startWebServer { defaultConfig with 
                        homeFolder = Some publicDirectory
                        bindings = [ HttpBinding.mk HTTP IPAddress.Any port ] } app
    printfn "%A" argv
    0 // return an integer exit code
