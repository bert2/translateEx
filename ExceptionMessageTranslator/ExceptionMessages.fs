module ExceptionMessages

open System
open System.Collections
open System.Globalization
open System.Resources

type Key = Key of string

type Resource = Resource of Key * string

let private assembly = typeof<Exception>.Assembly

let private resourceManager = new ResourceManager(assembly.GetName().Name, assembly)

let makeResource key text = Resource (Key key, text)

let private toResource (entry:DictionaryEntry) =
    makeResource (string entry.Key) (string entry.Value)

let getMessages (culture:string) =
    let cultureInfo = CultureInfo.CreateSpecificCulture culture
    let resources = resourceManager.GetResourceSet(cultureInfo, true, false)

    if resources = null then 
        failwith (sprintf "Failed to load resources for culture %A." culture)

    resources
    |> Seq.cast<DictionaryEntry>
    |> Seq.map toResource
