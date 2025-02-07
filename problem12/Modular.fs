module Problem12.Modular

// Генерация
let triangleNumber n = n * (n + 1) / 2

// Подсчёт
let countDivisors n =
    let limit = int (sqrt (float n))

    seq { 1..limit }
    |> Seq.filter (fun i -> n % i = 0)
    |> Seq.collect (fun i -> if i = n / i then [ i ] else [ i; n / i ])
    |> Seq.length

// Генерация
let generateTriangleNumbers limit =
    seq { 1..limit } |> Seq.map triangleNumber

// Фильтрация
let filterTriangleNumbers seqNums =
    seqNums |> Seq.filter (fun n -> countDivisors n > 500)

// Свёртка
let findFirst seqNums = seqNums |> Seq.head

let modularSolution () =
    generateTriangleNumbers 100_000 |> filterTriangleNumbers |> findFirst
