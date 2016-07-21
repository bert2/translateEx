module ResourceLoader

open System.Reflection
open System.Resources
open Language

[<Literal>]
let private exceptionResourceNamePrefix = "mscorlib"
let private assembly = Assembly.GetExecutingAssembly()
let private resourceNames = assembly.GetManifestResourceNames()

let private tryFindResourceFor culture =
    let resourceName = sprintf "%s.%s.461.resources" exceptionResourceNamePrefix culture
    resourceNames
    |> Array.tryFind ((=) resourceName)

let private trimExtension (resourceName:string) =
    resourceName.LastIndexOf '.'
    |> resourceName.Remove

let private getResourceName culture =
    match tryFindResourceFor culture with
    | Some resource -> resource
    | None ->
        match tryFindResourceFor <| parentOf culture with
        | Some resource -> resource
        | None -> failwithf "Culture '%s' is not supported." culture

let private loadResourceFile resourceName =
    let resourceManager = new ResourceManager(resourceName, assembly)
    // As long as the tryParents flag is true GetResourceSet() will return the loaded
    // resource no matter what CultureInfo we pass to it.
    resourceManager.GetResourceSet(defaultCulture, true, true)

let loadAllResources =
    resourceNames
    |> Seq.filter (fun rn -> rn.StartsWith(exceptionResourceNamePrefix))
    |> Seq.map trimExtension
    |> Seq.map loadResourceFile

let loadResourcesFor culture =
    let resourceName = culture |> getResourceName |> trimExtension
    loadResourceFile resourceName
    