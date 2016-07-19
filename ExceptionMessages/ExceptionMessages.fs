module ExceptionMessages

open System
open System.Collections
open System.Globalization
open System.Reflection
open System.Resources

type Key = Key of string

type Resource = Resource of Key * string

let private assembly = Assembly.GetExecutingAssembly()

let private resourceNames = assembly.GetManifestResourceNames()

let private getParentCulture culture =
    let cultureInfo = CultureInfo.CreateSpecificCulture culture
    cultureInfo.TwoLetterISOLanguageName

let private tryFindResource culture =
    let resourceName = sprintf "mscorlib.%s.461.resources" culture
    resourceNames
    |> Array.tryFind (fun rn -> rn = resourceName)

let private getResourceName culture =
    match tryFindResource culture with
    | Some resource -> resource
    | None ->
        match tryFindResource (getParentCulture culture) with
        | Some resource -> resource
        | None -> failwith (sprintf "Failed to load resources for culture %s" culture)

let private trimExtension (resourceName:string) =
    resourceName.LastIndexOf '.'
    |> resourceName.Remove

let private toResource (entry:DictionaryEntry) =
    Resource (Key <| string entry.Key, string entry.Value)

let private loadResources culture =
    let resource = culture |> getResourceName |> trimExtension
    let resourceManager = new ResourceManager(resource, assembly)
    // As long as the tryParents flag is true GetResourceSet() will return the loaded
    // resource no matter what CultureInfo we pass to it.
    resourceManager.GetResourceSet(CultureInfo.CreateSpecificCulture culture, true, true)

let getMessageResources culture =
    culture
    |> loadResources
    |> Seq.cast<DictionaryEntry>
    |> Seq.map toResource

let getMessage culture (Key resourceKey) =
    let resources = loadResources culture
    resources.GetString(resourceKey)