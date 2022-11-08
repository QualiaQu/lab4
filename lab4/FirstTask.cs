namespace lab4;

public static class FirstTask
{
    internal static void Demonstration()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Создаём массив со случайными числами!");
            Console.Write("Введите размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите возможно-минимальный элемент массива: ");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите возможно-максимальный элемент массива: ");
            int max = Convert.ToInt32(Console.ReadLine());
            int[] arr = Helper.CreateArray(size, min, max);
            Console.WriteLine("Введите длительность задержки в миллисекундах: \n1000 миллисекунд = 1 секунда");
            int time = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Сформированный массив: ");
            Helper.PrintArray(arr);
            Console.WriteLine("Выберите сортировку:\n1 - сортировка выбором\n2 - быстрая сортировка\n");
            var sort = Console.ReadLine();
            switch (sort)
            {
                case "1":
                    Algorithms.SelectionSort(arr, time);
                    break;
                case "2":
                    Algorithms.QuickSort(arr, 0,arr.Length - 1, time);
                    break;
                default:
                    Console.WriteLine("Попробуйте ещё разок");
                    Console.ReadLine();
                    break;
            }
            
            Console.WriteLine();
            Console.Write("Отсортированный массив: ");
            Helper.PrintArray(arr);
            
            Console.WriteLine("Нажмите END для выхода или любую клавишу чтобы повторить");
        } while (Console.ReadKey().Key != ConsoleKey.End);
    }
}