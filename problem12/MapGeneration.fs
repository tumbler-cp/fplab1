module Lab1.Problem12.MapGen

let triangleNumber n = n * (n + 1) / 2

let countDivisors n =
    let limit = int (sqrt (float n))

    seq { 1..limit }
    |> Seq.filter (fun i -> n % i = 0)
    |> Seq.collect (fun i -> if i = n / i then [ i ] else [ i; n / i ])
    |> Seq.length

let generateTriangleNumbers limit =
    seq { 1..limit } |> Seq.map triangleNumber

let mapGenSolution () =
    generateTriangleNumbers 100_100 |> Seq.find (fun n -> countDivisors n > 500)
