module Translator

open ExceptionMessages
open Language
open Matcher

let private getResourceKey (MatchResult (Id key, _)) = key

let private toMatchCandidate (Resource (key, text)) =
    MatchCandidate (Id key, text)

let translate targetLang sourceLang exceptionMessage =
    getMessageResources sourceLang
    |> Seq.map toMatchCandidate
    |> findBestMatch exceptionMessage
    |> getResourceKey
    |> getMessage targetLang