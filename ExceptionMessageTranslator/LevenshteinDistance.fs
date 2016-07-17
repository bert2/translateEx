// Implementation taken from https://en.wikibooks.org/wiki/Algorithm_Implementation/Strings/Levenshtein_distance#F.23
module LevenshteinDistance

let inline min3 one two three = 
    if one < two && one < three then one
    elif two < three then two
    else three

let editDistance (s: string) (t: string) =
    let m = s.Length
    let n = t.Length
    let d = Array2D.create (m+1) (n+1) -1
    let rec dist =
        function
        | i, 0 -> i
        | 0, j -> j
        | i, j when d.[i,j] <> -1 -> d.[i,j]
        | i, j ->
            let dval = 
                if s.[i-1] = t.[j-1] then dist (i-1, j-1)
                else
                    min3
                        (dist (i-1, j)   + 1) // a deletion
                        (dist (i,   j-1) + 1) // an insertion
                        (dist (i-1, j-1) + 1) // a substitution
            d.[i, j] <- dval; dval 
    dist (m, n)