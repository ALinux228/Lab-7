using System;
using System.IO;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int choice;

        do
        {
            Console.Clear();
            Console.WriteLine("Тестирование\n");
            Console.WriteLine("1. Сумма квадратов элементов");
            Console.WriteLine("2. Произведение элементов");
            Console.WriteLine("3. Копирование строк заданной длины");
            Console.WriteLine("4. Произведение нечётных отрицательных компонент (бинарный файл)");
            Console.WriteLine("5. Задача о багаже");
            Console.WriteLine("0. Выход\n");
            Console.Write("Выберите действие: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Ошибка: введите число!");
                continue;
            }

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Сумма квадратов элементов\n");
                    FileClass.RandomFileOne(10, -10, 10);
                    Console.WriteLine("Содержимое файла:");
                    foreach (string line in File.ReadLines(FileClass.FullPath))
                    {
                        Console.WriteLine($"  {line}");
                    }
                    long sumSquares = FileClass.SumOfSquares();
                    Console.WriteLine($"\nСумма квадратов элементов: {sumSquares}");
                    break;

                case 2:
                    Console.WriteLine("Произведение элементов\n");
                    FileClass.RandomFileSeveral(5, 3, -5, 5);
                    Console.WriteLine("Содержимое файла:");
                    foreach (string line in File.ReadLines(FileClass.FullPath))
                    {
                        Console.WriteLine($"  {line}");
                    }
                    long product = FileClass.ProductElements();
                    Console.WriteLine($"\nПроизведение всех элементов: {product}");
                    break;

                case 3:
                    Console.WriteLine("Копирование строк заданной длины\n");
                    string sourceFile = FileClass.FullPath;
                    string destFile = Path.Combine(FileClass.FilePath, "filtered.txt");

                    using (StreamWriter writer = new StreamWriter(sourceFile))
                    {
                        writer.WriteLine("Короткая строка");
                        writer.WriteLine("Эта строка немного длиннее");
                        writer.WriteLine("Средняя");
                        writer.WriteLine("Очень длинная строка для тестирования");
                        writer.WriteLine("Hello");
                        writer.WriteLine("World!");
                    }

                    Console.WriteLine("Исходный файл:");
                    foreach (string line in File.ReadLines(sourceFile))
                    {
                        Console.WriteLine($"  [{line.Length}] {line}");
                    }

                    Console.Write("\nВведите длину строки для копирования: ");
                    if (int.TryParse(Console.ReadLine(), out int targetLength))
                    {
                        FileClass.CopyLinesOfLength(destFile, targetLength);
                        Console.WriteLine($"\nСтроки длиной {targetLength} скопированы в {destFile}");

                        if (File.Exists(destFile))
                        {
                            Console.WriteLine("Скопированные строки:");
                            foreach (string line in File.ReadLines(destFile))
                            {
                                Console.WriteLine($"  {line}");
                            }
                        }
                    }
                    break;

                case 4:
                    Console.WriteLine("Произведение нечётных отрицательных компонент\n");
                    FileClass.BinaryFileRandom(15, -20, 20);
                    Console.WriteLine("Содержимое бинарного файла:");
                    using (BinaryReader reader = new BinaryReader(File.Open(FileClass.FullPath, FileMode.Open)))
                    {
                        int index = 1;
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            int number = reader.ReadInt32();
                            Console.WriteLine($"  {index++}. {number}");
                        }
                    }
                    long negativeProduct = FileClass.BinaryProductNegative();
                    Console.WriteLine($"\nПроизведение нечётных отрицательных компонент: {negativeProduct}");
                    break;

                case 5:
                    Console.WriteLine("Задача о багаже\n");
                    FileClass.CreateSampleFile();
                    FileClass.PrintAllPassengers();

                    Console.WriteLine("\nРезультаты анализа");
                    int moreThanTwo = FileClass.CountMoreThanTwo();
                    Console.WriteLine($"Число пассажиров, имеющих более двух единиц багажа: {moreThanTwo}");

                    int aboveAverage = FileClass.CountMorethanAverage();
                    Console.WriteLine($"Число пассажиров, количество единиц багажа которых превосходит среднее: {aboveAverage}");
                    break;

                case 0:
                    Console.WriteLine("Выход из программы...");
                    break;

                default:
                    Console.WriteLine("Неверный выбор! Попробуйте снова.");
                    break;
            }

            if (choice != 0)
            {
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }

        } while (choice != 0);
    }
}
