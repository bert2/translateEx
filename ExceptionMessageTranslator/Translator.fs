module Translator

open System.Collections
open ExceptionMessages
open LongestCommonSubsequence
open LevenshteinDistance

type MatchScore = MatchScore of double

type MatchResult = MatchResult of Resource * MatchScore

let score (MatchResult (_, score)) = score

let private getSimilarityWithLcs (string1:string) (string2:string) =
    let maxLength = max string1.Length string2.Length
    let lcs = lcs string1 string2
    (double lcs.Length) / (double maxLength)

let private getSimilarityWithLevenshtein (string1:string) (string2:string) =
    let maxDist = max string1.Length string2.Length
    let actualDist = editDistance string1 string2
    1.0 - (double actualDist) / (double maxDist)

let getMatchScore (text:string) (Resource (key, resourceText)) =
    let score = MatchScore (getSimilarityWithLevenshtein text resourceText)
    MatchResult (Resource (key, resourceText), score)

let getMatchScores (exceptionMessage:string) (sourceMessages:Resource seq) =
    sourceMessages
    |> Seq.map (getMatchScore exceptionMessage)
    |> Seq.sortByDescending score

let getBestMatch (exceptionMessage:string) (sourceMessages:Resource seq) = 
    getMatchScores exceptionMessage sourceMessages
    |> Seq.head