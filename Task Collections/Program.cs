using System;
using System.IO;
internal class Program
{
    private static void Main(string[] args)
    {
        int n;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Задания 6-10\n");
            Console.WriteLine("1. Заменить первое вхождение списка");
            Console.WriteLine("2. Отсортировать элементы по возрастанию");
            Console.WriteLine("3. Задача на компьютерные игры");
            Console.WriteLine("4. Вывод файла в алфавитном порядке");
            Console.WriteLine("5. Задача на олимпиаду по информатике");
            Console.WriteLine("0. Выход\n");
            Console.WriteLine("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();

                    List<string> L = ListClass.InputList("Введите количество элементов в списке");

                    Console.WriteLine("\nВаш список:");
                    ListClass.PrintList(L);

                    List<string> L1 = ListClass.InputList("\nВведите количество элементов в списке, который необходимо заменить: ");

                    Console.WriteLine("\nВаш список:");
                    ListClass.PrintList(L1);

                    List<string> L2 = ListClass.InputList("\nВведите количество элементов в подсписке, на который необходимо заменить: ");

                    Console.WriteLine("\nИсходный список:");
                    ListClass.PrintList(L);

                    Console.WriteLine("\nПодсписок, который необходимо заменить:");
                    ListClass.PrintList(L1);

                    Console.WriteLine("\nПодсписок, на который необходимо заменить:");
                    ListClass.PrintList(L2);

                    bool result = ListClass.ReplaceFirst(L, L1, L2);
                    Console.WriteLine($"\nРезультат: {result}");
                    Console.WriteLine($"Список после замены: ");
                    ListClass.PrintList(L);

                    Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                    Console.ReadKey(true);
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Сортировка по возрастанию");
                    LinkedList<int> numbers = ListClass.InputLinkedList();

                    Console.WriteLine("\nИсходный список: " + string.Join(", ", numbers));
                    ListClass.BubbleSort(numbers);
                    Console.WriteLine("Отсортированный: " + string.Join(", ", numbers));

                    Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                    Console.ReadKey(true);
                    break;

                case "3":
                    Console.Clear();
                    ListClass.PrintResult();
                    Console.WriteLine("\nНажмите любую клавишу для выхода...");
                    Console.ReadKey(true);
                    break;

                case "4":
                    Console.Clear();
                    string filePath = Path.Combine(Environment.CurrentDirectory, "test.txt");
                    ListClass.PrintResult(filePath);

                    Console.WriteLine("\nНажмите любую клавишу для выхода...");
                    Console.ReadKey(true);
                    break;

                case "5":
                    string filePath2 = Path.Combine(Environment.CurrentDirectory, "participants.txt");

                    Console.WriteLine("Результаты:");
                    ListClass.PrintResults(filePath2);
                    Console.WriteLine();

                    Console.WriteLine("\nСодержимое файла:");
                    if (File.Exists(filePath2))
                    {
                        string[] lines = File.ReadAllLines(filePath2, System.Text.Encoding.UTF8);
                        foreach (string line in lines)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    Console.ReadKey(true);
                    break;

                case "0":
                    Console.WriteLine("Выход из программы...");
                    return;

                default:
                    Console.WriteLine("Неверный ввод. Пожалуйста, выберите пункт от 0 до 4.");
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}