#r "packages/Suave/lib/net40/Suave.dll"
#r "System.Data.Linq.dll"
#r "System.Data.dll"
#r "packages/FSharp.Data.TypeProviders/lib/net40/FSharp.Data.TypeProviders.dll"
#load "staticSite.fsx"

open System
open System.Linq
open System.Data
open System.Data.Linq
open Suave
open Suave.Web
open Suave.Http
open Suave.Successful
open Suave.Redirection
open Suave.Files
open Suave.Filters
open Suave.Operators
open Suave.RequestErrors

open FSharp.Data.TypeProviders
open FSharp.Linq
open StaticSite

let interestedTrainees = 
    ["Emilien Pecoul", "https://twitter.com/Ouarzy";
     "Florent Pellet", "https://twitter.com/florentpellet";
     "Clément Bouillier", "https://twitter.com/clem_bouillier";
     "Jean Helou", "https://twitter.com/jeanhelou"]

let getInterestedTrainees =
    interestedTrainees
    |> List.map (fun trainee -> sprintf "{ \"name\": \"%s\", \"twitterUrl\": \"%s\" }" (fst trainee) (snd trainee))
    |> String.concat ","
    |> sprintf "{ \"interestedTrainees\": [ %s ] }" 

let app : WebPart =
    choose 
        [ GET >=> choose
            [ path "/static/app.js" >=> Writers.setMimeType "application/javascript" >=> OK script
              path "/static/style.css" >=> Writers.setMimeType "text/css" >=> OK style
              path "/" >=> OK html 
              path "/interestedTrainees" >=> request (fun req -> OK getInterestedTrainees)]   
        ]

