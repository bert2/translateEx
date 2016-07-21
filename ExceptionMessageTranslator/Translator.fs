module Translator

open ExceptionMessages
open Language
open Matcher

let private toMatchCandidate (Resource (key, text)) =
    MatchCandidate (Id key, text)

let translate targetLang sourceLang exceptionMessage =
    getMessageResources sourceLang
    |> Seq.map toMatchCandidate
    |> findBestMatch exceptionMessage
    |> getMessage targetLang