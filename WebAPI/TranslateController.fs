module TranslateController

open System.Net
open System.Net.Http
open System.Web.Http

/// Retrieves values.
type TranslateController() =
    inherit ApiController()

    /// Gets all values.
    member this.Get() = 
        this.Request.CreateResponse(HttpStatusCode.OK, "test")
