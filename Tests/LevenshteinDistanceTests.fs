module ``Levenshtein distance``

open Xunit
open FsCheck
open FsCheck.Xunit
open LevenshteinDistance

[<Literal>]
let maxLength = 30
[<Literal>]
let numTests = 1000

[<Fact>]
let ``.. of "ABCDE" and "ABXDY" is 2``() =
    Assert.Equal(2, editDistance "ABCDE" "ABXDY")

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. is at least the difference of the sizes of the two strings`` (NonNull s1) (NonNull s2) = 
    let dist = editDistance s1 s2
    let diff = abs (s1.Length - s2.Length)
    dist >= diff

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. is at most the length of the longer string`` (NonNull s1) (NonNull s2) = 
    let dist = editDistance s1 s2
    let longestLength = max s1.Length s2.Length
    dist <= longestLength

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``.. is 0 if and only if the strings are equal`` (NonNull s1) (NonNull s2) = 
    let dist = editDistance s1 s2
    (s1 = s2 && dist = 0) || (s1 <> s2 && dist <> 0)

[<Property(QuietOnSuccess = true, EndSize = maxLength, MaxTest = numTests)>]
let ``..  between two strings is no greater than the sum of their distances from a third string`` (NonNull s1) (NonNull s2) (NonNull s3) = 
    let distOf1To2 = editDistance s1 s2
    let distOf1To3 = editDistance s1 s3
    let distOf2To3 = editDistance s2 s3
    distOf1To2 <= distOf1To3 + distOf2To3