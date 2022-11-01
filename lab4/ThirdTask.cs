namespace lab4;

public static class ThirdTask
{
    internal static void DoAbcSort()
    {
        var result = String.Join(" ", AbcSort(File.ReadAllText("text1.txt")
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

    static List<string> AbcSort(List<string> words, int rank = 0)
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
}