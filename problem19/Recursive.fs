module Lab1.Problem19.Recursive

let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    (year % 4 = 0 && year % 100 <> 0) || (year % 400 = 0)


let recursiveSolution () =
    let rec countSundaysRec year month dayOfWeek =
        if year > 2000 then
            0
        else
            let count = if dayOfWeek = 0 then 1 else 0

            let days =
                if month = 1 && isLeapYear year then
                    29
                else
                    daysInMonth.[month]

            let nextDayOfWeek = (dayOfWeek + days) % 7
            let (newYear, newMonth) = if month = 11 then (year + 1, 0) else (year, month + 1)
            count + countSundaysRec newYear newMonth nextDayOfWeek

    countSundaysRec 1901 0 2
