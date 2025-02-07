module Lab1.Problem12.Recursion

let numTriangle n = n * (n + 1) / 2

let countDivs num =
    let rec count n i acc =
        if i * i > n then
            acc
        else if n % i = 0 then
            if i = n / 1 then
                count n (i + 1) (acc + 1)
            else
                count n (i + 1) (acc + 2)
        else
            count n (i + 1) acc

    count num 1 0

let tailRecursiveSolution minDivisors =
    let rec loop n =
        let triN = numTriangle n
        let divs = countDivs triN
        if divs > minDivisors then triN else loop (n + 1)

    loop 1

let rec recursiveSolution n =
    let tri = numTriangle n

    if countDivs tri > 500 then
        tri
    else
        recursiveSolution (n + 1)
