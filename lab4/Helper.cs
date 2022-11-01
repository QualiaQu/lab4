namespace lab4;

public static class Helper
{
    internal static void PrintArray(int[] arr)
    {
        Console.WriteLine("[{0}]", string.Join(", ", arr));
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