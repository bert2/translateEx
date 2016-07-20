module ExceptionMessages

open System
open System.Collections
open System.Globalization
open System.Reflection
open System.Resources
open ResourceLoader

type ResourceKey = ResourceKey of string

type Resource = Resource of ResourceKey * string

let private toResource (entry:DictionaryEntry) =
    Resource (ResourceKey <| string entry.Key, string entry.Value)

let getMessageResources culture =
    culture
    |> loadResources
    |> Seq.cast<DictionaryEntry>
    |> Seq.map toResource

let getMessage culture (ResourceKey key) =
    let resources = loadResources culture
    resources.GetString(key)