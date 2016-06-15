module LongestCommonSubsequenceTests

open Xunit
open FsCheck
open FsCheck.Xunit
open LongestCommonSubsequence

[<Literal>]
let maxLength = 12
[<Literal>]
let numTests = 1000

[<Fact>]
let ``LCS of "ABCDE" and "ABXDY" is "ABD"``() =
    Assert.Equal("ABD", strGet "ABCDE" "ABXDY")

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``Identity`` (x:char list) = 
    get x x = x

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``Absorber`` (x:char list) = 
    get x [] = [] && get [] x = []

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``Idempotence`` (x:char list) (y:char list) = 
    let lcs = get x y
    get x lcs = lcs && get y lcs = lcs

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``Prepending a char to both inputs gives an LCS starting with that char`` (x:char list) (y:char list) c = 
    let lcs = get (c::x) (c::y)
    List.head lcs = c    