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

let private toResource (dictionaryEntry:DictionaryEntry) =
    makeResource (string dictionaryEntry.Key) (string dictionaryEntry.Value)

let getMessages (culture:string) =
    resourceManager.GetResourceSet(new CultureInfo(culture), true, true)
    |> Seq.cast<DictionaryEntry>
    |> Seq.map toResource
