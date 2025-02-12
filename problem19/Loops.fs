module Lab1.Problem19.Loops

let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    year % 4 = 0 && year % 100 <> 0 || year % 400 = 0

let countSundaysForLoop () =
    let mutable dayOfWeek = 2 // Вторник
    let mutable count = 0

    for year in 1901..2000 do
        for month in 0..11 do
            if dayOfWeek = 0 then
                count <- count + 1

            let days =
                if month = 1 && isLeapYear year then
                    29
                else
                    daysInMonth.[month]

            dayOfWeek <- (dayOfWeek + days) % 7

    count

let loopsSolution () = countSundaysForLoop ()
