using System.Xml.Serialization;
public class FileClass
{
    private static Random rand = new Random();

    // Заполнение файла случайными числами (по одному в строке)
    public static void RandomFileOne
        (string name = "file.txt", int count = 10, 
        int min = -100, int max = 100)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, name);
        Random rand = new Random();
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(rand.Next(min, max + 1));
            }
        }
    }

    // Сумма квадратов элементов
    public static long SumOfSquares(string fileName = "file.txt")
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
        long sum = 0;
        foreach (string line in File.ReadLines(filePath))
        {
            if (int.TryParse(line.Trim(), out int number))
            {
                sum += (long)number * number;
            }
        }
        return sum;
    }


    // Заполнение файла случайными числами (по несколько в строке)
    public static void RandomFileSeveral
        (string filePath, int lineCount, int numbersPerLine, 
        int minValue = -100, int maxValue = 100)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
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
    public static long ProductElements(string filePath)
    {
        long product = 1;
        bool hasNumbers = false;

        foreach (string line in File.ReadLines(filePath))
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
    public static void CopyLinesOfLength(string sourceFile, string destFile, int targetLength)
    {
        using (StreamReader reader = new StreamReader(sourceFile))
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
    public static void BinaryFileRandom(string filePath, int count, int minValue = -100, int maxValue = 100)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(rand.Next(minValue, maxValue + 1));
            }
        }
    }

    // Вычислить произведение нечетных отрицательных компонент файла
    public static long BinaryProductNegative(string filePath)
    {
        long product = 1;
        bool hasNumbers = false;

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
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
}