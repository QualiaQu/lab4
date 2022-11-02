
namespace lab4;

public static class FirstTask
{
    internal static void Demonstration()
    {
        do
        {
            Console.WriteLine("Создаём массив со случайными числами!");
            Console.Write("Введите размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите возможно-минимальный элемент массива: ");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите возможно-максимальный элемент массива: ");
            int max = Convert.ToInt32(Console.ReadLine());
            int[] arr = Helper.CreateArray(size, min, max);
            Console.WriteLine("Введите длительность задержки в миллисекундах: ");
            int time = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Сформированный массив: ");
            Helper.PrintArray(arr);
            Console.WriteLine("Выберите сортировку:\n1 - сортировка выбором\n2 - быстрая сортировка\n");
            var sort = Console.ReadLine();
            Console.Write("\b");
            Console.Write("\b");
            switch (sort)
            {
                case "1":
                    SelectionSort(arr, time);
                    break;
                case "2":
                    QuickSort(arr, 0,arr.Length - 1, time);
                    break;
                default:
                    Console.WriteLine("Ропробуйте ещё разок");
                    break;
            }
            Console.WriteLine();
            Console.Write("Отсортированный массив: ");
            Helper.PrintArray(arr);
            
            Console.WriteLine("Нажмите END для выхода или любую клавишу чтобы повторить");
            Console.Write("\b");
        } while (Console.ReadKey().Key != ConsoleKey.End);
    }
    static void SelectionSort(int[] array, int time)
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
            
            Console.WriteLine($"Выбранный минимальный элемент = {array[min]}");
            Helper.PrintWithHighlight(array, min);
            Thread.Sleep(time);
            if (array[min] == array[i])
            {
                Console.WriteLine("Элемент на своём месте, замены не будет");
                continue;
            }
            Console.WriteLine($"Меняем его с элементом {i} месте");
            Thread.Sleep(time);
            (array[i], array[min]) = (array[min], array[i]);
            Helper.PrintWithHighlight(array,i,min);
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
        Helper.PrintWithHighlight(array, marker);
        Thread.Sleep(time);
        
        return marker;
    }

    static void QuickSort(int[] array, int start, int end, int time)
    {
        if (start >= end) 
            return;

        int pivot = Partition (array, start, end, time);
        QuickSort(array, start, pivot - 1, time);
        QuickSort(array, pivot + 1, end, time);
    }
}