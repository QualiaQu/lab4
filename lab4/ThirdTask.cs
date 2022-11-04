namespace lab4;

public static class ThirdTask
{
    internal static void DoAbcSort()
    {
        var result = String.Join(" ", Algorithms.AbcSort(File.ReadAllText("text1.txt")
                .ToLower()
                .Split(' ', ',', '.')
                .ToList()))
            .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).GroupBy(x => x)
            .Where(x => x.Any())
            .Select(x => new { Word = x.Key, Frequency = x.Count() });
        
        foreach (var item in result)
        {
            Console.WriteLine("Слово: {0}\tКоличество повторов: {1}", item.Word, item.Frequency);
        }
    }
}