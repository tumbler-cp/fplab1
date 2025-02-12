module Lab1.Problem19.TailRec

let tailRecSolution () =
    let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

    let isLeapYear year =
        year % 4 = 0 && year % 100 <> 0 || year % 400 = 0

    let rec countSundaysTailRec year month dayOfWeek acc =
        match year, month with
        | y, _ when y > 2000 -> acc
        | _, m ->
            let newAcc =
                match dayOfWeek with
                | 0 -> acc + 1
                | _ -> acc

            let days =
                match m with
                | 1 when isLeapYear year -> 29
                | _ -> daysInMonth.[m]

            let nextDayOfWeek = (dayOfWeek + days) % 7

            let newYear, newMonth =
                match m with
                | 11 -> year + 1, 0
                | _ -> year, m + 1

            countSundaysTailRec newYear newMonth nextDayOfWeek newAcc

    countSundaysTailRec 1901 0 2 0
