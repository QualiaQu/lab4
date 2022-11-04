namespace lab4;

public static class Algorithms
{
    internal static List<string> AbcSort(List<string> words, int rank = 0)
    {
        if (words.Count <= 1)
            return words;

        var square = new Dictionary<char, List<string>>(62);
        var result = new List<string>();
        int shortWordsCounter = 0;
        foreach (var word in words)
        {
            if (rank < word.Length)
            {
                if (square.ContainsKey(word[rank]))
                    square[word[rank]].Add(word);
                else
                    square.Add(word[rank], new List<string> { word });
            }
            else
            {
                result.Add(word);
                shortWordsCounter++;
            }
        }
        if (shortWordsCounter == words.Count)
            return words;

        for (char i = 'a'; i <= 'z'; i++)
        {
            if (square.ContainsKey(i))
            {
                foreach (var word in AbcSort(square[i], rank + 1))
                    result.Add(word);
            }
        }
        return result;
    }
    internal static void SelectionSort(int[] array, int time)
    {
        for (int i = 0; i < array.Length; i++)
        {
            var min = i;
            for (int j = i; j < array.Length; j++)
            {
                if (array[j] < array[min])
                {
                    min = j; 
                }
            }

            Console.WriteLine($"Шаг номер {i + 1}.");
            Console.WriteLine($"Выбранный минимальный элемент = {array[min]}");
            Helper.PrintWithHighlight(array, min);
            Thread.Sleep(time);
            if (array[min] == array[i])
            {
                Console.WriteLine("Элемент на своём месте, замены не будет");
                Console.WriteLine();
                continue;
            }
            Console.WriteLine($"Меняем его с элементом {i} месте");
            Helper.PrintWithHighlight(array,i,min);
            foreach (var k in array)
            {
                Console.Write(" ↓ ");
            }
            Console.WriteLine();
            Thread.Sleep(time);
            (array[i], array[min]) = (array[min], array[i]);
            Helper.PrintWithHighlight(array,i,min);
            Console.WriteLine();
            Thread.Sleep(time);
        }
    }

    static int Partition(int[] array, int start, int end, int time) 
    {
        int marker = start;
        Console.WriteLine($"Выбранный опорный элемент (pivot) = {array[end]} c индексом {end}");
        Helper.PrintWithHighlight(array, end);
        Thread.Sleep(time);
        for (int i = start; i < end; i++) 
        {
            if (array[i] < array[end])
            {
                (array[marker], array[i]) = (array[i], array[marker]);
                marker += 1;
            }
        }
        
        (array[marker], array[end]) = (array[end], array[marker]);
        Console.WriteLine("Расставляем все элементы меньше опорного слева, а больше - справа");
        Helper.PrintWithHighlightLeftRight(array, marker);
        Thread.Sleep(time);
        
        return marker;
    }

    internal static void QuickSort(int[] array, int start, int end, int time)
    {
        if (start >= end) 
            return;

        int pivot = Partition (array, start, end, time);
        QuickSort(array, start, pivot - 1, time);
        QuickSort(array, pivot + 1, end, time);
    }
}