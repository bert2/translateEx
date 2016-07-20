module Translator

open ExceptionMessages
open Matcher

let private getResourceKey (MatchResult (Id key, _)) = key

let private toMatchCandidate (Resource (key, text)) =
    MatchCandidate (Id key, text)

let translate toCulture fromCulture exceptionMessage =
    getMessageResources fromCulture
    |> Seq.map toMatchCandidate
    |> findBestMatch exceptionMessage
    |> getResourceKey
    |> getMessage toCulture

let translateToEng = translate "en"