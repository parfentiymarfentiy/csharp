using System;

public class Film : IDisposable
{
    public string Title { get; private set; }
    public string Studio { get; private set; }
    public string Genre { get; private set; }
    public int DurationMinutes { get; private set; }
    public int ReleaseYear { get; private set; }

    private bool disposed = false;

    public Film(string title, string studio, string genre, int durationMinutes, int releaseYear)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Studio = studio ?? throw new ArgumentNullException(nameof(studio));
        Genre = genre ?? throw new ArgumentNullException(nameof(genre));
        DurationMinutes = durationMinutes > 0 ? durationMinutes : throw new ArgumentException("Тривалість має бути більше 0");
        ReleaseYear = releaseYear >= 1895 && releaseYear <= DateTime.Now.Year + 5
            ? releaseYear
            : throw new ArgumentException("Некоректний рік випуску");
    }

    public override string ToString()
    {
        return $"«{Title}» ({ReleaseYear})\n" +
               $"Студія: {Studio}\n" +
               $"Жанр: {Genre}\n" +
               $"Тривалість: {DurationMinutes} хв";
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Console.WriteLine($"[Dispose] Фільм '{Title}' ({ReleaseYear}) успішно очищено (managed)");
            }

            Console.WriteLine($"[Dispose] Фільм '{Title}' повністю очищено");
            disposed = true;
        }
    }

    ~Film()
    {
        Console.WriteLine($"[Destructor] Фільм '{Title}' ({ReleaseYear}) очищається через деструктор!");
        Dispose(false);
    }
}

class Velocity
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Тест 1: звичайне використання з using ===\n");

        using (var film1 = new Film("Дюна: Частина друга", "Warner Bros.", "Наукова фантастика", 166, 2024))
        {
            Console.WriteLine(film1);
            Console.WriteLine();
        } 

        Console.WriteLine("\n=== Тест 2: без using, ручне очищення ===\n");

        var film2 = new Film("Інтерстеллар", "Paramount", "Фантастика/Драма", 169, 2014);
        Console.WriteLine(film2);
        film2.Dispose();  

        Console.WriteLine("\n=== Тест 3: забули викликати Dispose → спрацює деструктор ===\n");

        var film3 = new Film("Початок", "Warner Bros.", "Трилер/Фантастика", 148, 2010);
        Console.WriteLine(film3);
        Console.WriteLine("\nНатисніть Enter для завершення...");
        Console.ReadLine();
    }
}