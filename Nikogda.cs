using System;
using System.Collections.Generic;
using System.Text.Json;

namespace LabWork
{
    // 1)
    interface IPlugin
    {
        string Process(string text);
    }

    class UpperPlugin : IPlugin
    {
        public string Process(string text)
        {
            return text.ToUpper();
        }
    }

    class BadWordsPlugin : IPlugin
    {
        public string Process(string text)
        {
            text = text.Replace("pizza", "🤬");
            text = text.Replace("banana", "🤬");
            text = text.Replace("orange", "🤬");
            return text;
        }
    }

    class ReversePlugin : IPlugin
    {
        public string Process(string text)
        {
            char[] arr = text.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }

    class RandomColorPlugin : IPlugin
    {
        public string Process(string text)
        {
            string[] colors = { "red", "blue", "green", "yellow", "purple" };
            Random r = new Random();
            string color = colors[r.Next(colors.Length)];
            return $"[{color}]{text}[/{color}]";
        }
    }
    class PluginProcessor
    {
        private List<IPlugin> plugins = new List<IPlugin>();

        public void AddPlugin(IPlugin p)
        {
            plugins.Add(p);
        }

        public string Execute(string input)
        {
            string result = input;
            foreach (var plugin in plugins)
            {
                result = plugin.Process(result);
            }
            return result;
        }
    }

    // 2) 
    interface ICommand
    {
        void Execute();
        void Undo();
    }

    class MyList
    {
        private List<string> items = new List<string>();

        public void Add(string item) => items.Add(item);
        public void Remove(string item) => items.Remove(item);
        public void Show()
        {
            Console.WriteLine("Список: " + string.Join(", ", items));
        }
    }

    class AddCommand : ICommand
    {
        private MyList list;
        private string item;

        public AddCommand(MyList l, string value)
        {
            list = l;
            item = value;
        }

        public void Execute()
        {
            list.Add(item);
        }

        public void Undo()
        {
            list.Remove(item);
        }
    }

    // 3) 
    interface ICache
    {
        string Get(string key);
        void Set(string key, string value);
        bool Exists(string key);
    }

    class SimpleCache : ICache
    {
        private Dictionary<string, string> cache = new Dictionary<string, string>();

        public string Get(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key];
            return null;
        }

        public void Set(string key, string value)
        {
            cache[key] = value;
        }

        public bool Exists(string key)
        {
            return cache.ContainsKey(key);
        }
    }

    class CalculatorWithCache
    {
        private ICache cache;

        public CalculatorWithCache(ICache c)
        {
            cache = c;
        }

        public int Add(int a, int b)
        {
            string key = $"add_{a}_{b}";

            if (cache.Exists(key))
            {
                return int.Parse(cache.Get(key));
            }

            int result = a + b;
            cache.Set(key, result.ToString());
            return result;
        }
    }

    // 4)
    interface IJson
    {
        string ToJson();
    }

    class Student : IJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public string ToJson()
        {
            return "{" +
                $"\"Id\":\"{Id}\"," +
                $"\"Name\":\"{Name}\"," +
                $"\"Surname\":\"{Surname}\"," +
                $"\"BirthDate\":\"{BirthDate:dd-MM-yyyy}\"" +
                "}";
        }
    }

    class StudentJournal : List<Student>, IJson
    {
        public string ToJson()
        {
            string json = "[\n";
            for (int i = 0; i < Count; i++)
            {
                json += this[i].ToJson();
                if (i < Count - 1)
                    json += ",\n";
            }
            json += "\n]";
            return json;
        }
    }

}