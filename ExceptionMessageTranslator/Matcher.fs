module Matcher

open System.Collections
open ExceptionMessages
open LongestCommonSubsequence
open LevenshteinDistance

type MatchScore = MatchScore of double

type MatchResult = MatchResult of Resource * MatchScore

let private score (MatchResult (_, score)) = score

// Cannot use this ATM, because current LCS implementation is too slow.
// LCS handles format placeholders ("{0}" etc.) better than Levenshtein distance though.
let private getSimilarityWithLcs string1 string2 =
    let maxLength = max (String.length string1) (String.length string2)
    let lcs = lcs string1 string2
    (double lcs.Length) / (double maxLength)

let private getSimilarity string1 string2 =
    let maxDist = max (String.length string1) (String.length string2)
    let actualDist = editDistance string1 string2
    1.0 - (double actualDist) / (double maxDist)

let getMatchScore text (Resource (key, resourceText)) =
    let score = MatchScore (getSimilarity text resourceText)
    MatchResult (Resource (key, resourceText), score)

let getMatchScores message resources =
    resources
    |> Seq.map (getMatchScore message)
    |> Seq.sortByDescending score

let getBestMatch message resources = 
    getMatchScores message resources
    |> Seq.head