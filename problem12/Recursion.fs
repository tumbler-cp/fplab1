module Lab1.Problem12.Recursion

let numTriangle n = n * (n + 1) / 2

let countDivs num =
    let rec count n i acc =
        match i * i > n, n % i = 0, i = n / 1 with
        | true, _, _ -> acc
        | false, true, true -> count n (i + 1) (acc + 1)
        | false, true, false -> count n (i + 1) (acc + 2)
        | false, false, _ -> count n (i + 1) acc

    count num 1 0

let tailRecursiveSolution minDivisors =
    let rec loop n =
        let triN = numTriangle n
        let divs = countDivs triN

        match divs > minDivisors with
        | true -> triN
        | false -> loop (n + 1)

    loop 1

let rec recursiveSolution n =
    let tri = numTriangle n

    match countDivs tri > 500 with
    | true -> tri
    | false -> recursiveSolution (n + 1)
