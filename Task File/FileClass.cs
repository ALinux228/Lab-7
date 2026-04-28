using System;
using System.IO;
using System.Xml.Serialization;

public class FileClass
{
    private string _filePath;
    private string _fileName;

    public FileClass()
    {
        _filePath = Environment.CurrentDirectory;
        _fileName = "file.txt";
    }

    public FileClass(string fileName)
    {
        _filePath = Environment.CurrentDirectory;
        _fileName = fileName;
    }

    public FileClass(string filePath, string fileName)
    {
        _filePath = filePath;
        _fileName = fileName;
    }

    public string FilePath
    {
        get 
        { 
            return _filePath; 
        }
        set 
        { 
            _filePath = value;
        }
    }

    public string FileName
    {
        get 
        { 
            return _fileName; 
        }
        set 
        { 
            _fileName = value; 
        }
    }


    public string FullPath
    {
        get 
        { 
            return Path.Combine(_filePath, _fileName); 
        }
    }

    // Заполнение файла случайными числами (по одному в строке)
    public void RandomFileOne(int count = 10, int min = -100, int max = 100)
    {
        Random rand = new Random();
        using (StreamWriter writer = new StreamWriter(FullPath))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(rand.Next(min, max + 1));
            }
        }
    }

    // Сумма квадратов элементов
    public long SumOfSquares()
    {
        long sum = 0;
        foreach (string line in File.ReadLines(FullPath))
        {
            if (int.TryParse(line.Trim(), out int number))
            {
                sum += (long)number * number;
            }
        }
        return sum;
    }

    // Заполнение файла случайными числами (по несколько в строке)
    public void RandomFileSeveral(int lineCount = 10, int numbersPerLine = 2, int minValue = -100, int maxValue = 100)
    {
        Random rand = new Random();
        using (StreamWriter writer = new StreamWriter(FullPath))
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < numbersPerLine; j++)
                {
                    writer.Write(rand.Next(minValue, maxValue + 1));
                    if (j < numbersPerLine - 1) writer.Write(" ");
                }
                writer.WriteLine();
            }
        }
    }

    // Вычислить произведение элементов
    public long ProductElements()
    {
        long product = 1;
        bool hasNumbers = false;

        foreach (string line in File.ReadLines(FullPath))
        {
            string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                if (int.TryParse(part, out int number))
                {
                    product *= number;
                    hasNumbers = true;
                }
            }
        }

        if (hasNumbers)
        {
            return product;
        }
        else
        {
            return 0;
        }
    }

    // Переписать в другой файл строки, имеющие заданную длину m
    public void CopyLinesOfLength(string destFile, int targetLength)
    {
        using (StreamReader reader = new StreamReader(FullPath))
        using (StreamWriter writer = new StreamWriter(destFile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length == targetLength)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }

    // Заполнение бинарного файла случайными числами
    public void BinaryFileRandom(int count = 10, int minValue = -100, int maxValue = 100)
    {
        Random rand= new Random();
        using (BinaryWriter writer = new BinaryWriter(File.Open(FullPath, FileMode.Create)))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(rand.Next(minValue, maxValue + 1));
            }
        }
    }

    // Вычислить произведение нечетных отрицательных компонент файла
    public long BinaryProductNegative()
    {
        long product = 1;
        bool hasNumbers = false;

        using (BinaryReader reader = new BinaryReader(File.Open(FullPath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                if (number < 0 && Math.Abs(number) % 2 == 1)
                {
                    product *= number;
                    hasNumbers = true;
                }
            }
        }

        if (hasNumbers)
        {
            return product;
        }
        else
        {
            return 0;
        }
    }

    [Serializable]
    public struct BaggageItem
    {
        public string _name;   // Наименование багажа
        public double _weight; // Вес багажа

        public BaggageItem(string name, double weight)
        {
            _name = name;
            _weight = weight;
        }

        public override string ToString()
        {
            return $"{_name} ({_weight} кг)";
        }
    }

    public struct PassengerBaggage
    {
        public int _passengerId;       // Идентификатор пассажира
        public BaggageItem[] _items;   // Массив единиц багажа

        public PassengerBaggage(int id, BaggageItem[] items)
        {
            _passengerId = id;
            _items = items;
        }

        // Количество единиц багажа
        public int ItemCount
        {
            get 
            { 
                if (_items != null)
                {
                    return _items.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        // Общая масса багажа
        public double TotalWeight
        {
            get
            {
                if (_items == null)
                {
                    return 0;
                }
                double sum = 0;
                foreach (var item in _items)
                {
                    sum += item._weight;
                }
                return sum;
            }
        }

        public override string ToString()
        {
            return $"Пассажир {_passengerId}: {ItemCount} единиц, общая масса {TotalWeight} кг";
        }
    }
    public void CreateSampleFile()
    {
        PassengerBaggage[] passengers = new PassengerBaggage[]
        {
                new PassengerBaggage(1, new BaggageItem[]
                {
                    new BaggageItem("Чемодан", 15.5),
                    new BaggageItem("Сумка", 5.2),
                    new BaggageItem("Рюкзак", 3.8)
                }),
                new PassengerBaggage(2, new BaggageItem[]
                {
                    new BaggageItem("Чемодан", 12.0),
                    new BaggageItem("Сумка", 4.5)
                }),
                new PassengerBaggage(3, new BaggageItem[]
                {
                    new BaggageItem("Коробка", 8.0),
                    new BaggageItem("Чемодан", 18.0),
                    new BaggageItem("Сумка", 6.0),
                    new BaggageItem("Пакет", 2.0)
                }),
                new PassengerBaggage(4, new BaggageItem[]
                {
                    new BaggageItem("Рюкзак", 4.2)
                }),
                new PassengerBaggage(5, new BaggageItem[]
                {
                    new BaggageItem("Чемодан", 20.0),
                    new BaggageItem("Коробка", 10.0),
                    new BaggageItem("Сумка", 5.0)
                }),
                new PassengerBaggage(6, new BaggageItem[]
                {
                    new BaggageItem("Сумка", 3.0),
                    new BaggageItem("Пакет", 1.5)
                })
        };

        string fullPath = Path.Combine(_filePath, _fileName);

        // Сериализация XML
        XmlSerializer serializer = new XmlSerializer(typeof(PassengerBaggage[]));
        using (FileStream fs = new FileStream(fullPath, FileMode.Create))
        {
            serializer.Serialize(fs, passengers);
        }

        Console.WriteLine($"Файл {fullPath} успешно создан с тестовыми данными"); 
        
    }

    // Десериализация XML
    public PassengerBaggage[] ReadPassengers()
    {
        string fullPath = Path.Combine(_filePath, _fileName);

        XmlSerializer serializer = new XmlSerializer(typeof(PassengerBaggage[]));
        using (FileStream fs = new FileStream(fullPath, FileMode.Open))
        {
            return (PassengerBaggage[])serializer.Deserialize(fs);
        }
    }

    // Сериализация XML
    public void WritePassengers(PassengerBaggage[] passengers)
    {
        string fullPath = Path.Combine(_filePath, _fileName);

        XmlSerializer serializer = new XmlSerializer(typeof(PassengerBaggage[]));
        using (FileStream fs = new FileStream(fullPath, FileMode.Create))
        {
            serializer.Serialize(fs, passengers);
        }
    }

    public int CountMoreThanTwo()
    {
        PassengerBaggage[] passengers = ReadPassengers();
        int count = 0;

        foreach (var passenger in passengers)
        {
            if (passenger.ItemCount > 2)
            {
                count++;
            }
        }

        return count;
    }

    public int CountMorethanAverage()
    {
        PassengerBaggage[] passengers = ReadPassengers();

        if (passengers.Length == 0)
        {
            return 0;
        }

        double sum = 0;
        for (int i = 0; i < passengers.Length; i++)
        {
            sum += passengers[i].ItemCount;
        }

        double average = sum / passengers.Length;

        Console.WriteLine($"Среднее количество единиц багажа: {average}");

        int count = 0;
        foreach (var passenger in passengers)
        {
            if (passenger.ItemCount > average)
            {
                count++;
            }
        }

        return count;
    }

    public void PrintAllPassengers()
    {
        PassengerBaggage[] passengers = ReadPassengers();

        Console.WriteLine("\nИнформация о багаже пассажиров");
        foreach (var passenger in passengers)
        {
            Console.WriteLine($"\nПассажир {passenger._passengerId}:");
            for (int i = 0; i < passenger._items.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {passenger._items[i]}");
            }
            Console.WriteLine($"  Итого: {passenger.ItemCount} единиц, " +
                $"{passenger.TotalWeight} кг");
        }
    }
}