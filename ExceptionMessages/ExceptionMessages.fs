module ExceptionMessages

open System
open System.Collections
open Language.Language
open ResourceLoader

type ResourceKey = ResourceKey of string

type Resource = Resource of ResourceKey * string

let private toResource (entry:DictionaryEntry) =
    Resource (ResourceKey <| string entry.Key, string entry.Value)

let getMessageResources language =
    let resources = 
        match language with
        | None -> loadAllResources
        | Some (Language l) -> seq { yield loadResourcesFor l }
    resources
    |> Seq.map Seq.cast<DictionaryEntry>
    |> Seq.concat
    |> Seq.map toResource

let getMessage language (ResourceKey key) =
    let resources = 
        match language with
        | None -> loadResourcesFor <| get fallbackLanguage
        | Some (Language l) -> loadResourcesFor l
    resources.GetString(key)