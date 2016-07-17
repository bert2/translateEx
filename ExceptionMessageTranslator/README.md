Exception Message Translator

============================



Translates a localized exception message to English.



To Do

-----

* Longest common subsequence algorithm way too slow
	* Implement dynamic programming approach (utilizing Memoization/Monads)
		-> Memoization: http://www.fssnip.net/8P
		-> Memo Monad: http://www.fssnip.net/3v
* Having two LCS methods (one for strings and one for other lists) is ugly
	* Is there a way to have one for both?
		-> Pattern matching can check the runtime type
	* Is overloading possible?
		-> Not w/o classes
* Implement actual translation when LCS is fast enough
	* Get localized messages to match: http://stackoverflow.com/a/13955941/1025555
	* Make language to translate to configurable
	* Make language to translate from optional
	* Return single best match or all/n matches ordered by matching score
* Implement multiple interfaces
	* API
	* CLI
	* Webservice
		* Host with Azure
	* WPF or Xamarin app
* Make .NET framework version configurable
