module Lab1.Problem12.Modular

let triangleNumber n = n * (n + 1) / 2

let countDivisors n =
    let limit = int (sqrt (float n))

    seq { 1..limit }
    |> Seq.filter (fun i -> n % i = 0)
    |> Seq.collect (fun i ->
        match i with
        | _ when i = n / i -> [ i ]
        | _ -> [ i; n / i ])
    |> Seq.length

let generateTriangleNumbers limit =
    seq { 1..limit } |> Seq.map triangleNumber

let filterTriangleNumbers seqNums =
    seqNums |> Seq.filter (fun n -> countDivisors n > 500)

let findFirst seqNums = seqNums |> Seq.head

let modularSolution () =
    generateTriangleNumbers 100_000 |> filterTriangleNumbers |> findFirst
