using System.Diagnostics;

namespace lab4;

public static class Test
{

    static void DoTest(string type)
    {
        var textLowerAbc = (File.ReadAllText("text_100_000.txt")).ToLower();
        var textLowerBubble = (File.ReadAllText("text10000.txt")).ToLower();
        string[] array = Array.Empty<string>();
        
        switch (type)
        {
            case "Abc":
                array = textLowerAbc.Split(' ');
                break;
            case "Bubble":
                array = textLowerBubble.Split(' ');
                break;
        }
        File.AppendAllText(Path.GetFullPath("./uniqueWords.txt"), Helper.PrintCountWords(Algorithms.AbcSort(array.ToList())));

        Stopwatch stopwatch = new Stopwatch();

        string results = "Объём данных;Время (миллисекунды)\n";

        while (array.Length != 0)
        {
            List<double> timeList = new List<double>();
            for (int k = 1; k <= 10; k++)
            {
                List<string> sort = array.ToList();
                stopwatch.Restart();
                switch (type)
                {
                    case "Abc":
                        Algorithms.AbcSort(sort);
                        break;
                    case "Bubble":
                        Algorithms.BubbleSort(sort);
                        break;
                }
                stopwatch.Stop();
                timeList.Add(stopwatch.Elapsed.TotalMilliseconds * 1000);

            }
            Helper.TimeListCleaning(timeList);

            var averageTime = timeList.ToArray().Sum() / Helper.FindCount(timeList);
            timeList.Clear();

            results += $"{array.Length};{Math.Round(averageTime, 3)}\n";
            Array.Resize(ref array, array.Length - 100);
        }
        Helper.SaveResults(results);
    }
}