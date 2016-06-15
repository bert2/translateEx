open LongestCommonSubsequence

let run() =
    printfn "Enter two strings:"
    let s1 = System.Console.ReadLine()
    let s2 = System.Console.ReadLine()
    let lcs = strGet s1 s2
    printfn "LCS: %A" lcs
    printfn "Continue? (y/n)"
    System.Console.ReadKey(true).KeyChar <> 'n'

[<EntryPoint>]
let main argv = 
    while run() do
        ignore None
    0