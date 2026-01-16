using System;
using System.Timers;
using System.Windows.Forms;

namespace TamagotchiSimple
{
    class Program
    {
        private static readonly string[] Requests =
        {
            "Я хочу їсти! Погодувати?",
            "Погуляй зі мною, нудно!",
            "Хочу спати... Уклади мене?",
            "Мені погано... Полікуй мене :(",
            "Давай пограємо! Пограти?"
        };

        private static string currentRequest = "";
        private static string previousRequest = "";
        private static int ignoreCount = 0;
        private static bool isSick = false;
        private static bool isAlive = true;

        private static Timer timer;

        static void Main(string[] args)
        {
            Console.Title = "Тамагочі - Артур";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Налаштування таймера (кожні 12 секунд випадкове прохання)
            timer = new Timer(12000);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();

            Console.WriteLine("Тамагочі народився!");
            DrawTamagotchi();

            while (isAlive)
            {
                Console.WriteLine("\nНатисни Enter, щоб відповісти на прохання...");
                Console.ReadLine();

                if (!isAlive) break;

                ShowRequest();
            }

            Console.WriteLine("\n   ...Тамагочі помер :(");
            DrawDeadTamagotchi();
            Console.ReadKey();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!isAlive) return;

            // Випадкове прохання, але не те саме, що попереднє
            string newRequest;
            do
            {
                newRequest = Requests[new Random().Next(Requests.Length)];
            } while (newRequest == previousRequest);

            currentRequest = newRequest;
            previousRequest = newRequest;

            // Якщо вже хворіє — завжди просить лікувати
            if (isSick)
            {
                currentRequest = Requests[3]; // "Мені погано... Полікуй мене :("
            }

            Console.Clear();
            Console.WriteLine("Прохання від тамагочі:");
            Console.WriteLine("──────────────────────────");
            Console.WriteLine($"   {currentRequest}");
            Console.WriteLine("──────────────────────────");
            DrawTamagotchi();
        private static void ShowRequest()
        {
            if (string.IsNullOrEmpty(currentRequest))
            {
                Console.WriteLine("Поки що тихо...");
                return;
            }

            Console.Clear();
            Console.WriteLine("Поточне прохання:");
            Console.WriteLine($"   {currentRequest}");
            DrawTamagotchi();
        }

        private static void ProcessAnswer(bool satisfied)
        {
            if (satisfied)
            {
                ignoreCount = 0;
                isSick = false;
                Console.WriteLine("\nДякую! Я щасливий! ♥");
            }
            else
            {
                ignoreCount++;

                if (ignoreCount >= 3 && !isSick)
                {
                    isSick = true;
                    Console.WriteLine("\nЯ захворів... :(");
                    currentRequest = "Мені погано... Полікуй мене :(";
                }
                else if (isSick && !satisfied)
                {
                    isAlive = false;
                    Console.WriteLine("\nЯ більше не витримав... Прощавай...");
                }
                else
                {
                    Console.WriteLine("\nТи мене проігнорував... Це вже " + ignoreCount + "/3");
                }
            }

            if (isAlive)
            {
                DrawTamagotchi();
            }
        }

        private static void DrawTamagotchi()
        {
            Console.WriteLine(@"
              /\_/\  
             ( o.o ) 
              > ^ < 
            /       \
           /  TAMAGO \
          /    CHI    \
         ----------------
            ");

            if (isSick)
            {
                Console.WriteLine("     😷  ХВОРИЙ!   ");
            }
            else if (ignoreCount > 0)
            {
                Console.WriteLine($"     Грустно... ({ignoreCount}/3)");
            }
            else
            {
                Console.WriteLine("     Щасливий! ♥ ");
            }
        }

        private static void DrawDeadTamagotchi()
        {
            Console.WriteLine(@"
               x.x   
              (   )  
               /|\   
              / | \  
             RIP TAMAGOCHI
            ");
        }
    }
}