def count_sundays():
    days_in_months = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]
    day_of_week = 2 
    sunday_count = 0

    for year in range(1901, 2001):
        for month in range(12):
            if day_of_week == 6:
                sunday_count += 1

            days = days_in_months[month]

            if month == 1 and (year % 4 == 0 and (year % 100 != 0 or year % 400 == 0)):
                days += 1

            day_of_week = (day_of_week + days) % 7

    return sunday_count

print(count_sundays())
