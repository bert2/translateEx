module LongestCommonSubsequence

open System
open System.Collections.Generic

let private longest xs ys = if List.length xs > List.length ys then xs else ys

let private memoize f = 
    let cache = new Dictionary<_, _>()
    fun x y ->
        // Using the two list arguments as cache key is very inefficient. Either
        // because GetHashCode() performs to slow for long lists or because we
        // get to many hash collisions.
        let key = (x, y)
        let found, value = cache.TryGetValue(key)
        if found then
            value
        else
            let value = f x y
            cache.Add(key, value)
            value

/// <summary>Returns the longest common subsequence of two lists.</summary>
#nowarn "40"
let lcs' list1 list2 =
    let rec _lcs = memoize (fun xs ys ->
        match (xs, ys) with
        | ([], _) | (_, []) -> []
        | (x::xs', y::ys') when x = y -> x :: _lcs xs' ys'
        | (x::xs', y::ys') -> longest (_lcs (x::xs') ys') (_lcs xs' (y::ys')))
    _lcs list1 list2

/// <summary>Returns the longest common subsequence of two strings.</summary>
let lcs string1 string2 =
     String.Concat<char> (lcs' (string1 |> List.ofSeq) (string2 |> List.ofSeq))