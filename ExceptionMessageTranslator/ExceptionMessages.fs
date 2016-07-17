module ExceptionMessages

open System
open System.Collections
open System.Globalization
open System.Resources

type Key = Key of string

type Resource = Resource of Key * string

let private assembly = typeof<Exception>.Assembly

let private resourceManager = new ResourceManager(assembly.GetName().Name, assembly)

let private toResource (entry:DictionaryEntry) =
    Resource (Key <| string entry.Key, string entry.Value)

let private loadResources culture =
    let cultureInfo = CultureInfo.CreateSpecificCulture culture
    resourceManager.GetResourceSet(cultureInfo, true, true)

let getMessageResources culture =
    culture
    |> loadResources
    |> Seq.cast<DictionaryEntry>
    |> Seq.map toResource

let getMessage culture (Key resourceKey) =
    let resources = loadResources culture
    resources.GetString(resourceKey)
