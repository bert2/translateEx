module LCS

open Xunit
open FsCheck
open FsCheck.Xunit
open LongestCommonSubsequence

[<Literal>]
let maxLength = 30
[<Literal>]
let numTests = 1000

[<Fact>]
let ``.. of "ABCDE" and "ABXDY" is "ABD"``() =
    Assert.Equal("ABD", lcs "ABCDE" "ABXDY")

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. has identity property`` (x:char list) = 
    lcs' x x = x

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. has absorber property`` (x:char list) = 
    lcs' x [] = [] && lcs' [] x = []

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. has idempotence property`` (x:char list) (y:char list) = 
    let lcs = lcs' x y
    lcs' x lcs = lcs && lcs' y lcs = lcs

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. starts with the char that has been prepended to both intputs`` (x:char list) (y:char list) c = 
    let lcs = lcs' (c::x) (c::y)
    List.head lcs = c    