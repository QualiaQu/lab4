namespace lab4;

public static class ThirdTask
{
    internal static void DemonstrationAbc()
    {
        var result = String.Join(" ", Algorithms.AbcSort(File.ReadAllText("text.txt")
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

    internal static void DemonstrationBubble()
    {
        var result = String.Join(" ", Algorithms.BubbleSort(File.ReadAllText("textForBubble.txt")
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