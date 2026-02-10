//Task 1

//using System;
//using System.Collections.Generic;

//public class AppConfig
//{

//    private static AppConfig _instance;
//    private Dictionary<string, string> _settings;
//    private AppConfig()
//    {
//        _settings = new Dictionary<string, string>();
//    }
//    public static AppConfig Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                _instance = new AppConfig();
//            }
//            return _instance;
//        }
//    }

//    public void SetSetting(string key, string value)
//    {
//        if (_settings.ContainsKey(key))
//        {
//            _settings[key] = value;
//        }
//        else
//        {
//            _settings.Add(key, value);
//        }
//    }
//    public string GetSetting(string key)
//    {
//        if (_settings.ContainsKey(key))
//        {
//            return _settings[key];
//        }
//        return null; 
//    }
//}

//class Program1
//{
//    static void Main1()
//    {
//        AppConfig config = AppConfig.Instance;
//        config.SetSetting("Theme", "Dark");
//        config.SetSetting("Language", "English");

//        Console.WriteLine(config.GetSetting("Theme")); 
//        Console.WriteLine(config.GetSetting("Language")); 
//    }
//}

// TASK 2

//using System;

//public interface IShape
//{
//    void Draw();
//}

//public class Circle : IShape
//{
//    public void Draw()
//    {
//        Console.WriteLine("Drawing a circle");
//    }
//}

//public class Rectangle : IShape
//{
//    public void Draw()
//    {
//        Console.WriteLine("Drawing a rectangle");
//    }
//}

//public abstract class ShapeFactory
//{
//    public abstract IShape CreateShape();
//}
//public class CircleFactory : ShapeFactory
//{
//    public override IShape CreateShape()
//    {
//        return new Circle();
//    }
//}

//public class RectangleFactory : ShapeFactory
//{
//    public override IShape CreateShape()
//    {
//        return new Rectangle();
//    }
//}
//class Program2
//{
//    static void Main2()
//    {
//        ShapeFactory circleFactory = new CircleFactory();
//        IShape circle = circleFactory.CreateShape();
//        circle.Draw(); 

//        ShapeFactory rectangleFactory = new RectangleFactory();
//        IShape rectangle = rectangleFactory.CreateShape();
//        rectangle.Draw(); 
//    }
//}
