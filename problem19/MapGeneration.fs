module Lab1.Problem19.MapGen

let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    year % 4 = 0 && year % 100 <> 0 || year % 400 = 0

let dates =
    [ for y in 1901..2000 do
          for m in 0..11 -> y, m ]

let rec calculateFirstDay (year, month) =
    let rec daysInYear y =
        [ for m in 0..11 -> if m = 1 && isLeapYear y then 29 else daysInMonth.[m] ]

    let rec daysUpToYear y =
        match y with
        | 1901 -> 0
        | _ -> (daysInYear (y - 1) |> List.sum) + daysUpToYear (y - 1)

    let rec daysUpToMonth y m =
        match m with
        | 0 -> 0
        | _ ->
            (if m = 1 && isLeapYear y then 29 else daysInMonth.[m - 1])
            + daysUpToMonth y (m - 1)

    let totalDays = daysUpToYear year + daysUpToMonth year month
    (totalDays + 2) % 7

let mapGenSolution () =
    dates
    |> List.map (fun date -> if calculateFirstDay date = 0 then 1 else 0)
    |> List.sum
