module TranslateController

open System.Net
open System.Net.Http
open System.Web.Http
open Language
open Translator

type TranslateController() =
    inherit ApiController()

    [<Route("{targetLang?}/{sourceLang?}/{message?}")>]
    member this.Get targetLang sourceLang message = 
        let translatedMessage = translate (Language.create targetLang) (Language.create sourceLang) message
        this.Request.CreateResponse(HttpStatusCode.OK, translatedMessage)
