module Translator

open ExceptionMessages
open Matcher

let private getResourceKey (MatchResult (Id key, _)) = key

let private toMatchCandidate (Resource (key, text)) =
    MatchCandidate (Id key, text)

let translate targetLanguage sourceLanguage exceptionMessage =
    getMessageResources sourceLanguage
    |> Seq.map toMatchCandidate
    |> findBestMatch exceptionMessage
    |> getResourceKey
    |> getMessage targetLanguage