//using System;
//using System.Linq;

//namespace SimpleLinqTasks
//{
//    class Library
//    {
//        static void Main(string[] args)
//        {
//            // ────────────── Заdanka 1 ──────────────
//            Firm[] firms = new Firm[]
//            {
//                new Firm("FoodMaster", new DateTime(2018, 5, 12), "Food", "John White", 245, "London"),
//                new Firm("TechBit", new DateTime(2022, 3, 8), "IT", "Anna Black", 78, "Berlin"),
//                new Firm("MarketPro", new DateTime(2020, 11, 30), "Marketing", "Sarah White", 320, "Paris"),
//                new Firm("WhiteCloud Food", new DateTime(2023, 1, 15), "Food", "Mike Black", 45, "London"),
//                new Firm("GreenIT", new DateTime(2019, 7, 22), "IT", "Emma Green", 180, "Kyiv"),
//                new Firm("AdVibe", new DateTime(2021, 9, 5), "Marketing", "Tom White", 95, "London")
//            };

//            Console.WriteLine("=== Всі фірми ===");
//            firms.ToList().ForEach(f => Console.WriteLine(f));

//            Console.WriteLine("\n=== Містять 'Food' у назві ===");
//            firms.Where(f => f.Name.Contains("Food"))
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Маркетинг ===");
//            firms.Where(f => f.Profile == "Marketing")
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Маркетинг або IT ===");
//            firms.Where(f => f.Profile == "Marketing" || f.Profile == "IT")
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Більше 100 співробітників ===");
//            firms.Where(f => f.Employees > 100)
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Від 100 до 300 співробітників ===");
//            firms.Where(f => f.Employees >= 100 && f.Employees <= 300)
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Лондон ===");
//            firms.Where(f => f.Address == "London")
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Директор з прізвищем White ===");
//            firms.Where(f => f.DirectorName.EndsWith("White"))
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Засновані більше 2 років тому ===");
//            firms.Where(f => (DateTime.Now - f.FoundationDate).TotalDays > 730)
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Від дня заснування минуло рівно 123 дні ===");
//            firms.Where(f => Math.Abs((DateTime.Now - f.FoundationDate).Days) == 123)
//                 .ToList().ForEach(Console.WriteLine);

//            Console.WriteLine("\n=== Прізвище Black і в назві є White ===");
//            firms.Where(f => f.DirectorName.EndsWith("Black") && f.Name.Contains("White"))
//                 .ToList().ForEach(Console.WriteLine);


//            // ────────────── Заdanka 2 ──────────────
//            Phone[] phones = new Phone[]
//            {
//                new Phone("Galaxy S23", "Samsung", 899, new DateTime(2023, 2, 1)),
//                new Phone("iPhone 14", "Apple", 1099, new DateTime(2022, 9, 16)),
//                new Phone("Pixel 7", "Google", 599, new DateTime(2022, 10, 6)),
//                new Phone("Redmi Note 12", "Xiaomi", 249, new DateTime(2023, 3, 30)),
//                new Phone("iPhone 15 Pro", "Apple", 1299, new DateTime(2023, 9, 22)),
//                new Phone("Galaxy A54", "Samsung", 399, new DateTime(2023, 3, 15))
//            };

//            Console.WriteLine("\n\n=== Телефони ===");
//            Console.WriteLine($"Всього телефонів: {phones.Count()}");
//            Console.WriteLine($"Дорожче 100:     {phones.Count(p => p.Price > 100)}");
//            Console.WriteLine($"Ціна 400–700:    {phones.Count(p => p.Price >= 400 && p.Price <= 700)}");
//            Console.WriteLine($"Samsung:         {phones.Count(p => p.Manufacturer == "Samsung")}");
//            Console.WriteLine($"Найдешевший:     {phones.Min(p => p.Price)}$  ({phones.OrderBy(p => p.Price).First().Name})");
//            Console.WriteLine($"Найдорожчий:     {phones.Max(p => p.Price)}$  ({phones.OrderByDescending(p => p.Price).First().Name})");
//            Console.WriteLine($"Найстаріший:     {phones.OrderBy(p => p.ReleaseDate).First().Name}  ({phones.Min(p => p.ReleaseDate):d})");
//            Console.WriteLine($"Найновіший:      {phones.OrderByDescending(p => p.ReleaseDate).First().Name}  ({phones.Max(p => p.ReleaseDate):d})");
//            Console.WriteLine($"Середня ціна:    {phones.Average(p => p.Price):0.##}$");
//        }
//    }

//    class Firm
//    {
//        public string Name { get; }
//        public DateTime FoundationDate { get; }
//        public string Profile { get; }
//        public string DirectorName { get; }
//        public int Employees { get; }
//        public string Address { get; }

//        public Firm(string name, DateTime date, string profile, string director, int employees, string address)
//        {
//            Name = name;
//            FoundationDate = date;
//            Profile = profile;
//            DirectorName = director;
//            Employees = employees;
//            Address = address;
//        }

//        public override string ToString()
//        {
//            return $"{Name,-18} | {FoundationDate:yyyy-MM-dd} | {Profile,-12} | {DirectorName,-12} | {Employees,4} | {Address}";
//        }
//    }

//    class Phone
//    {
//        public string Name { get; }
//        public string Manufacturer { get; }
//        public decimal Price { get; }
//        public DateTime ReleaseDate { get; }

//        public Phone(string name, string manufacturer, decimal price, DateTime release)
//        {
//            Name = name;
//            Manufacturer = manufacturer;
//            Price = price;
//            ReleaseDate = release;
//        }

//        public override string ToString()
//        {
//            return $"{Name,-18} | {Manufacturer,-10} | {Price,5}$ | {ReleaseDate:yyyy-MM-dd}";
//        }
//    }
//}