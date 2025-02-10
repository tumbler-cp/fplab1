module Lab1.Problem12.InfSeq

let triangleNumber n = n * (n + 1) / 2

let countDivisors n =
    let limit = int (sqrt (float n))

    seq { 1..limit }
    |> Seq.filter (fun i -> n % i = 0)
    |> Seq.collect (fun i ->
    match i with
        | _ when i = n / i -> [ i ]
        | _ -> [ i; n / i ]
    )
    |> Seq.length

let triangleNumbers = Seq.initInfinite (fun n -> triangleNumber (n + 1))

let infiniteSequenceSolution () =
    triangleNumbers |> Seq.find (fun n -> countDivisors n > 500)
