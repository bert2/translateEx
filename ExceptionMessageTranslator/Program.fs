module ExceptionMessageTranslator

open Translator

[<EntryPoint>]
let main argv = 
    let message = "Das Argument muss zwischen -2 und 2000 liegen."
    printfn "Searching for:"
    printfn "%A" message

    let translatedMessage = translateToEng "de-DE" message
    
    printfn "\nTranslation:"
    printfn "%A" translatedMessage

    System.Console.ReadKey(true) |> ignore
    0