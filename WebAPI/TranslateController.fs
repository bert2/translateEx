module TranslateController

open System.Net
open System.Net.Http
open System.Web.Http
open Language
open Translator

/// Retrieves values.
type TranslateController() =
    inherit ApiController()

    /// Gets all values.
    [<Route("translate/{targetLang?}/{sourceLang?}/{message?}")>]
    member this.Get targetLang sourceLang message = 
        let translatedMessage = translate (Language.create targetLang) (Language.create sourceLang) message
        this.Request.CreateResponse(HttpStatusCode.OK, translatedMessage)
