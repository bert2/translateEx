module Translator

open ExceptionMessages
open Matcher

let translate toCulture fromCulture message =
    let resources = getMessageResources fromCulture
    let (MatchResult (Resource (key, _), _)) = getBestMatch message resources
    getMessage toCulture key

let translateToEng = translate "en"