module Lab1.Problem12.Recursion

let numTriangle n = n * (n + 1) / 2

let countDivs num =
    let rec count n i acc =

        match () with
        | _ when i * i > n -> acc
        | _ when n % i = 0 && i = n / 1 -> count n (i + 1) (acc + 1)
        | _ when n % i = 0 -> count n (i + 1) (acc + 2)
        | _ -> count n (i + 1) acc

    count num 1 0

let tailRecursiveSolution minDivisors =
    let rec loop n =

        let tri = numTriangle n

        match countDivs tri > minDivisors with
        | true -> tri
        | false -> loop (n + 1)

    loop 0
