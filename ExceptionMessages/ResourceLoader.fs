module ResourceLoader

open System.Globalization
open System.Reflection
open System.Resources

let private assembly = Assembly.GetExecutingAssembly()

let private resourceNames = assembly.GetManifestResourceNames()

let private parentOf culture =
    let cultureInfo = CultureInfo.CreateSpecificCulture culture
    cultureInfo.TwoLetterISOLanguageName

let private tryFindResourceFor culture =
    let resourceName = sprintf "mscorlib.%s.461.resources" culture
    resourceNames
    |> Array.tryFind ((=) resourceName)

let private getResourceName culture =
    match tryFindResourceFor culture with
    | Some resource -> resource
    | None ->
        match tryFindResourceFor <| parentOf culture with
        | Some resource -> resource
        | None -> failwith (sprintf "Culture %s is not supported" culture)

let private trimExtension (resourceName:string) =
    resourceName.LastIndexOf '.'
    |> resourceName.Remove

let loadResources culture =
    let resource = culture |> getResourceName |> trimExtension
    let resourceManager = new ResourceManager(resource, assembly)
    // As long as the tryParents flag is true GetResourceSet() will return the loaded
    // resource no matter what CultureInfo we pass to it.
    resourceManager.GetResourceSet(CultureInfo.CreateSpecificCulture culture, true, true)