module Lab1.Problem19.Loops

let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    year % 4 = 0 && year % 100 <> 0 || (year % 400 = 0)

let countSundaysForLoop () =
    let monthsSeq =
        seq {
            for year in 1901..2000 do
                for month in 0..11 do
                    yield (year, month)
        }

    let finalDay, count =
        Seq.fold
            (fun (dayOfWeek, count) (year, month) ->
                let count = if dayOfWeek = 0 then count + 1 else count

                let days =
                    if month = 1 && isLeapYear year then
                        29
                    else
                        daysInMonth.[month]

                let dayOfWeek = (dayOfWeek + days) % 7
                (dayOfWeek, count))
            (2, 0)
            monthsSeq

    count


let loopsSolution () = countSundaysForLoop ()
