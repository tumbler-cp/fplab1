module Lab1.Problem19.Recursive

let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    year % 4 = 0 && year % 100 <> 0 || year % 400 = 0

let recursiveSolution () =
    let rec countSundaysRec year month dayOfWeek =
        match year, month with
        | y, _ when y > 2000 -> 0
        | _ ->
            let count =
                match dayOfWeek with
                | 0 -> 1
                | _ -> 0

            let days =
                match month with
                | 1 when isLeapYear year -> 29
                | m -> daysInMonth.[m]

            let nextDayOfWeek = (dayOfWeek + days) % 7

            let newYear, newMonth =
                match month with
                | 11 -> year + 1, 0
                | _ -> year, month + 1

            count + countSundaysRec newYear newMonth nextDayOfWeek

    countSundaysRec 1901 0 2
