namespace lab4;

public static class Helper
{
    internal static void PrintArray(int[] arr)
    {
        Console.Write("[");
        // Console.Write(string.Join(", ", arr));
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
                Console.ForegroundColor = ConsoleColor.Black;
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
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(", ");
            }
            if (i == index2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(arr[i]);
                Console.ForegroundColor = ConsoleColor.Black;
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
}