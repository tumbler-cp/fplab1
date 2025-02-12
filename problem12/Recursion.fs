module Lab1.Problem12.Recursion

let numTriangle n = n * (n + 1) / 2

let countDivs num =
    let rec count n i acc =
<<<<<<< HEAD
        match i * i > n, n % i = 0, i = n / 1 with
        | true, _, _ -> acc
        | false, true, true -> count n (i + 1) (acc + 1)
        | false, true, false -> count n (i + 1) (acc + 2)
        | false, false, _ -> count n (i + 1) acc

=======
        match () with
        | _ when i * i > n -> acc
        | _ when n % i = 0 && i = n / 1 -> count n (i + 1) (acc + 1)
        | _ when n % i = 0 -> count n (i + 1) (acc + 2)
        | _ -> count n (i + 1) acc
>>>>>>> d801cb0 (staging)
    count num 1 0

let tailRecursiveSolution minDivisors =
    let rec loop n =
<<<<<<< HEAD
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
=======
        let tri = numTriangle n
        match countDivs tri > minDivisors with
        | true -> tri
        | false -> loop (n + 1)
    loop 0
>>>>>>> d801cb0 (staging)
