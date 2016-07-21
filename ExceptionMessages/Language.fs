module Language

open System.Globalization

let defaultCulture = new CultureInfo("en")

let parentOf culture =
    let cultureInfo = CultureInfo.CreateSpecificCulture culture
    cultureInfo.TwoLetterISOLanguageName

module Language =

    type T = Language of string

    let create (cultureCode:string) = 
        try
            let _ = CultureInfo.CreateSpecificCulture cultureCode
            Some (Language cultureCode)
        with
        | _ -> None

    let get (Language cultureCode) = cultureCode

    let fallbackLanguage = Language "en"