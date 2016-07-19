Exception Message Translator
============================

Translates a localized exception message to English.

To Do
-----

* Longest common subsequence algorithm way too slow
	* Implement dynamic programming approach
		-> Memoization with dictionary: http://www.fssnip.net/8P
			-> done: still too slow because (list1, list2).GetHashCode() is slow for long lists
		-> Memo monad: http://www.fssnip.net/3v
			-> won't do: overly complex
		-> Memoization with 2D array
			-> will do: only return LCS length, actual LCS not needed
	* Utilize read-to-use F# implementation of Levenshtein distance
		-> done: faster than current LCS implementation, but does not work as good with string placeholders ("{0}" etc.)
		-> will do: replace with proper LCS implementation
* Implement actual translation when LCS is fast enough
	* Get localized messages to match: http://stackoverflow.com/a/13955941/1025555
	* Done: make language to translate from configurable
	* Retrieving the translation with GetResourceSet() requires installing all language packs
		-> Create new resource file from .NET framework installers
			-> download .NET framework offline installer for all languages
			-> unpack them with 7zip
			-> unpack one of the .cab files
			-> open mscorlib.resources.dll in ILSpy and save resources as mscorlib.xx-XX.resources binary
				-> don't let name end with ".xx-XX.resources" or CSC will create satellite assemblies that are loaded depending on the current culture
				-> use "_xx-XX.resources" to let CSC embed the resources
			-> better add framework version to the file name
			-> add *.resources to project
			-> build project
			-> check name of resource with ILSpy ("<Namespace>.<file name>")
			-> find resources matching input culture with Assembly.GetManifestResourceNames()
			-> load resources with "new ResourceManager(<resource name>, ...)"

	* Make language to translate to optional
	* Return single best match or all/n matches ordered by matching score
* Implement multiple interfaces
	* API
	* CLI
	* Webservice
		* Host with Azure
	* WPF or Xamarin app
* Make .NET framework version configurable
* Don't throw. Catch all possible exceptions and propagte errors with monadic binds.
