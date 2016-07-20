module Matcher

open System.Collections
open LongestCommonSubsequence
open LevenshteinDistance

type Id<'T> = Id of 'T

type MatchScore = MatchScore of double

type MatchCandidate<'T> = MatchCandidate of Id<'T> * string

type MatchResult<'T> = MatchResult of Id<'T> * MatchScore

let private score (MatchResult (_, score)) = score

// Cannot use this ATM, because current LCS implementation is too slow.
// LCS handles format placeholders ("{0}" etc.) better than Levenshtein distance though.
let private normalizedLcs string1 string2 =
    let maxLength = max (String.length string1) (String.length string2)
    let lcs = lcs string1 string2
    (double lcs.Length) / (double maxLength)

let private normalizedEditDistance string1 string2 =
    let maxDist = max (String.length string1) (String.length string2)
    let actualDist = editDistance string1 string2
    1.0 - (double actualDist) / (double maxDist)

let private getMatchScore targetString (MatchCandidate (id, candiateString)) =
    let similarity = normalizedEditDistance targetString candiateString
    MatchResult (id, MatchScore similarity)

let findBestMatch target candidates =
    candidates
    |> Seq.map (getMatchScore target)
    |> Seq.sortByDescending score
    |> Seq.head