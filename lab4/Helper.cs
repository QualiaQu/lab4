namespace lab4;

public static class Helper
{
    internal static void PrintArray(int[] arr)
    {
        Console.Write("[");
        string? result = null;
        foreach (var elem in arr)
        {
            result += elem + ", ";
        }
        Console.Write(result!.Remove(result.Length - 2));
        Console.Write("]");
        Console.Write("\n");
    }
    internal static void PrintWithHighlight(int[] arr, int index)
    {
        Console.Write("[");
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == index)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", ");
            }
            else
            {
                Console.Write(arr[i] + ", ");
            }
        }
        Console.Write("\b\b");
        Console.Write("]");
        Console.Write("\n");
    }
    internal static void PrintWithHighlight(int[] arr, int index1, int index2)
    {
        Console.Write("[");
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == index1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", ");
                continue;
            }
            if (i == index2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", ");
                
            }
            else
            {
                Console.Write(arr[i] + ", ");
            }
        }
        Console.Write("\b\b");
        Console.Write("]");
        Console.Write("\n");
    }

    internal static int[] CreateArray(int size, int min, int max)
    {
        var range = Tuple.Create(min, max + 1);
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            Random random = new Random();
            arr[i] = random.Next(range.Item1, range.Item2);
        }

        return arr;
    }

    public static void PrintWithHighlightLeftRight(int[] arr, int index)
    {
        Console.Write("[");
        for (int i = 0; i < arr.Length; i++)
        {
            if (i < index)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", ");
                continue;
            }
            if (i == index)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", ");
            }
            if (i > index)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(arr[i]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", ");
            }
        }
        Console.Write("\b\b");
        Console.Write("]");
        Console.Write("\n");
    }

    internal static string PrintCountWords(List<string> sort)
    {
        string uniqueWords = "";
        string result = string.Join(" ", sort);

        string[] words1 = result.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var input = words1.GroupBy(x => x)
            .Where(x => x.Any())
            .Select(x => new { Word = x.Key, Frequency = x.Count() });
        var enumerable = input.ToList();
        uniqueWords += $"Количество уникальных слов: {enumerable.Count()}\n";
        foreach (var item in enumerable)
        {
            string str = $"Слово: {item.Word}";
            uniqueWords += (str.PadRight(25));
            uniqueWords += ($"Количество повторов: {item.Frequency}\n");
        }
        return uniqueWords;
    }

    internal static void SaveResults(string results)
    {
        File.WriteAllText(Path.GetFullPath("./results100000.csv"), string.Empty);
        File.AppendAllText(Path.GetFullPath("./results100000.csv"), results);
    }

    internal static void TimeListCleaning(List<double> timeList)
    {
        var min = timeList.Min();
        for (var i = 0; i < timeList.Count; i++)
        {
            if (timeList[i] - min > 1.5)
            {
                timeList[i] = 0;
            }
        }
    }

    internal static int FindCount(List<double> timeList)
    {
        return timeList.Count(i => i != 0);
    }
}