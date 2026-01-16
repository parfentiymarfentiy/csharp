using System;

class Program
{
    static void Main(string[] args)
    {
        // TASK 1
        Console.Write("Завдання 1. Введіть число (1-100): ");
        if (int.TryParse(Console.ReadLine(), out int n1) && n1 >= 1 && n1 <= 100)
        {
            if (n1 % 3 == 0 && n1 % 5 == 0) Console.WriteLine("Fizz Buzz");
            else if (n1 % 3 == 0) Console.WriteLine("Fizz");
            else if (n1 % 5 == 0) Console.WriteLine("Buzz");
            else Console.WriteLine(n1);
        }
        else
        {
            Console.WriteLine("Помилка: число повинно бути від 1 до 100");
        }

        // TASK 2
        Console.Write("\nЗавдання 2. Введіть число та відсоток через пробіл: ");
        string[] input2 = Console.ReadLine().Split();
        if (input2.Length == 2 && double.TryParse(input2[0], out double num) && double.TryParse(input2[1], out double percent))
        {
            double result = num * percent / 100;
            Console.WriteLine($"{percent}% від {num} = {result}");
        }
        else
        {
            Console.WriteLine("Некоректний ввід");
        }

        // TASK 3
        Console.Write("\nЗавдання 3. Введіть 4 цифри через пробіл: ");
        string[] digits = Console.ReadLine().Split();
        if (digits.Length == 4 && digits.All(d => d.Length == 1 && char.IsDigit(d[0])))
        {
            string number = string.Concat(digits);
            Console.WriteLine($"Отримане число: {number}");
        }
        else
        {
            Console.WriteLine("Потрібно ввести рівно 4 цифри");
        }

        // TASK 4
        Console.Write("\nЗавдання 4. Введіть шестизначне число: ");
        string numStr = Console.ReadLine();
        if (numStr.Length == 6 && long.TryParse(numStr, out _))
        {
            Console.Write("Введіть два номери розрядів (1-6) через пробіл: ");
            string[] pos = Console.ReadLine().Split();
            if (pos.Length == 2 && int.TryParse(pos[0], out int a) && int.TryParse(pos[1], out int b) &&
                a >= 1 && a <= 6 && b >= 1 && b <= 6 && a != b)
            {
                char[] chars = numStr.ToCharArray();
                (chars[a - 1], chars[b - 1]) = (chars[b - 1], chars[a - 1]);
                Console.WriteLine($"Результат: {new string(chars)}");
            }
            else
            {
                Console.WriteLine("Некоректні номери розрядів");
            }
        }
        else
        {
            Console.WriteLine("Помилка: потрібно ввести шестизначне число");
        }

        // TASK 5
        Console.Write("\nЗавдання 5. Введіть дату (дд.мм.рррр): ");
        if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            string season = date.Month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                9 or 10 or 11 => "Autumn",
                _ => "Unknown"
            };
            string dayOfWeek = date.DayOfWeek.ToString();
            Console.WriteLine($"{season} {dayOfWeek}");
        }
        else
        {
            Console.WriteLine("Неправильний формат дати");
        }

        // TASK 6
        Console.Write("\nЗавдання 6. Введіть температуру: ");
        if (double.TryParse(Console.ReadLine(), out double temp))
        {
            Console.Write("Введіть 1 (C→F) або 2 (F→C): ");
            string choice = Console.ReadLine();
            double resultTemp = choice switch
            {
                "1" => (temp * 9 / 5) + 32,
                "2" => (temp - 32) * 5 / 9,
                _ => double.NaN
            };

            if (!double.IsNaN(resultTemp))
                Console.WriteLine(choice == "1" ? $"Результат: {resultTemp:F1}°F" : $"Результат: {resultTemp:F1}°C");
            else
                Console.WriteLine("Невірний вибір конвертації");
        }
        else
        {
            Console.WriteLine("Некоректне значення температури");
        }

        // TASK 7
        Console.Write("\nЗавдання 7. Введіть два числа (межі діапазону): ");
        string[] range = Console.ReadLine().Split();
        if (range.Length == 2 && int.TryParse(range[0], out int start) && int.TryParse(range[1], out int end))
        {
            if (start > end) (start, end) = (end, start);

            Console.Write("Парні числа: ");
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0) Console.Write(i + " ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Некоректні межі діапазону");
        }

        // TASK 8 
        Console.Write("\nЗавдання 8. Введіть число: ");
        if (long.TryParse(Console.ReadLine(), out long numArm))
        {
            long original = numArm;
            int digitsCount = numArm.ToString().Length;
            long sum = 0;

            while (numArm > 0)
            {
                long digit = numArm % 10;
                sum += (long)Math.Pow(digit, digitsCount);
                numArm /= 10;
            }

            Console.WriteLine(original == sum
                ? "Це число Армстронга"
                : "Це НЕ число Армстронга");
        }
        else
        {
            Console.WriteLine("Некоректне число");
        }

        // TASK 9 
        Console.Write("\nЗавдання 9. Введіть число: ");
        if (long.TryParse(Console.ReadLine(), out long perfectNum) && perfectNum > 0)
        {
            long sumDivisors = 1;

            for (long i = 2; i * i <= perfectNum; i++)
            {
                if (perfectNum % i == 0)
                {
                    sumDivisors += i;
                    if (i != perfectNum / i)
                        sumDivisors += perfectNum / i;
                }
            }

            Console.WriteLine(sumDivisors == perfectNum
                ? "Це досконале число"
                : "Це НЕ досконале число");
        }
        else
        {
            Console.WriteLine("Введіть додатнє число");
        }
    }
}