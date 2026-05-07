using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

internal class ListClass
{
    public static LinkedList<int> InputLinkedList()
    {
        LinkedList<int> list = new LinkedList<int>();

        Console.Write("Введите длину списка: ");
        if (!(int.TryParse(Console.ReadLine(), out int count)))
        {
            Console.Write("Ошибка: введено не целое число");
            return list;
        }

        for (int i = 0; i < count; i++)
        {
            Console.Write($"Введите число {i + 1}: ");
            int number = int.Parse(Console.ReadLine());
            list.AddLast(number);
        }

        return list;
    }

    public static List<string> InputList(string msg)
    {
        Console.WriteLine(msg);

        List<string> list = new List<string>();
        if (!(int.TryParse(Console.ReadLine(), out int count)))
        {
            Console.Write("Ошибка: введено не целое число");
            return list;
        }

        for (int i = 0; i < count; i++)
        {
            Console.Write($"Введите элемент {i + 1}: ");
            string element = Console.ReadLine();
            list.Add(element);
        }

        return list;
    }
    public static void PrintList(List<string> list)
    {
        foreach (var item in list)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    }

    public static bool ReplaceFirst<T>(List<T> list, List<T> oldSubList, List<T> newSubList)
    {
        if (list == null || oldSubList == null || newSubList == null)
        {
            return false;
        }


        int index = FindFirstSublist(list, oldSubList);
        if (index == -1)
        {
            return false;
        }

        list.RemoveRange(index, oldSubList.Count);
        list.InsertRange(index, newSubList);

        return true;
    }

    private static int FindFirstSublist<T>(List<T> list, List<T> search)
    {
        for (int i = 0; i <= list.Count - search.Count; i++)
        {
            bool found = true;
            for (int j = 0; j < search.Count; j++)
            {
                if (!EqualityComparer<T>.Default.Equals(list[i + j], search[j]))
                {
                    found = false;
                    break;
                }
            }
            if (found)
            {
                return i;
            }    
        }
        return -1;
    }

    public static void BubbleSort(LinkedList<int> list)
    {
        if (list == null || list.Count <= 1)
        {
            return;
        }
            
        bool swapped;

        do
        {
            swapped = false;
            LinkedListNode<int> current = list.First;

            while (current.Next != null)
            {
                if (current.Value > current.Next.Value)
                {
                    int temp = current.Value;
                    current.Value = current.Next.Value;
                    current.Next.Value = temp;
                    swapped = true;
                }
                current = current.Next;
            }
        } while (swapped);
    }

    public static Dictionary<string, HashSet<string>> CreateStudentsDictionary()
    {
        Dictionary<string, HashSet<string>> students = 
            new Dictionary<string, HashSet<string>>();

        students.Add("Иванов", new HashSet<string> { "Minecraft", "Dota 2", "Sims 4" });
        students.Add("Петров", new HashSet<string> { "Minecraft", "Genshin Impact", "Among Us" });
        students.Add("Сидоров", new HashSet<string> { "Minecraft", "GTA V" });
        students.Add("Козлов", new HashSet<string> { "Minecraft", "The Witcher 3" });

        return students;
    }

    public static HashSet<string> GetAllGamesList()
    {
        return new HashSet<string>
        {
            "Sims 4",
            "Dota 2",
            "Genshin Impact",
            "Minecraft",
            "Among Us",
            "GTA V",
            "The Witcher 3",
            "Metro 2039"
        };
    }

    public static HashSet<string> GetGamesAllStudentsPlay(Dictionary<string, HashSet<string>> students)
    {
        if (students == null)
        {
            return new HashSet<string>();
        }

        HashSet<string> result = new HashSet<string>();

        bool first = true;
        foreach (var student in students.Values)
        {
            if (first)
            {
                foreach (var game in student)
                {
                    result.Add(game);
                }
                first = false;
            }
            else
            {
                result.IntersectWith(student);
            }
        }
        return result;
    }

    public static HashSet<string> GetGamesSomeStudentsPlay(Dictionary<string, HashSet<string>> students)
    {
        if (students == null)
        {
            return new HashSet<string>();
        }
            

        HashSet<string> result = new HashSet<string>();
        foreach (var student in students.Values)
        {
            foreach (var game in student)
            {
                result.Add(game);
            }     
        }
        return result;
    }

    public static HashSet<string> GetGamesNoOnePlay(
        HashSet<string> allGames, 
        HashSet<string> someStudentsPlay)
    {
        HashSet<string> result = new HashSet<string>();

        foreach (var game in allGames)
        {
            result.Add(game);
        }
            

        foreach (var game in someStudentsPlay)
        {
            result.Remove(game);
        }
            
        return result;
    }

