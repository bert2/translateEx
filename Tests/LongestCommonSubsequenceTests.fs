module LongestCommonSubsequenceTests

open NUnit.Framework
open FsCheck
open FsCheck.NUnit
open LongestCommonSubsequence

[<Literal>]
let maxLength = 12
[<Literal>]
let numTests = 1000

[<Test>]
let ``LCS of "ABCDE" and "ABXDY" is "ABD"``() =
    Assert.AreEqual("ABD", strGet "ABCDE" "ABXDY")

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

//[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
//let ``LCS is a subset of both its inputs`` (x:char list) (y:char list) c = 
//    let lcs = get (c::x) (c::y)
    