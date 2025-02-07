import math

def count_divisors(n):
    count = 0
    sqrt_n = int(math.sqrt(n))
    for i in range(1, sqrt_n + 1):
        if n % i == 0:
            count += 2 if i * i != n else 1
    return count

def find_triangle_with_divisors(limit):
    n = 1
    triangle = 1
    while count_divisors(triangle) <= limit:
        n += 1
        triangle += n
    return triangle

print(find_triangle_with_divisors(500))
