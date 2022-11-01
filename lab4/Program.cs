
namespace lab4;
static class Program
{
    static void Main()
    {
        // FirstTask.Demonstration();
        // ThirdTask.DoAbcSort();
        var arr = Helper.CreateArray(10, 1, 10);
        Helper.PrintArray(arr);
        FirstTask.QuickSort(arr,0,arr.Length - 1,0);
        Helper.PrintArray(arr);
    }
    
}