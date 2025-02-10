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

```
let numTriangle n = n * (n + 1) / 2
```

А для подсчёта делитей
```
let countDivs num =
    let rec count n i acc =
        if i * i > n then
            acc
        else if n % i = 0 then
            if i = n / 1 then
                count n (i + 1) (acc + 1)
            else
                count n (i + 1) (acc + 2)
        else
            count n (i + 1) acc

    count num 1 0
```

А сама хвостовая рекурсия в ```tailRecSolution```
```
let rec loop n =
        let triN = numTriangle n
        let divs = countDivs triN
        if divs > minDivisors then triN else loop (n + 1)
```

### Обычная рекурсия
Идентична решению с хвостовой рекурсией, т.к. 

### Модульная реализация

Генерация треугольного числа
```
let triangleNumber n = n * (n + 1) / 2
```
Подсчёт
```
let countDivisors n =
    let limit = int (sqrt (float n))

    seq { 1..limit }
    |> Seq.filter (fun i -> n % i = 0)
    |> Seq.collect (fun i -> if i = n / i then [ i ] else [ i; n / i ])
    |> Seq.length
```
Генерация последовательности
```
let generateTriangleNumbers limit =
    seq { 1..limit } |> Seq.map triangleNumber
```
Фильтрация
```
let filterTriangleNumbers seqNums =
    seqNums |> Seq.filter (fun n -> countDivisors n > 500)
```
Свёртка
```
let findFirst seqNums = seqNums |> Seq.head
```
Результат
```
let modularSolution () =
    generateTriangleNumbers 100_000 |> filterTriangleNumbers |> findFirst
```

### Генерация с помощью map
Вспомогательные функции идентичны функциям из рекурсивного подхода
Функция нахождения нужного числа с помощью map

```
let generateTriangleNumbers limit =
    seq { 1..limit } |> Seq.map triangleNumber

let mapGenSolution () =
    generateTriangleNumbers 100_100
    |> Seq.find (fun n -> countDivisors n > 500)
```

**!** Используется функция генерации последовательности из модульной реализации

### Бесконечная последовательность
```
let mapGenSolution () =
    Seq.initInfinite id
    |> Seq.map (fun n -> triangleNumber (n + 1))
    |> Seq.find (fun n -> countDivisors n > 500)
```

Да это функция из предыдущей реализации но с бесконечной последовательностью

### Циклы
Используются выражения ```for ... in ... do``` и ```while <bool> do```
```
let countDivisors n =
    let limit = int (sqrt (float n))
    let mutable count = 0

    for i in 1..limit do
        if n % i = 0 then
            count <- count + (if i = n / i then 1 else 2)

    count

let loopSolution () =
    let mutable n = 1
    let mutable loop = true
    let mutable result = 0

    while loop do
        let tri = triangleNumber n

        if countDivisors tri > 500 then
            result <- tri
            loop <- false

        n <- n + 1

    result
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
```
let daysInMonth = [| 31; 28; 31; 30; 31; 30; 31; 31; 30; 31; 30; 31 |]

let isLeapYear year =
    (year % 4 = 0 && year % 100 <> 0) || (year % 400 = 0)
```

### Хвостовая рекурсия
Сама рекурсия
```
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
```
Чтобы сделать рекурсию хвостовой без проблем, используется аккумулятор
### Обычная рекурсия
Поэтому реализация через обычную рекурсия, схожа, но эта рекурсия не хвостовая т.к. возвращается вызов с выражением, а не просто вызов
```
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
```
### Модульная реализация
Генерация месяцев
```
let dates =
    [ for y in 1901..2000 do
          for m in 0..11 -> (y, m) ]
```
Нахождение первого дня (с использованием mutable)
```
let calculateFirstDay (year, month) =
    let mutable dayOfWeek = 2

    for y in 1901 .. year - 1 do
        for m in 0..11 do
            let days = if m = 1 && isLeapYear y then 29 else daysInMonth.[m]
            dayOfWeek <- (dayOfWeek + days) % 7

    for m in 0 .. month - 1 do
        let days = if m = 1 && isLeapYear year then 29 else daysInMonth.[m]
        dayOfWeek <- (dayOfWeek + days) % 7

    dayOfWeek
```
Результат
```
let modularSolution () =
    dates |> List.filter (fun date -> calculateFirstDay date = 0) |> List.length
```
### Генерация с помощью map
Схожа с модульной реализацией, но генерируется список единиц и находится сумма элементов списка
```
let mapGenSolution () =
    dates
    |> List.map (fun date -> if calculateFirstDay date = 0 then 1 else 0)
    |> List.sum
```
### Бесконечные последовательности
Меняется генерация месяцев
```
let infiniteDates =
    Seq.initInfinite (fun i ->
        let year = 1901 + (i / 12)
        let month = i % 12
        (year, month))
    |> Seq.takeWhile (fun (y, _) -> y <= 2000)
```
Нахождение результата как в предыдущей реализации но с новой генерацией последовательности
```
let infSeqSolution () =
    infiniteDates
    |> Seq.map (fun date -> if calculateFirstDay date = 0 then 1 else 0)
    |> Seq.sum
```

### Циклы
Подсчёт в цикле и возвращение результата
```
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
```

## Вывод
В ходе работы я на практике поняла разницу между написанием кода на функциональных языках и императивных языках. 
