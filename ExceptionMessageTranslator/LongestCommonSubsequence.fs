module LongestCommonSubsequence

let get list1 list2 =
    let longest xs ys = if List.length xs > List.length ys then xs else ys
    let rec get' xs ys =
        match (xs, ys) with
        | ([], _) | (_, []) -> []
        | (x::xs', y::ys') when x = y -> x :: get' xs' ys'
        | (x::xs', y::ys') -> longest (get' (x::xs') ys') (get' xs' (y::ys'))
    get' list1 list2

let strGet string1 string2 =
     System.String.Concat<char> (get (string1 |> List.ofSeq) (string2 |> List.ofSeq))