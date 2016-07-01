﻿module app

open System
open System.Linq
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

let interestedTrainees = 
    ["Emilien Pecoul", "https://twitter.com/Ouarzy";
     "Florent Pellet", "https://twitter.com/florentpellet";
     "Clément Bouillier", "https://twitter.com/clem_bouillier";
     "Jean Helou", "https://twitter.com/jeanhelou"
     "Yannick Ringapin", "https://twitter.com/BlackBeard486";
     "Kevin Lejeune", "https://twitter.com/kevin_le_jeune";
     "Karol Chmist", "https://twitter.com/karolchmist";
     "Gregory Cica", "";
     "Samuel Pecoul", "https://twitter.com/SamPecoul";
     "Nadège Rouelle", "https://twitter.com/nadegerouelle";
     "Romain Berthon", "https://twitter.com/RomainTrm";
     ]

let getInterestedTrainees =
    interestedTrainees
    |> List.map (fun trainee -> sprintf "{ \"name\": \"%s\", \"twitterUrl\": \"%s\" }" (fst trainee) (snd trainee))
    |> String.concat ","
    |> sprintf "{ \"interestedTrainees\": [ %s ] }" 

let app : WebPart =
    choose 
        [ 
            Filters.GET >=> choose [ Filters.path "/" >=> Files.browseFileHome "index.html"; Files.browseHome ] 
            GET >=> choose
                [ path "/interestedTrainees" >=> request (fun req -> OK getInterestedTrainees)]   
            RequestErrors.NOT_FOUND "Page not found." 
        ]