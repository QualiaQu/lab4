
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
            Thread.Sleep(time);
            if (array[min] == array[i])
            {
                Console.WriteLine("Элемент на своём месте, замены не будет");
                continue;
            }
            Console.WriteLine($"Меняем его с элементом {i} месте");
            Thread.Sleep(time);
            (array[i], array[min]) = (array[min], array[i]);
            Helper.PrintArray(array);
            Thread.Sleep(time);
        }
    }
    static int Partition (int[] array, int start, int end) 
    {
        int temp;
        int marker = start;
        for (int i = start; i < end; i++) 
        {
            if (array[i] < array[end]) 
            {
                temp = array[marker]; 
                array[marker] = array[i];
                array[i] = temp;
                marker += 1;
            }
        }
        temp = array[marker];
        array[marker] = array[end];
        array[end] = temp;
        return marker;
    }

    internal static void QuickSort (int[] array, int start, int end, int time)
    {
        if (start >= end) 
        {
            return;
        }
        int pivot = Partition (array, start, end);
        Console.WriteLine(pivot);
        return;
        QuickSort (array, start, pivot - 1, time);
        QuickSort (array, pivot + 1, end, time);
    }
}