using System;
using System.Collections.Generic;

namespace BookLibrarySimple
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, string genre, int year)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Title} | {Author} | {Genre} | {Year}";
        }
    }

    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": RemoveBook(); break;
                    case "3": EditBook(); break;
                    case "4": SearchBooks(); break;
                    case "5": InsertAtBeginning(); break;
                    case "6": InsertAtEnd(); break;
                    case "7": InsertAtPosition(); break;
                    case "8": RemoveFirst(); break;
                    case "9": RemoveLast(); break;
                    case "10": RemoveAtPosition(); break;
                    case "0": Console.WriteLine("Бувай!"); return;
                    default: Console.WriteLine("Нема такого пункту..."); break;
                }

                Console.WriteLine("\nНатисни будь-яку клавішу...");
                Console.ReadKey();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== БІБЛІОТЕКА (дуже проста) ===");
            Console.WriteLine("1. Додати книгу в кінець");
            Console.WriteLine("2. Видалити книгу (за номером)");
            Console.WriteLine("3. Змінити книгу");
            Console.WriteLine("4. Пошук книг");
            Console.WriteLine("5. Додати на початок списку");
            Console.WriteLine("6. Додати в кінець списку (те саме що 1)");
            Console.WriteLine("7. Додати в певну позицію");
            Console.WriteLine("8. Видалити з початку");
            Console.WriteLine("9. Видалити з кінця");
            Console.WriteLine("10. Видалити за позицією");
            Console.WriteLine("0. Вийти");
            Console.Write("→ Обери: ");
        }

        static void ShowAllBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Книжок ще немає :(");
                return;
            }

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {books[i]}");
            }
        }

        static void AddBook()
        {
            Console.Write("Назва: ");
            string title = Console.ReadLine();
            Console.Write("Автор: ");
            string author = Console.ReadLine();
            Console.Write("Жанр: ");
            string genre = Console.ReadLine();
            Console.Write("Рік: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Рік кривий, книга не додана");
                return;
            }

            books.Add(new Book(title, author, genre, year));
            Console.WriteLine("Книжку додано!");
        }

        static void RemoveBook()
        {
            ShowAllBooks();
            Console.Write("Номер книги для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int num) && num > 0 && num <= books.Count)
            {
                books.RemoveAt(num - 1);
                Console.WriteLine("Видалено!");
            }
            else
            {
                Console.WriteLine("Нема такого номера");
            }
        }

        static void EditBook()
        {
            ShowAllBooks();
            Console.Write("Номер книги для редагування: ");
            if (int.TryParse(Console.ReadLine(), out int num) && num > 0 && num <= books.Count)
            {
                var book = books[num - 1];
                Console.WriteLine("Залиш пустим, якщо не хочеш змінювати");

                Console.Write($"Назва ({book.Title}): ");
                string t = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(t)) book.Title = t;

                Console.Write($"Автор ({book.Author}): ");
                string a = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(a)) book.Author = a;

                Console.Write($"Жанр ({book.Genre}): ");
                string g = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(g)) book.Genre = g;

                Console.Write($"Рік ({book.Year}): ");
                string y = Console.ReadLine();
                if (int.TryParse(y, out int year)) book.Year = year;

                Console.WriteLine("Змінено!");
            }
            else
            {
                Console.WriteLine("Нема такого номера");
            }
        }

        static void SearchBooks()
        {
            Console.Write("Шукати за (введи частину назви/автора/жанру): ");
            string search = Console.ReadLine().ToLower();

            bool found = false;
            for (int i = 0; i < books.Count; i++)
            {
                var b = books[i];
                if (b.Title.ToLower().Contains(search) ||
                    b.Author.ToLower().Contains(search) ||
                    b.Genre.ToLower().Contains(search))
                {
                    Console.WriteLine($"{i + 1}. {b}");
                    found = true;
                }
            }

            if (!found) Console.WriteLine("Нічого не знайдено");
        }

        static void InsertAtBeginning()
        {
            Console.WriteLine("Додаємо на початок");
            AddBook(); 
            var newBook = books[books.Count - 1];
            books.RemoveAt(books.Count - 1);
            books.Insert(0, newBook);
            Console.WriteLine("Додано на початок!");
        }

        static void InsertAtEnd()
        {
            AddBook(); 
        }

        static void InsertAtPosition()
        {
            ShowAllBooks();
            Console.Write("На яку позицію вставити (1-" + (books.Count + 1) + "): ");
            if (int.TryParse(Console.ReadLine(), out int pos) && pos >= 1 && pos <= books.Count + 1)
            {
                AddBook();
                var newBook = books[books.Count - 1];
                books.RemoveAt(books.Count - 1);
                books.Insert(pos - 1, newBook);
                Console.WriteLine("Вставлено!");
            }
            else
            {
                Console.WriteLine("Позиція крива");
            }
        }

        static void RemoveFirst()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Нема що видаляти");
                return;
            }
            books.RemoveAt(0);
            Console.WriteLine("Першу книгу видалено");
        }

        static void RemoveLast()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Нема що видаляти");
                return;
            }
            books.RemoveAt(books.Count - 1);
            Console.WriteLine("Останню книгу видалено");
        }

        static void RemoveAtPosition()
        {
            ShowAllBooks();
            Console.Write("Номер для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int num) && num > 0 && num <= books.Count)
            {
                books.RemoveAt(num - 1);
                Console.WriteLine("Видалено з позиції!");
            }
            else
            {
                Console.WriteLine("Нема такого номера");
            }
        }
    }
}