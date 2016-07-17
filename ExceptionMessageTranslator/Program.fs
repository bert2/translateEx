module ExceptionMessageTranslator

open System.Collections

open ExceptionMessages
open LongestCommonSubsequence
open LevenshteinDistance
open Translator

let run() =
    printfn "Enter two strings:"
    let s1 = System.Console.ReadLine()
    let s2 = System.Console.ReadLine()
    let maxLength = max s1.Length s2.Length
    let lcs = lcs s1 s2
    let dist = editDistance s1 s2
    printfn "LCS: %A (%A)" lcs ((double lcs.Length) / (double maxLength))
    printfn "Edit distance: %A (%A)" dist (1.0 - (double dist) / (double maxLength))
    printfn "Continue? (y/n)"
    System.Console.ReadKey(true).KeyChar <> 'n'

[<EntryPoint>]
let main argv = 
    printfn "Loading resources..."
    let message = "Das Argument muss zwischen 0 und 100 liegen."
    let messages = getMessages "de-DE"

    printfn "Press any key to start searching for %A" message
    System.Console.ReadKey(true) |> ignore
    printfn "Starting search."

    messages
    |> getBestMatch message
    |> printfn "%A"

    while run() do
        ignore None
    0