module Lab1.Problem12.Loops

let triangleNumber n = n * (n + 1) / 2

let countDivisors n =
    let limit = int (sqrt (float n))
    let mutable count = 0

    for i in 1..limit do
        if n % i = 0 then
            count <- count + (if i = n / i then 1 else 2)

    count

let loopSolution () =
    let mutable n = 1
    let mutable loop = true
    let mutable result = 0

    while loop do
        let tri = triangleNumber n

        if countDivisors tri > 500 then
            result <- tri
            loop <- false

        n <- n + 1

    result