    public static void PrintResult()
    {
        Dictionary<string, HashSet<string>> students = 
            CreateStudentsDictionary();

        HashSet<string> allGames = GetAllGamesList();

        ListClass.PrintStudents(students);

        HashSet<string> allPlay = GetGamesAllStudentsPlay(students);
        HashSet<string> somePlay = GetGamesSomeStudentsPlay(students);
        HashSet<string> noPlay = GetGamesNoOnePlay(allGames, somePlay);

        Console.WriteLine("Игры, в которые играют все студенты");
        PrintSet(allPlay);

        Console.WriteLine("\nИгры, в которые играют некоторые студенты");
        PrintSet(somePlay);

        Console.WriteLine("\nИгры, в которые не играет ни один студент");
        PrintSet(noPlay);
    }

    public static void PrintStudents(Dictionary<string, HashSet<string>> students)
    {
        Console.WriteLine("Список студентов и их игр:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Key}:");
            foreach (var game in student.Value)
            {
                Console.WriteLine($"  - {game}");
            }
            Console.WriteLine();
        }
    }

    private static void PrintSet(HashSet<string> set)
    {
        if (set.Count == 0)
        {
            return;
        }
        foreach (string item in set)
        {
            Console.WriteLine($"-{item}");
        }   
    }

    private static string[] SplitIntoWords(string text)
    {
        char[] delimiters = 
            { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')', '\n', '\r', '\t' };
        string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        return words;
    }

    public static HashSet<char> FindVoiceless(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new HashSet<char>();
        }

        string text = File.ReadAllText(filePath, Encoding.UTF8);
        string[] words = SplitIntoWords(text);

        if (words.Length == 0)
        {
            return new HashSet<char>();
        }

        HashSet<char> voicelessCons = new HashSet<char>
        {
            'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ'
        };

        HashSet<char> consInEveryWord = new HashSet<char>(voicelessCons);

        foreach (string word in words)
        {
            string lowerWord = word.ToLower();

            HashSet<char> consInCurrentWord = new HashSet<char>();

            foreach (char ch in lowerWord)
            {
                if (voicelessCons.Contains(ch))
                {
                    consInCurrentWord.Add(ch);
                }
            }

            consInEveryWord.IntersectWith(consInCurrentWord);

            if (consInEveryWord.Count == 0)
                break;
        }

        HashSet<char> missingCons = new HashSet<char>(voicelessCons);
        missingCons.ExceptWith(consInEveryWord);

        return missingCons;
    }

    public static void PrintResult(string filePath)
    {
        if (File.Exists(filePath))
        {
            Console.WriteLine("\nСодержимое файла:");
            string content = File.ReadAllText(filePath, Encoding.UTF8);
            Console.WriteLine(content);
        }

        HashSet<char> missingCons = FindVoiceless(filePath);

        if (missingCons.Count == 0)
        {
            Console.WriteLine("\nВсе глухие согласные входят во все слова!");
            return;
        }

        List<char> sortedList = new List<char>(missingCons);
        sortedList.Sort();

        Console.WriteLine("\nГлухие согласные буквы, которые не входят " +
            "хотя бы в одно слово:");

        foreach (char ch in sortedList)
        {
            Console.WriteLine($"  '{ch}'");
        }

        Console.WriteLine($"\nВсего: {sortedList.Count} букв");
    }

    public static Dictionary<string, int> ReadItemsToDict(string filePath)
    {
        Dictionary<string, int> participants = new Dictionary<string, int>();

        if (!File.Exists(filePath))
        {
            return participants;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
                
            string[] parts = line.Split(' ');

            if (parts.Length != 5)
            {
                continue;
            }

            string fullName = $"{parts[0]} {parts[1]}";

            if (int.TryParse(parts[2], out int s1) &&
                int.TryParse(parts[3], out int s2) &&
                int.TryParse(parts[4], out int s3))
            {
                participants.TryAdd(fullName, s1 + s2 + s3);
            }
        }

        return participants;
    }

    public static int FindMaxScore(Dictionary<string, int> items)
    {
        if (items.Count == 0)
        {
            return -1;
        }
            
        int maxScore = -1;
        foreach (var participant in items)
        {
            if (participant.Value > maxScore)
            {
                maxScore = participant.Value;
            }
        }
        return maxScore;
    }

    public static List<string> GetWinners(Dictionary<string, int> participants)
    {
        List<string> winners = new List<string>();

        if (participants.Count == 0)
        {
            return winners;
        }
            
        int maxScore = FindMaxScore(participants);

        foreach (var participant in participants)
        {
            if (participant.Value == maxScore)
            {
                winners.Add(participant.Key);
            }
        }

        return winners;
    }

    public static void PrintResults(string filePath)
    {
        Dictionary<string, int> participants = ReadItemsToDict(filePath);

        if (participants.Count == 0)
        {
            Console.WriteLine("Нет данных об участниках!");
            return;
        }

        Console.WriteLine("\nУчастники");
        foreach (var p in participants)
        {
            Console.WriteLine($"{p.Key}: {p.Value} баллов");
        }

        int maxScore = FindMaxScore(participants);
        List<string> winners = GetWinners(participants);

        Console.WriteLine($"\nПобедители (максимальный балл: {maxScore})");

        foreach (string winner in winners)
        {
            Console.WriteLine(winner);
        }

        Console.WriteLine($"\nВсего победителей: {winners.Count}");
    }
}