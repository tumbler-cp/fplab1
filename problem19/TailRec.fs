module Lab1.Problem19.TailRec

let tailRecSolution () =
    let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

    let isLeapYear year =
        (year % 4 = 0 && year % 100 <> 0) || (year % 400 = 0)

    let rec countSundaysTailRec year month dayOfWeek acc =
        if year > 2000 then
            acc
        else
            let newAcc = if dayOfWeek = 0 then acc + 1 else acc

            let days =
                if month = 1 && isLeapYear year then
                    29
                else
                    daysInMonth.[month]

            let nextDayOfWeek = (dayOfWeek + days) % 7
            let (newYear, newMonth) = if month = 11 then (year + 1, 0) else (year, month + 1)
            countSundaysTailRec newYear newMonth nextDayOfWeek newAcc

    countSundaysTailRec 1901 0 2 0
