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
