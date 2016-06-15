Exception Message Translator
============================

Translates a localized exception message to English.

To Do
-----

* Longest common subsequence algorithm way too slow
	* Implement dynamic programming approach (utilizing Memoization/Monads)
* Having two LCS methods (one for strings and one for other lists) is ugly
	* Is there a way to have one for both?
	* Is overloading possible?
* Implement actual translation when LCS is fast enough
