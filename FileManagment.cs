using System;
using System.IO;
using System.Linq;

class Dox
{
    static string currentPath = "";

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Простий файловий менеджер\n");

        while (true)
        {
            currentPath = ReadValidDirectory("Введіть початкову директорію:");
            ShowDirectoryContent();
            break;
        }

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("  1  - Створити файл");
            Console.WriteLine("  2  - Видалити файл");
            Console.WriteLine("  3  - Створити папку");
            Console.WriteLine("  4  - Видалити папку");
            Console.WriteLine("  5  - Перейти в попередню директорію (..)");
            Console.WriteLine("  6  - Перейти в папку за номером");
            Console.WriteLine("  7  - Перейти в папку за повним шляхом");
            Console.WriteLine("  0  - Вийти");
            Console.Write("\nВибір → ");

            string choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1": CreateFile(); break;
                    case "2": DeleteFile(); break;
                    case "3": CreateDirectory(); break;
                    case "4": DeleteDirectory(); break;
                    case "5": GoToParentDirectory(); break;
                    case "6": GoToDirectoryByNumber(); break;
                    case "7": GoToDirectoryByPath(); break;
                    case "0":
                        Console.WriteLine("До побачення!");
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            ShowDirectoryContent();
        }
    }

    static string ReadValidDirectory(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} ");
            string path = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Шлях не може бути порожнім!");
                continue;
            }

            if (Directory.Exists(path))
                return path;

            Console.WriteLine("Така директорія не існує. Спробуйте ще раз.");
        }
    }

    static void ShowDirectoryContent()
    {
        Console.Clear();
        Console.WriteLine($"Вміст директорії: {currentPath}\n");

        try
        {
            var directories = Directory.GetDirectories(currentPath)
                .Select(d => new { Name = Path.GetFileName(d), Creation = Directory.GetCreationTime(d) })
                .OrderBy(d => d.Name)
                .ToList();

            var files = Directory.GetFiles(currentPath)
                .Select(f => new
                {
                    Name = Path.GetFileName(f),
                    Extension = Path.GetExtension(f) != "" ? Path.GetExtension(f) : "(без розширення)",
                    Creation = File.GetCreationTime(f)
                })
                .OrderBy(f => f.Name)
                .ToList();

            Console.WriteLine($"Кількість папок:  {directories.Count}");
            Console.WriteLine($"Кількість файлів: {files.Count}\n");

            Console.WriteLine("Directories:");
            if (directories.Count == 0)
                Console.WriteLine("No dirs");
            else
            {
                for (int i = 0; i < directories.Count; i++)
                {
                    Console.WriteLine($"  {i + 1,2}. {directories[i].Name,-35}  {directories[i].Creation:yyyy-MM-dd HH:mm}");
                }
            }

            Console.WriteLine("\nFiles:");
            if (files.Count == 0)
                Console.WriteLine("   No files");
            else
            {
                foreach (var file in files)
                {
                    Console.WriteLine($"     {file.Name,-35}  {file.Extension,-12}  {file.Creation:yyyy-MM-dd HH:mm}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не вдалося прочитати вміст директорії: {ex.Message}");
        }
    }

    static void CreateFile()
    {
        Console.Write("Введіть назву нового файлу (з розширенням): ");
        string fileName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("Назва не може бути порожньою!");
            return;
        }

        string fullPath = Path.Combine(currentPath, fileName);

        if (File.Exists(fullPath))
        {
            Console.WriteLine("Файл з такою назвою вже існує!");
            return;
        }

        Console.WriteLine("Введіть вміст файлу (закінчити введення: Enter + Ctrl+Z + Enter):\n");
        string content = Console.In.ReadToEnd();

        try
        {
            File.WriteAllText(fullPath, content);
            Console.WriteLine($"Файл '{fileName}' успішно створено!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при створенні файлу: {ex.Message}");
        }
    }

    static void DeleteFile()
    {
        Console.Write("Введіть назву файлу для видалення: ");
        string fileName = Console.ReadLine()?.Trim();

        string fullPath = Path.Combine(currentPath, fileName);

        if (!File.Exists(fullPath))
        {
            Console.WriteLine("Файл не знайдено!");
            return;
        }

        Console.Write($"Ви дійсно хочете видалити '{fileName}'? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "y")
        {
            Console.WriteLine("Операцію скасовано.");
            return;
        }

        try
        {
            File.Delete(fullPath);
            Console.WriteLine("Файл успішно видалено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void CreateDirectory()
    {
        Console.Write("Введіть назву нової папки: ");
        string dirName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(dirName))
        {
            Console.WriteLine("Назва не може бути порожньою!");
            return;
        }

        string fullPath = Path.Combine(currentPath, dirName);

        if (Directory.Exists(fullPath))
        {
            Console.WriteLine("Папка з такою назвою вже існує!");
            return;
        }

        try
        {
            Directory.CreateDirectory(fullPath);
            Console.WriteLine($"Папка '{dirName}' успішно створена!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void DeleteDirectory()
    {
        Console.Write("Введіть назву папки для видалення: ");
        string dirName = Console.ReadLine()?.Trim();

        string fullPath = Path.Combine(currentPath, dirName);

        if (!Directory.Exists(fullPath))
        {
            Console.WriteLine("Папку не знайдено!");
            return;
        }

        Console.Write($"Видалити папку '{dirName}' та весь її вміст? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "y")
        {
            Console.WriteLine("Операцію скасовано.");
            return;
        }

        try
        {
            Directory.Delete(fullPath, true);
            Console.WriteLine("Папку та весь вміст успішно видалено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void GoToParentDirectory()
    {
        try
        {
            string parent = Directory.GetParent(currentPath)?.FullName;
            if (parent == null)
            {
                Console.WriteLine("Ви вже в кореневій директорії диска!");
                return;
            }

            currentPath = parent;
            Console.WriteLine($"Перехід виконано. Поточна директорія: {currentPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static void GoToDirectoryByNumber()
    {
        var dirs = Directory.GetDirectories(currentPath);
        if (dirs.Length == 0)
        {
            Console.WriteLine("У поточній директорії немає папок.");
            return;
        }

        Console.Write("Введіть номер папки (1.." + dirs.Length + "): ");
        if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > dirs.Length)
        {
            Console.WriteLine("Некоректний номер!");
            return;
        }

        currentPath = dirs[number - 1];
        Console.WriteLine($"Перехід виконано: {currentPath}");
    }

    static void GoToDirectoryByPath()
    {
        Console.Write("Введіть повний або відносний шлях до папки: ");
        string newPath = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newPath))
        {
            Console.WriteLine("Шлях не може бути порожнім!");
            return;
        }

        string absolutePath;

        try
        {
            absolutePath = Path.GetFullPath(Path.Combine(currentPath, newPath));
        }
        catch
        {
            Console.WriteLine("Некоректний шлях!");
            return;
        }

        if (!Directory.Exists(absolutePath))
        {
            Console.WriteLine("Вказана директорія не існує!");
            return;
        }

        currentPath = absolutePath;
        Console.WriteLine($"Перехід виконано: {currentPath}");
    }
}