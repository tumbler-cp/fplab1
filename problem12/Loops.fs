module Lab1.Problem12.Loops

let triangleNumber n = n * (n + 1) / 2

let countDivisors n =
    let limit = int (sqrt (float n))

    seq {
        for i in 1..limit do
            match n % i with
            | 0 when i = n / i -> yield 1
            | 0 -> yield 2
            | _ -> ()
    }
    |> Seq.sum

let loopSolution () =
    let rec loop n =
        let tri = triangleNumber n

        match countDivisors tri with
        | x when x > 500 -> tri
        | _ -> loop (n + 1)

    loop 1
