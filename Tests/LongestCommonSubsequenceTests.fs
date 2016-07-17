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
let ``.. has identity property`` (NonNull x) = 
    lcs x x = x

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. has absorber property`` (x:NonNull<string>) = 
    lcs x.Get "" = "" && lcs "" x.Get = ""

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. has idempotence property`` (x:NonNull<string>) (y:NonNull<string>) = 
    let result = lcs x.Get y.Get
    lcs x.Get result = result && lcs y.Get result = result

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. starts with the char that has been prepended to both intputs`` (NonNull x) (NonNull y) c = 
    let result = lcs (c.ToString() + x) (c.ToString() + y)
    (Seq.head result) = c    