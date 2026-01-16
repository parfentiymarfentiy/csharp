using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleLinqOrders
{
    class Lunacy
    {
        static void Main(string[] args)
        {
            // ────────────── Тестовые данные ──────────────

            List<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Олег", City = "Київ" },
                new Customer { Id = 2, Name = "Анна", City = "Львів" },
                new Customer { Id = 3, Name = "Максим", City = "Одеса" },
                new Customer { Id = 4, Name = "Юлія", City = "Харків" },
                new Customer { Id = 5, Name = "Ігор", City = "Дніпро" }
            };

            List<Order> orders = new List<Order>
            {
                new Order { Id = 101, CustomerId = 1, OrderDate = new DateTime(2025, 10,  5), Total =  450m },
                new Order { Id = 102, CustomerId = 1, OrderDate = new DateTime(2025, 11, 12), Total = 1280m },
                new Order { Id = 103, CustomerId = 1, OrderDate = new DateTime(2025, 12,  3), Total =  720m },

                new Order { Id = 201, CustomerId = 2, OrderDate = new DateTime(2025,  9, 15), Total =  890m },
                new Order { Id = 202, CustomerId = 2, OrderDate = new DateTime(2025, 11, 28), Total =  340m },

                new Order { Id = 301, CustomerId = 3, OrderDate = new DateTime(2025, 10, 22), Total = 2150m },
                new Order { Id = 302, CustomerId = 3, OrderDate = new DateTime(2025, 12, 14), Total =  680m },
                new Order { Id = 303, CustomerId = 3, OrderDate = new DateTime(2025, 12, 25), Total =  420m },

                new Order { Id = 401, CustomerId = 4, OrderDate = new DateTime(2025, 11,  8), Total =  950m },

                new Order { Id = 501, CustomerId = 5, OrderDate = new DateTime(2025, 10, 10), Total = 1640m },
                new Order { Id = 502, CustomerId = 5, OrderDate = new DateTime(2025, 12, 19), Total =  570m },
            };


            // 1.
            Console.WriteLine("1. Загальна сума по кожному клієнту:");
            var totalByCustomer = orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalSum = g.Sum(o => o.Total)
                });

            foreach (var item in totalByCustomer)
            {
                string name = customers.First(c => c.Id == item.CustomerId).Name;
                Console.WriteLine($"{name,-8} : {item.TotalSum,8:N0} грн");
            }


            // 2.
            Console.WriteLine("\n2. Найбільше витратив:");
            var topSpender = totalByCustomer
                .OrderByDescending(x => x.TotalSum)
                .First();

            string topName = customers.First(c => c.Id == topSpender.CustomerId).Name;
            Console.WriteLine($"{topName} — {topSpender.TotalSum:N0} грн");


            // 3.
            Console.WriteLine("\n3. Статистика по клієнтах:");
            var customerStats = orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    Customer = customers.First(c => c.Id == g.Key),
                    OrdersCount = g.Count(),
                    AverageCheck = g.Average(o => o.Total)
                })
                .OrderByDescending(x => x.OrdersCount);

            foreach (var stat in customerStats)
            {
                Console.WriteLine($"{stat.Customer.Name,-8} | замовлень: {stat.OrdersCount,2} | середній чек: {stat.AverageCheck,8:N0} грн");
            }


            // 4. 
            Console.WriteLine("\n4. Найдорожче замовлення кожного клієнта:");
            var maxPerCustomer = orders
                .GroupBy(o => o.CustomerId)
                .Select(g => g.OrderByDescending(o => o.Total).First());

            foreach (var order in maxPerCustomer)
            {
                string name = customers.First(c => c.Id == order.CustomerId).Name;
                Console.WriteLine($"{name,-8} → {order.Total,8:N0} грн  ({order.OrderDate:dd.MM.yyyy})");
            }


            // 5. 
            Console.WriteLine("\n5. Клієнти з сумою > 1000 грн та ≥ 2 замовлення:");
            var goodCustomers = totalByCustomer
                .Where(x => x.TotalSum > 1000)
                .Join(customerStats,
                    t => t.CustomerId,
                    s => s.Customer.Id,
                    (t, s) => new { s.Customer, s.OrdersCount, t.TotalSum })
                .Where(x => x.OrdersCount >= 2);

            foreach (var c in goodCustomers)
            {
                Console.WriteLine($"{c.Customer.Name,-8} — {c.TotalSum,8:N0} грн ({c.OrdersCount} замовлення)");
            }


            // 6. 
            Console.WriteLine("\n6. Замовлення за місяцями:");
            var byMonth = orders
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    OrdersCount = g.Count(),
                    TotalSum = g.Sum(o => o.Total)
                })
                .OrderBy(x => x.Month);

            foreach (var m in byMonth)
            {
                Console.WriteLine($"{m.Month} | {m.OrdersCount,3} замовл. | {m.TotalSum,9:N0} грн");
            }


            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }


    // Модели
    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}