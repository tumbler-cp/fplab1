module Lab1.Problem19.MapGen

let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    (year % 4 = 0 && year % 100 <> 0) || (year % 400 = 0)

let dates =
    [ for y in 1901..2000 do
          for m in 0..11 -> y, m ]

let calculateFirstDay (year, month) =
    let daysInYear y =
        [ for m in 0..11 -> if m = 1 && isLeapYear y then 29 else daysInMonth.[m] ]

    let daysUpToYear y =
        seq { 1901 .. y - 1 }
        |> Seq.map (fun yr -> daysInYear yr |> List.sum)
        |> Seq.sum

    let daysUpToMonth y m = daysInYear y |> List.take m |> List.sum

    let totalDays = daysUpToYear year + daysUpToMonth year month
    (totalDays + 2) % 7

let mapGenSolution () =
    dates
    |> List.map (fun date -> if calculateFirstDay date = 0 then 1 else 0)
    |> List.sum
