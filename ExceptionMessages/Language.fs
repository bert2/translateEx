module Language

open System.Globalization

[<Literal>]
let defaultLang = "en"
let defaultCulture = new CultureInfo(defaultLang)

let parentOf culture =
    let cultureInfo = CultureInfo.CreateSpecificCulture culture
    cultureInfo.TwoLetterISOLanguageName

module Language =

    type T = Language of string

    let create (cultureCode:string) = 
        try
            let c = CultureInfo.CreateSpecificCulture cultureCode
            if c = CultureInfo.InvariantCulture then None
            else Some (Language cultureCode)
        with
        | _ -> None

    let fallbackLanguage = Language defaultLang

    let get (Language cultureCode) = cultureCode

    let getSafe (language:T option) =
        match language with
        | None -> get fallbackLanguage
        | Some l -> get l
    
    let toString (language:T option) =
        match language with
        | None -> "unknown"
        | Some l -> get l