### Ходжаев А.А., 368994, P3308

## Функциональное программирование
# Лабораторная работа №1

## Вариант

**12. Highly Divisible Triangular Number** и **19. Counting Sundays**

## Проблема №12
Последовательность треугольных чисел образуется путем сложения натуральных чисел. К примеру, 7-е треугольное число равно 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. Первые десять треугольных чисел:

1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...

Перечислим делители первых семи треугольных чисел:

     1: 1
     3: 1, 3
     6: 1, 2, 3, 6
    10: 1, 2, 5, 10
    15: 1, 3, 5, 15
    21: 1, 3, 7, 21
    28: 1, 2, 4, 7, 14, 28 

Как мы видим, 28 - первое треугольное число, у которого более пяти делителей.

Каково первое треугольное число, у которого более пятисот делителей?

### Хвостовая рекурсия
Для генерации треугольних чисел используется

```fsharp
let numTriangle n = n * (n + 1) / 2
```

А для подсчёта делитей
```fsharp
let countDivs num =
    let rec count n i acc =

        match () with
        | _ when i * i > n -> acc
        | _ when n % i = 0 && i = n / 1 -> count n (i + 1) (acc + 1)
        | _ when n % i = 0 -> count n (i + 1) (acc + 2)
        | _ -> count n (i + 1) acc

    count num 1 0
```

А сама хвостовая рекурсия в ```tailRecSolution```
```fsharp
let rec loop n =

        let tri = numTriangle n

        match countDivs tri > minDivisors with
        | true -> tri
        | false -> loop (n + 1)
```

### Модульная реализация

Генерация треугольного числа
```fsharp
let triangleNumber n = n * (n + 1) / 2
```
Подсчёт
```fsharp
let countDivisors n =
    let limit = int (sqrt (float n))

    seq { 1..limit }
    |> Seq.filter (fun i -> n % i = 0)
    |> Seq.collect (fun i ->
        match i with
        | _ when i = n / i -> [ i ]
        | _ -> [ i; n / i ])
    |> Seq.length
```
Генерация последовательности
```fsharp
let generateTriangleNumbers limit =
    seq { 1..limit } |> Seq.map triangleNumber
```
Фильтрация
```fsharp
let filterTriangleNumbers seqNums =
    seqNums |> Seq.filter (fun n -> countDivisors n > 500)
```
Свёртка
```fsharp
let findFirst seqNums = seqNums |> Seq.head
```
Результат
```fsharp
let modularSolution () =
    generateTriangleNumbers 100_000 |> filterTriangleNumbers |> findFirst
```

### Генерация с помощью map
Вспомогательные функции идентичны функциям из рекурсивного подхода
Функция нахождения нужного числа с помощью map

```fsharp
let generateTriangleNumbers limit =
    seq { 1..limit } |> Seq.map triangleNumber

let mapGenSolution () =
    generateTriangleNumbers 100_100
    |> Seq.find (fun n -> countDivisors n > 500)
```

**!** Используется функция генерации последовательности из модульной реализации

### Бесконечная последовательность
```fsharp
let mapGenSolution () =
    Seq.initInfinite id
    |> Seq.map (fun n -> triangleNumber (n + 1))
    |> Seq.find (fun n -> countDivisors n > 500)
```

Да это функция из предыдущей реализации но с бесконечной последовательностью

### Циклы
Используются выражение ```for ... in ... do```. ```while <bool> do``` не используется, т.к. требует мутабелькую перемнную для остановки
```fsharp
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
```

## Проблема №19
Дана следующая информация (однако, вы можете проверить ее самостоятельно):

    - 1 января 1900 года - понедельник.
    - В апреле, июне, сентябре и ноябре 30 дней.
    В феврале 28 дней, в високосный год - 29.
    В остальных месяцах по 31 дню.
    - Високосный год - любой год, делящийся нацело на 4, однако последний год века (ХХ00) является високосным в том и только том случае, если делится на 400.

Сколько воскресений выпадает на первое число месяца в двадцатом веке (с 1 января 1901 года до 31 декабря 2000 года)?

### Примечание
Почти во всех реализацию используются этот массив дней в месяце и функция определения высокосности года
```fsharp
let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    (year % 4 = 0 && year % 100 <> 0) || (year % 400 = 0)
```

### Хвостовая рекурсия
Сама рекурсия
```fsharp
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
```
Чтобы сделать рекурсию хвостовой без проблем, используется аккумулятор
### Обычная рекурсия
Поэтому реализация через обычную рекурсия, схожа, но эта рекурсия не хвостовая т.к. возвращается вызов с выражением, а не просто вызов
```fsharp
let rec countSundaysRec year month dayOfWeek =
        match year, month with
        | y, _ when y > 2000 -> 0
        | _ ->
            let count = 
                match dayOfWeek with 
                | 0 -> 1
                |_ -> 0

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
```
### Модульная реализация
Генерация месяцев
```fsharp
let dates =
    [ for y in 1901..2000 do
          for m in 0..11 -> (y, m) ]
```
Нахождение первого дня
```fsharp
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
        | _ -> (if m = 1 && isLeapYear y then 29 else daysInMonth.[m - 1]) + daysUpToMonth y (m - 1)
    
    let totalDays = daysUpToYear year + daysUpToMonth year month
    (totalDays + 2) % 7
```
Результат
```fsharp
let modularSolution () =
    dates |> List.filter (fun date -> calculateFirstDay date = 0) |> List.length
```
### Генерация с помощью map
Схожа с модульной реализацией, но генерируется список единиц и находится сумма элементов списка
```fsharp
let mapGenSolution () =
    dates
    |> List.map (fun date -> if calculateFirstDay date = 0 then 1 else 0)
    |> List.sum
```
### Бесконечные последовательности
Меняется генерация месяцев
```fsharp
let infiniteDates =
    Seq.initInfinite (fun i ->
        let year = 1901 + i / 12
        let month = i % 12
        year, month)
    |> Seq.takeWhile (fun (y, _) -> y <= 2000)
```
Нахождение результата как в предыдущей реализации но с новой генерацией последовательности
```fsharp
let infSeqSolution () =
    infiniteDates
    |> Seq.map (fun date -> match calculateFirstDay date with
                            | 0 -> 1
                            | _ -> 0)
    |> Seq.sum
```

### Циклы
Подсчёт в цикле и возвращение результата
```fsharp
let countSundaysForLoop () =
    let monthsSeq = seq {
        for year in 1901 .. 2000 do
            for month in 0 .. 11 do
                yield (year, month)
    }
    
    let finalDay, count =
        Seq.fold (fun (dayOfWeek, count) (year, month) ->
            let count = if dayOfWeek = 0 then count + 1 else count
            let days =
                if month = 1 && isLeapYear year then 29
                else daysInMonth.[month]
            let dayOfWeek = (dayOfWeek + days) % 7
            (dayOfWeek, count)
        ) (2, 0) monthsSeq
    count
```

## Вывод
В ходе работы я на практике поняла разницу между написанием кода на функциональных языках и императивных языках. 
