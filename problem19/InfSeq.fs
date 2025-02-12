module Lab1.Problem19.InfSeq

let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    year % 4 = 0 && year % 100 <> 0 || year % 400 = 0

let calculateFirstDay (year, month) =
    let mutable dayOfWeek = 2

    for y in 1901 .. year - 1 do
        for m in 0..11 do
            let days =
                match m with
                | 1 when isLeapYear y -> 29
                | _ -> daysInMonth.[m]

            dayOfWeek <- (dayOfWeek + days) % 7

    for m in 0 .. month - 1 do
        let days =
            match m with
            | 1 when isLeapYear year -> 29
            | _ -> daysInMonth.[m]

        dayOfWeek <- (dayOfWeek + days) % 7

    dayOfWeek

let infiniteDates =
    Seq.initInfinite (fun i ->
        let year = 1901 + i / 12
        let month = i % 12
        year, month)
    |> Seq.takeWhile (fun (y, _) -> y <= 2000)

let infSeqSolution () =
    infiniteDates
    |> Seq.map (fun date ->
        match calculateFirstDay date with
        | 0 -> 1
        | _ -> 0)
    |> Seq.sum
