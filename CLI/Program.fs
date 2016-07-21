module ExceptionMessageTranslator

open Language
open Translator

type Args = Args of Language.T option * Language.T option * string

let private gatherUserInputs =
    Args (
        Language.create "de", 
        Language.create "fr", 
        "Impossible de créer une instance de ExceptionMessageTranslator, car il s'agit d'une classe abstraite.")

let private parseArguments (argv:string []) =
    match argv.Length with
    | 0 -> gatherUserInputs
    | 1 -> Args (None, None, argv.[0])
    | 2 -> Args (None, Language.create argv.[0], argv.[1])
    | _ -> Args (Language.create argv.[0], Language.create argv.[1], argv.[2])

[<EntryPoint>]
let main argv = 
    let (Args (targetLang, sourceLang, message)) = parseArguments argv
    printfn "Translating message from '%s' to '%s'..." (Language.toString sourceLang) (Language.getSafe targetLang)
    let translatedMessage = translate targetLang sourceLang message
    printfn "Translation:"
    printfn "'%s'" translatedMessage

#if DEBUG
    System.Console.ReadKey(true) |> ignore
#endif
    0
