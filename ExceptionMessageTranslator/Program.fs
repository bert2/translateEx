module ExceptionMessageTranslator

open Translator

[<EntryPoint>]
let main argv = 
//    let message = "Das Argument muss zwischen -2 und 2000 liegen."
    let message = "Impossible de créer une instance de ExceptionMessageTranslator, car il s'agit d'une classe abstraite."

    printfn "Searching for:"
    printfn "%A" message

    let translatedMessage = translateToEng "fr-FR" message
    
    printfn "\nTranslation:"
    printfn "%A" translatedMessage

    System.Console.ReadKey(true) |> ignore
    0